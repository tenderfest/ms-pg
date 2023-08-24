using PgConvert;
using PgConvert.Config;
using PgConvert.Element;

namespace ConvertToPg;

public partial class FormMain : Form
{
	private const string PrePathInFile = "..\\..\\..\\..\\..\\!Дополнительно\\2postgres";
	private readonly ConvertMsToPg convert;
	private readonly Color _resultColor;
	private readonly Color _sourceColor;

	private bool _isTableSelected = false;
	private ElmType selectedElementType = ElmType.None;

	public FormMain()
	{
		InitializeComponent();
		_resultColor = labelResultTree.BackColor;
		_sourceColor = labelSourceElte.BackColor;
		convert = new ConvertMsToPg();
		MakeTypeCheckboxes();
	}

	/// <summary>
	/// Создание чекбоксов всех типов элементов
	/// </summary>
	private void MakeTypeCheckboxes()
	{
		int i = 0, y = 0;
		foreach (var elementType in Enum.GetValues(typeof(ElmType)))
		{
			RadioButton radioButton = new();
			groupBoxCheckElmType.Controls.Add(radioButton);

			radioButton.AutoSize = true;
			radioButton.Location = new Point(6, 22 + y);
			radioButton.Name = $"radioButton{i}";
			radioButton.Size = new Size(83, 19);
			radioButton.TabIndex = i;
			radioButton.Text = elementType.ToString();
			radioButton.UseVisualStyleBackColor = true;
			radioButton.Tag = (ElmType)elementType;
			radioButton.Checked = false;
			radioButton.CheckedChanged += CheckBoxElmType_CheckedChanged;

			i++;
			y += 25;
		}
	}

	#region Открытие исходного файла

	/// <summary>
	/// Загрузка файла проекта
	/// </summary>
	private void ButtonLoad_Click(object sender, EventArgs e)
	{
		OpenFileDialog openFileDialog = new()
		{
			Filter =
				$"*{ConvertMsToPg._extProj.ToUpper()}|*{ConvertMsToPg._extProj}|" +
				$"*{ConvertMsToPg._extSql.ToUpper()}|*{ConvertMsToPg._extSql}",
			InitialDirectory = Application.StartupPath + PrePathInFile,
		};

		if (openFileDialog.ShowDialog() != DialogResult.OK ||
			string.IsNullOrEmpty(openFileDialog.FileName))
			return;

		var err = convert.LoadFile(openFileDialog.FileName);
		if (!string.IsNullOrEmpty(err))
		{
			MessageBox.Show(err);
			return;
		}

		AfterLoadElements();
	}

	#endregion

	#region Обработка событий нажатия на кнопки

	private void ButtonSetup_Click(object sender, EventArgs e)
	{
		ConvertMsToPgCfg cfg = convert.Config;
		var formCfg = new FormCfg(cfg);
		if (formCfg.ShowDialog(this) != DialogResult.OK)
			return;

		convert.SetConfig(
			cfg.Databases,
			cfg.FreeElements,
			formCfg.SkipOperation,
			formCfg.SkipElement);

		AfterLoadElements();
	}

	private void ButtonSave_Click(object sender, EventArgs e)
	{
		FolderBrowserDialog saveFileDialog = new()
		{
			InitialDirectory = PrePathInFile,
		};
		if (saveFileDialog.ShowDialog() != DialogResult.OK)
			return;

		var errMessage = convert.SaveFile(saveFileDialog.SelectedPath, out string projectFile);
		if (string.IsNullOrEmpty(errMessage))
		{
			errMessage = $"Проект сохранён в файле {projectFile}";
		}
		MessageBox.Show(errMessage);
	}

	/// <summary>
	/// Добавить выбранные элементы в базу данных
	/// </summary>
	private void ButtonAdd_Click(object sender, EventArgs e)
	{
		if (convert.YesElementsForAddDatabase)
		{
			SetButtonAddToOriginal();
			return;
		}

		var selectedElements = GetSelectedElements();
		convert.SetElementsForAddDatabase(selectedElements);

		if (selectedElements.Any())
		{
			buttonAdd.Text = "Отменить";
			EnableDisableControls(false);
		}
	}

