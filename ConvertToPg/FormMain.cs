using PgConvert;
using PgConvert.Config;

namespace ConvertToPg
{
	public partial class FormMain : Form
	{
		private const string PrePathInFile = "..\\..\\..\\..\\..\\!Дополнительно\\2postgres";
		private readonly ConvertMsToPg convert;
		private readonly Color _resultColor;
		private readonly Color _sourceColor;

		public FormMain()
		{
			InitializeComponent();
			_resultColor = labelResultTree.BackColor;
			_sourceColor = labelSourceElte.BackColor;

			convert = new ConvertMsToPg();
			// показ типов элементов
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
				radioButton.Checked = (ElmType)elementType == ElmType.None;
				radioButton.CheckedChanged += CheckBox_CheckedChanged;

				i++;
				y += 25;
			}
		}

		private void CheckBox_CheckedChanged(object sender, EventArgs e) =>
			FillTables();

		private void FillTables()
		{
			List<ElmType> elmTypeList = new();
			foreach (var ctrl in groupBoxCheckElmType.Controls)
			{
				var chBox = (RadioButton)ctrl;
				if (chBox.Checked)
					elmTypeList.Add((ElmType)chBox.Tag);
			}

			checkedListBoxTable.BeginUpdate();
			checkedListBoxTable.Items.Clear();
			checkedListBoxTable.Items.AddRange(convert.GetElements(elmTypeList.ToArray()));
			checkedListBoxTable.EndUpdate();
		}

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

			ShowElements();
		}

		private void ShowElements()
		{
			SetDatabasesRadiobuttons();
			convert.ParseSource();

			checkedListBoxTable.BeginUpdate();
			checkedListBoxTable.Items.Clear();
			checkedListBoxTable.Items.AddRange(convert.GetAllElements());
			checkedListBoxTable.EndUpdate();

			groupBoxCheckElmType.Enabled =
				buttonCreate.Enabled =
				buttonSave.Enabled = checkedListBoxTable.Items.Count > 0;
		}

		private void ButtonSetup_Click(object sender, EventArgs e)
		{
			ConvertMsToPgCfg cfg = convert.GetConfig();
			var formCfg = new FormCfg(cfg);
			if (formCfg.ShowDialog(this) != DialogResult.OK)
				return;
			ConvertMsToPgCfg newCfg = new()
			{
				Databases = cfg.Databases,
				SkipOperation = formCfg.SkipOperation,
				SkipElement = formCfg.SkipElement,
			};

			convert.SetConfig(newCfg);
			ShowElements();
		}

		private void SetDatabasesRadiobuttons()
		{
			panelLeft.SuspendLayout();
			panelLeft.Controls.Clear();
			panelLeft.Controls.Add(radioButtonNone);
			panelLeft.Controls.Add(groupBoxCheckElmType);
			radioButtonNone.Checked = true;

			foreach (var db in convert.GetConfig().Databases)
			{
				var radioButtonDb = new RadioButton();
				panelLeft.Controls.Add(radioButtonDb);
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
			}
			panelLeft.ResumeLayout();
		}

		private static void ShowErrorMessage(string err)
			=> MessageBox.Show(err, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

		private void CheckedListBoxTable_SelectedValueChanged(object sender, EventArgs e)
		{
			checkedListBoxFkey.Items.Clear();
			textBoxContent.Text = string.Empty;

			if (checkedListBoxTable.SelectedItem is not DtElement dtTable)
				return;

			if (null != dtTable.GetChild)
				checkedListBoxFkey.Items.AddRange(dtTable.GetChild);
			textBoxContent.BackColor = _sourceColor;
			textBoxContent.Text = dtTable.GetEmenenlContent;
		}

		private void ButtonSave_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog saveFileDialog = new()
			{
				InitialDirectory = PrePathInFile,
			};
			var save = saveFileDialog.ShowDialog();
			if (save != DialogResult.OK)
				return;

			var err = convert.SaveFile(saveFileDialog.SelectedPath, out string projectFile);
			if (!string.IsNullOrEmpty(err))
			{
				MessageBox.Show(err);
			}
			else MessageBox.Show($"Проект сохранён в файле {projectFile}");
		}

		private void ButtonParseSource_Click(object sender, EventArgs e)
			=> FillTables();

		private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			textBoxContent.Text = string.Empty;

			if (treeView.SelectedNode == null ||
				treeView.SelectedNode.Tag == null ||
				treeView.SelectedNode.Tag is not PgElement dtTable)
				return;

			textBoxContent.BackColor = _resultColor;
			textBoxContent.Text = dtTable.GetEmenenlContentPostgreSql;
		}

		private void RadioButtonNone_CheckedChanged(object sender, EventArgs e)
		{
			buttonAdd.Enabled = ((RadioButton)sender).Checked;
			buttonDelete.Enabled = !buttonAdd.Enabled;
		}
	}
}