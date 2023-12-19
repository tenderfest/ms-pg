using PgConvert;
using PgConvert.Config;
using PgConvert.Element;

namespace ConvertToPg;

public partial class FormMain : Form
{
	private const string PrePathInFile = "..\\..\\..\\..\\..\\!Дополнительно\\2postgres";
	private readonly ConvertMsToPg convert;
	private readonly Color _sourceColor;

	private bool _isTableSelected = false;
	private ElmType selectedElementType = ElmType.None;

	public FormMain()
	{
		InitializeComponent();
		_sourceColor = labelSourceElte.BackColor;
		convert = new ConvertMsToPg();
		CreateElementsTypeCheckboxes();

		// прячем полоску вкладок на tabControlEditElement
		splitContainerEditElement.Panel2.SuspendLayout();
		splitContainerEditElement.Panel2.Controls.Clear();
		splitContainerEditElement.Panel2.Controls.Add(panelEditText);
		splitContainerEditElement.Panel2.Controls.Add(tabControlEditElement);
		splitContainerEditElement.Panel2.ResumeLayout(false);

		// наполнение комбобокса с типами полей значениями
		comboBoxEditTableCurrentFieldType.Items.AddRange(Enum.GetNames(typeof(FldType)));
		comboBoxEditTableCurrentFieldType.SelectedIndex = 0;

		// наполднение комбобокса процедурных языков
		comboBoxEditFunctionLanguage.Items.AddRange(convert.GetProcedureLanguages());
		comboBoxEditFunctionLanguage.SelectedIndex = 0;
	}