	#endregion

	#region Обработка событий выбора чекбоксов

	/// <summary>
	/// Смена выбранного типа элементов
	/// </summary>
	private void CheckBoxElmType_CheckedChanged(object sender, EventArgs e)
	{
		var checkBox = (RadioButton)sender;
		if (!checkBox.Checked) return;
		selectedElementType = (ElmType)checkBox.Tag;
		FillTables();
	}

	private void CheckedListBoxTable_SelectedValueChanged(object sender, EventArgs e)
	{
		textBoxContent.Text = string.Empty;
		if (checkedListBoxElements.SelectedItem is not DtElement dtElement)
			return;

		textBoxContent.BackColor = _sourceColor;
		textBoxContent.Text = dtElement.GetEmenenlContent;

		FillTreeView(dtElement);
	}

	#endregion

	#region Обработка событий выбора радио-кнопок

	private void RadioButtonDatabase_CheckedChanged(object sender, EventArgs e)
	{
		if (sender is not RadioButton control ||
			!control.Checked ||
			control.Tag is not OnePgDatabase dataBase)
			return;

		convert.SelectedDataBase = dataBase;

		// добавление элементов к БД
		if (convert.YesElementsForAddDatabase)
		{
			convert.AddSelectedElementsToDatabase();
			// разблокировать контролы, вернуть кнопку "Добавить" в оригинальный вид
			SetButtonAddToOriginal();
		}
		// отобразить список элементов выбранной БД
		FillTables();
	}

	private void RadioButtonNoDatabase_CheckedChanged(object sender, EventArgs e)
	{
		bool IsSelectNoDatabase = ((RadioButton)sender).Checked;

		buttonAdd.Enabled = IsSelectNoDatabase;
		buttonDelete.Enabled = !IsSelectNoDatabase;
		if (IsSelectNoDatabase)
			convert.SelectedDataBase = null;
		FillTables();
	}

	private void RadioButtonShowTables_CheckedChanged(object sender, EventArgs e)
	{
		if (!_isTableSelected) return;
		if (((RadioButton)sender).Checked)
			FillTables();
	}

	#endregion

	private void FillTables()
	{
		_isTableSelected = ElmType.Table == selectedElementType;
		groupBoxShowTable.Enabled = _isTableSelected;

		var showCreateTableOnly = _isTableSelected && radioButtonShowTablesCreate.Checked;
		var elements = convert.GetElements(selectedElementType, showCreateTableOnly);
		checkedListBoxElements.BeginUpdate();
		checkedListBoxElements.Items.Clear();
		if (null != elements && elements.Any())
			checkedListBoxElements.Items.AddRange(elements);
		checkedListBoxElements.EndUpdate();
	}

	private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
	{
		textBoxContent.Text = string.Empty;

		if (treeView.SelectedNode == null ||
			treeView.SelectedNode.Tag == null)
			return;

		textBoxContent.BackColor = _sourceColor;
		switch (treeView.SelectedNode.Tag)
		{
			case DtElement dtElement:
				textBoxContent.Text = dtElement.GetEmenenlContent;
				break;
			case DtField dtField:
				textBoxContent.Text = dtField.ToString();
				break;
		}
	}

	private void FillTreeView(DtElement dtElement)
	{
		static TreeNode MakeTreeNode(string treeNodeName, IEnumerable<object> list)
		{
			var treeNode = new TreeNode(treeNodeName)
			{
				ForeColor = Color.Blue,
			};
			treeNode.Nodes.AddRange(list
				.Select(f => new TreeNode(f.ToString()) { Tag = f, })
				.ToArray());
			return treeNode;
		}

		treeView.BeginUpdate();
		treeView.Nodes.Clear();
		try
		{
			if (dtElement is not ElTable elTable)
				return;
			if (elTable.Fields.Any())
				treeView.Nodes.Add(MakeTreeNode("поля", elTable.Fields));
			if (elTable.Indexes.Any())
				treeView.Nodes.Add(MakeTreeNode("индексы", elTable.Indexes));
			if (elTable.Triggers.Any())
				treeView.Nodes.Add(MakeTreeNode("триггеры", elTable.Triggers));
		}
		finally
		{
			treeView.ExpandAll();
			treeView.EndUpdate();
		}
	}

	/// <summary>
	/// Выполнить действия после загрузки и разбора
	/// </summary>
	private void AfterLoadElements()
	{
		string errorMessage = convert.ParseSource();
		if (!string.IsNullOrEmpty(errorMessage))
		{
			ShowErrorMessage(errorMessage);
			return;
		}
		SetDatabasesRadiobuttons();
		EnableDisableControls(true);
		buttonSave.Enabled = true;
	}

	private void SetDatabasesRadiobuttons()
	{
		groupBoxNewDatabases.SuspendLayout();
		groupBoxNewDatabases.Controls.Clear();
		groupBoxNewDatabases.Controls.Add(radioButtonNoDatabase);

		foreach (var db in convert.GetDatabases)
		{
			var radioButtonDb = new RadioButton();
			groupBoxNewDatabases.Controls.Add(radioButtonDb);
			radioButtonDb.AutoSize = true;
			radioButtonDb.Dock = DockStyle.Top;
			radioButtonDb.ForeColor = Color.Blue;
			radioButtonDb.Location = new Point(0, 38);
			radioButtonDb.Name = $"radioButton{db}";
			radioButtonDb.Size = new Size(107, 19);
			radioButtonDb.TabIndex = 5;
			radioButtonDb.Text = $"{db}";
			radioButtonDb.UseVisualStyleBackColor = true;
			radioButtonDb.Tag = db;
			radioButtonDb.CheckedChanged += RadioButtonDatabase_CheckedChanged;
		}
		groupBoxNewDatabases.ResumeLayout();
		radioButtonNoDatabase.Checked = true;
	}

	private void SetButtonAddToOriginal()
	{
		buttonAdd.Text = "Добавить";
		convert.SetElementsForAddDatabase(null);
		EnableDisableControls(true);
	}

	private void EnableDisableControls(bool isEnable)
	{
		panelTop.Enabled =
			splitContainerEltText.Enabled =
			groupBoxCheckElmType.Enabled = isEnable;
	}

	private static void ShowErrorMessage(string err) =>
		MessageBox.Show(err, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

	// ----------------------------------------------------
	// отсортировать

	private void ButtonDelete_Click(object sender, EventArgs e)
	{
		var selectedElements = GetSelectedElements();
		convert.RemoveElementsFromDatabase(selectedElements);
		FillTables();
	}

	private List<DtElement> GetSelectedElements()
	{
		var list = new List<DtElement>();
		var itemsCount = checkedListBoxElements.CheckedItems.Count;
		if (itemsCount >= 1)
			for (int i = 0; i < itemsCount; i++)
				list.Add(checkedListBoxElements.CheckedItems[i] as DtElement);

		return list;
	}

	private void LinkLabelSelectAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		checkedListBoxElements.BeginUpdate();
		for (int i = 0; i < checkedListBoxElements.Items.Count; i++)
			checkedListBoxElements.SetItemChecked(i, true);
		checkedListBoxElements.EndUpdate();
	}

	private void LinkLabelInvertSelect_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		checkedListBoxElements.BeginUpdate();
		for (int i = 0; i < checkedListBoxElements.Items.Count; i++)
			checkedListBoxElements.SetItemChecked(i, !checkedListBoxElements.CheckedItems.Contains(checkedListBoxElements.Items[i]));
		checkedListBoxElements.EndUpdate();
	}
}