	/// <summary>
	/// Создание чекбоксов всех типов элементов
	/// </summary>
	private void CreateElementsTypeCheckboxes()
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
			//cfg.FreeElementIds,
			formCfg.SkipOperation,
			formCfg.SkipElement);

		AfterLoadElements();
	}

	private void ButtonSave_Click(object sender, EventArgs e) =>
		SaveChanges();

	private void SaveChanges()
	{
		if (string.IsNullOrEmpty(convert.PathToSaveFile))
		{
			FolderBrowserDialog saveFileDialog = new()
			{
				InitialDirectory = PrePathInFile,
			};
			if (DialogResult.OK != saveFileDialog.ShowDialog())
				return;

			convert.PathToSaveFile = saveFileDialog.SelectedPath;
		}

		var errMessage = convert.SaveFile(out string projectFile);
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
		if (convert.IsPresentElementsForAddDatabase)
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
		textBoxContent.Text = dtElement.Lines.ToOneString();

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
		if (convert.IsPresentElementsForAddDatabase)
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
				textBoxContent.Text = dtElement.Lines.ToOneString();
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
		static RadioButton newRadioButtonDatabase(OnePgDatabase db, string name) => new()
		{
			Name = name,
			AutoSize = true,
			Dock = DockStyle.Top,
			ForeColor = db.IsDefault ? Color.Red : Color.Blue,
			Text = $"{db}",
			UseVisualStyleBackColor = true,
			Tag = db,
		};

		groupBoxNewDatabases.SuspendLayout();
		groupBoxNewDatabases.Controls.Clear();
		groupBoxNewDatabases.Controls.Add(radioButtonNoDatabase);

		groupBoxEditDatabasesList.SuspendLayout();
		groupBoxEditDatabasesList.Controls.Clear();
		groupBoxEditDatabasesList.Controls.Add(radioButtonEditDatabaseAll);

		foreach (var db in convert.GetDatabases)
		{
			var radioButtonDb = newRadioButtonDatabase(db, $"radioButton{db}");
			radioButtonDb.CheckedChanged += RadioButtonDatabase_CheckedChanged;
			groupBoxNewDatabases.Controls.Add(radioButtonDb);

			radioButtonDb = newRadioButtonDatabase(db, $"radioButtonEdit{db}");
			radioButtonDb.CheckedChanged += RadioButtonEditDatabase_CheckedChanged;
			groupBoxEditDatabasesList.Controls.Add(radioButtonDb);
		}

		groupBoxNewDatabases.ResumeLayout();
		groupBoxEditDatabasesList.ResumeLayout();
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
		textBoxContent.Enabled =
		groupBoxCheckElmType.Enabled =
		splitContainerSource.Enabled =
		splitContainerSourceElements.Panel2.Enabled =
			isEnable;
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

	private void ShowEditElements()
	{
		textBoxEditProcedure.Clear();
		listViewEditElements.BeginUpdate();
		listViewEditElements.Items.Clear();
		var editElements = convert.GetEditElements();
		listViewEditElements.Items.AddRange(
			editElements
			.Select(e => new ListViewItem(e.ToString())
			{
				ForeColor = ((IEdited)e).IsOk ? Color.Green : Color.Red,
				Tag = e,
			})
			.ToArray());
		listViewEditElements.EndUpdate();
	}

	private void RadioButtonEditDatabase_CheckedChanged(object sender, EventArgs e)
	{
		var radioButton = (RadioButton)sender;
		if (!radioButton.Checked)
			return;

		convert.CurrentEditDatabase = radioButton.Tag as OnePgDatabase;
		ShowEditElements();
	}

	private void RadioButtonEditElementsType_CheckedChanged(object sender, EventArgs e)
	{
		var radioButton = (RadioButton)sender;
		if (!radioButton.Checked)
			return;
		convert.SetEditElementsType(CheckEditElementsType(radioButton));
		ShowEditElements();
	}

	private EditElementsType CheckEditElementsType(RadioButton radioButton)
	{
		if (radioButton == radioButtonEditElementsTypeTable)
			return EditElementsType.Table;
		else if (radioButton == radioButtonEditElementsTypeProcedure)
			return EditElementsType.Procedure;
		else if (radioButton == radioButtonEditElementsTypeTrigger)
			return EditElementsType.Trigger;
		else
			return EditElementsType.All;
	}

	private void RadioButtonEditElements_CheckedChanged(object sender, EventArgs e)
	{
		var radioButton = (RadioButton)sender;
		if (!radioButton.Checked)
			return;

		ShowEditElements showEditElements;
		if (radioButton == radioButtonEditElementsAlert)
			showEditElements = PgConvert.ShowEditElements.Alert;
		else if (radioButton == radioButtonEditElementsOk)
			showEditElements = PgConvert.ShowEditElements.Ok;
		else
			showEditElements = PgConvert.ShowEditElements.All;
		convert.SetShowEditElements(showEditElements);
		ShowEditElements();
	}

	private void ListViewEditElements_SelectedIndexChanged(object sender, EventArgs e)
	{
		CurrentEditElement = listViewEditElements.SelectedItems.Count == 1
			? (DtElement)listViewEditElements.SelectedItems[0].Tag
			: null;
	}

	private void ListViewEditTableFieldNames_SelectedIndexChanged(object sender, EventArgs e)
	{
		textBoxEditTableCurrentField.Text = null;
		if (listViewEditTableFieldNames.SelectedItems.Count == 1)
		{
			var field = (DtField)listViewEditTableFieldNames.SelectedItems[0].Tag;
			textBoxEditTableCurrentField.Text = field.FormulaPg;
			textBoxEditTableCurrentField.Enabled = field.IsGenerated;
		}
	}

	private DtElement currentEditElement;

	/// <summary>
	/// Отображение текущего элемента для редактирования
	/// </summary>
	private DtElement CurrentEditElement
	{
		set
		{
			currentEditElement = value;
			var enableEditButtons = false;
			tabControlEditElement.SuspendLayout();
			try
			{
				// очистка текущего состояния
				listViewEditTableFieldNames.Items.Clear();
				labelEditElementType.Text =
					textBoxEditTableCurrentField.Text =
					textBoxEditProcedure.Text =
					textBoxEditTriggerFunctionName.Text =
					textBoxEditTriggerFunction.Text =
					null;
				if (null == value)
					return;

				// отображение нужной вкладки
				// показ данных для элемента
				if (SwitchCurrentElement(
					() =>
					{
						tabControlEditElement.SelectTab(0);
						// вычисляемые поля
						listViewEditTableFieldNames.Items.AddRange(
							((ElTable)value).FieldsForCorrect.Select(x =>
							new ListViewItem
							{
								Text = x.GeneratedFieldToString,
								Tag = x,
							})
							.ToArray());
						// остальные поля, для справки
						listViewEditTableFieldNames.Items.AddRange(
							((ElTable)value).Fields
							.Where(x => !x.IsGenerated)
							.Select(x =>
							new ListViewItem
							{
								Text = x.ToString(),
								Tag = x,
								ForeColor = Color.Gray,
							})
							.ToArray());
						enableEditButtons = true;
					},
					() =>
					{
						tabControlEditElement.SelectTab(1);
						textBoxEditProcedure.Text = ((ElProcedure)value).LinesPg.ToOneString();
						enableEditButtons = true;
					},
					() =>
					{
						tabControlEditElement.SelectTab(2);
						var trigger = (ElTrigger)value;
						textBoxEditTriggerFunctionName.Text = trigger.TriggerFunctionName;
						textBoxEditTriggerFunction.Text = trigger.LinesPg.ToOneString();
						if (comboBoxEditFunctionLanguage.SelectedItem != trigger.PLanguage)
							comboBoxEditFunctionLanguage.SelectedItem = trigger.PLanguage;
						else
							SetTextBoxEditTriggerFirstString(trigger);
						enableEditButtons = true;
					}))
				{
					labelEditElementType.Text = tabControlEditElement.SelectedTab.Text;
					buttonEditConfirmElement.Enabled = ((IEdited)value).CanSetOk;
					if (((IEdited)value).IsOk)
					{
						buttonEditConfirmElement.Text = "Разутвердить";
						buttonEditConfirmElement.ForeColor = Color.Red;
					}
					else
					{
						buttonEditConfirmElement.Text = "Утвердить";
						buttonEditConfirmElement.ForeColor = Color.Green;
					}
				}
			}
			finally
			{
				tabControlEditElement.ResumeLayout();
				buttonEditSave.Enabled =
					buttonEditAllUndo.Enabled =
					buttonEditUndo.Enabled = enableEditButtons;
			}
		}
		get =>
			currentEditElement;
	}
	private ElTable CurrentTable =>
		currentEditElement as ElTable;
	private ElProcedure CurrentProcedure =>
		currentEditElement as ElProcedure;
	private ElTrigger CurrentTrigger =>
		currentEditElement as ElTrigger;

	private void TextBoxEditTriggerFunctionName_TextChanged(object sender, EventArgs e)
	{
		if (CurrentEditElement is not ElTrigger trigger)
			return;
		var functionName = textBoxEditTriggerFunctionName.Text;
		if (functionName.Contains(' '))
		{
			var selectionStart = textBoxEditTriggerFunctionName.SelectionStart;
			textBoxEditTriggerFunctionName.Text = functionName.Replace(" ", string.Empty);
			textBoxEditTriggerFunctionName.SelectionStart = selectionStart - 1;
			return;
		}
		textBoxEditTriggerText.Text = trigger.GetTriggerText(functionName);
		labelEditTriggerFunctionBegin.Text = trigger.GetTriggerFunctionTextBegin();
		labelEditTriggerFunctionEnd.Text = trigger.GetTriggerFunctionTextEnd();
	}

	private void ComboBoxEditTableCurrentFieldType_SelectedIndexChanged(object sender, EventArgs e)
	{
		var fieldType = (FldType)Enum.Parse(typeof(FldType), comboBoxEditTableCurrentFieldType.SelectedItem as string);

		numericUpDownPrecision.Enabled = labelPrecision.Enabled =
			fieldType == FldType.Numeric ||
			fieldType == FldType.Char ||
			fieldType == FldType.Varchar;
		if (!numericUpDownPrecision.Enabled)
			numericUpDownPrecision.Value = 0;

		numericUpDownScale.Enabled = labelScale.Enabled =
			fieldType == FldType.Numeric;
		if (!numericUpDownScale.Enabled)
			numericUpDownScale.Value = 0;
	}

	private void ButtonEditUndo_Click(object sender, EventArgs e) =>
		UndoEdit();

	private void UndoEdit()
	{
		SwitchCurrentElement(
			() =>
			{
				MessageBox.Show("сделать");
			},
			() =>
			{
				textBoxEditProcedure.Text = CurrentProcedure.LinesPg.ToOneString();
			},
			() =>
			{
				textBoxEditTriggerFunction.Text = CurrentTrigger.LinesPg.ToOneString();
			});
	}

	private bool SwitchCurrentElement(
		Action actionTable,
		Action actionProcedure,
		Action actionTrigger)
	{
		if (null == CurrentEditElement)
			return false;
		switch (CurrentEditElement.ElementType)
		{
			case ElmType.Table: actionTable(); return true;
			case ElmType.Procedure: actionProcedure(); return true;
			case ElmType.Trigger: actionTrigger(); return true;
			default:
				MessageBox.Show($"Неизвестный тип элемента {CurrentEditElement.ElementType}");
				return false;
		}
	}

	private void ButtonEditAllUndo_Click(object sender, EventArgs e)
	{
		if (SwitchCurrentElement(
			() =>
			{
				MessageBox.Show("сделать");
			},
			() =>
			{
				NeedCorrect.LinesPgFromLines(CurrentProcedure);
			},
			() =>
			{
				NeedCorrect.LinesPgFromLines(CurrentTrigger);
				CurrentTrigger.TriggerFunctionName = null;
				textBoxEditTriggerFunctionName.Text = CurrentTrigger.TriggerFunctionName;
			}))
		{
			UndoEdit();
		}
	}

	private void ButtonEditSave_Click(object sender, EventArgs e)
	{
		if (SwitchCurrentElement(
			() =>
			{
				MessageBox.Show("сделать");
			},
			() =>
			{
				CurrentProcedure.LinesPg = textBoxEditProcedure.Text.FromOneString();
			},
			() =>
			{
				CurrentTrigger.LinesPg = textBoxEditTriggerFunction.Text.FromOneString();
			}))
		{
			SaveChanges();
		}
	}

	private void ComboBoxEditFunctionLanguage_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (CurrentEditElement is not ElTrigger trigger)
			return;
		trigger.PLanguage = comboBoxEditFunctionLanguage.SelectedItem as Plang;
		SetTextBoxEditTriggerFirstString(trigger);
	}

	private void SetTextBoxEditTriggerFirstString(ElTrigger trigger)
	{
		textBoxEditTriggerFirstString.Text = trigger.GetTriggerFunctionFirstString(out var nameIsNull);
		buttonEditOwnTriggerLanguageSave.Enabled = trigger.IsOwnVariantLanguage;
		textBoxEditTriggerFirstString.ReadOnly = !trigger.IsOwnVariantLanguage;
		textBoxEditTriggerFirstString.ForeColor = nameIsNull
			? Color.Red
			: textBoxEditTriggerFirstString.Parent.ForeColor;
	}

	private void TextBoxEditTriggerFirstString_Enter(object sender, EventArgs e) =>
		textBoxEditTriggerFirstString.SelectionStart = 0;

	private void ButtonEditOwnTriggerLanguageSave_Click(object sender, EventArgs e)
	{
		if (CurrentEditElement is not ElTrigger trigger)
			return;
		trigger.SetTriggerFunctionFirstString(textBoxEditTriggerFirstString.Text);
		SetTextBoxEditTriggerFirstString(trigger);
	}

	private void ButtonEditConfirmElement_Click(object sender, EventArgs e)
	{
		if (SwitchCurrentElement(
			() =>
			{
				MessageBox.Show("сделать");
			},
			() =>
			{
				CurrentProcedure.SetOk(true);
			},
			() =>
			{
				if (CurrentTrigger.IsOwnVariantLanguage &&
					string.IsNullOrEmpty(CurrentTrigger.OwnVariantLanguage))
				{
					MessageBox.Show("Необходимо указать собственный вариант языка триггерной функции или выбрать один из имеющихся.");
					return;
				}
				CurrentTrigger.SetOk(true);
			}))
		{
			ShowEditElements();
		}
	}
}
