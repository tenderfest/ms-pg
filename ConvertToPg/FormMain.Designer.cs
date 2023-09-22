namespace ConvertToPg;

partial class FormMain
{
	/// <summary>
	///  Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	/// <summary>
	///  Clean up any resources being used.
	/// </summary>
	/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	protected override void Dispose(bool disposing)
	{
		if (disposing && (components != null))
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	#region Windows Form Designer generated code

	/// <summary>
	///  Required method for Designer support - do not modify
	///  the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{
		panelTop = new Panel();
		labelInfo = new Label();
		buttonSave = new Button();
		buttonCreate = new Button();
		buttonSetup = new Button();
		buttonLoad = new Button();
		splitContainerEltAll = new SplitContainer();
		splitContainerCentral = new SplitContainer();
		groupBoxNewDatabases = new GroupBox();
		radioButtonNoDatabase = new RadioButton();
		buttonDelete = new Button();
		buttonAdd = new Button();
		groupBoxCheckElmType = new GroupBox();
		splitContainerSource = new SplitContainer();
		checkedListBoxElements = new CheckedListBox();
		groupBoxManySelect = new GroupBox();
		linkLabelInvertSelect = new LinkLabel();
		linkLabelSelectAll = new LinkLabel();
		groupBoxShowTable = new GroupBox();
		radioButtonShowTablesCreate = new RadioButton();
		radioButtonShowTablesAll = new RadioButton();
		labelSourceElte = new Label();
		treeView = new TreeView();
		labelFkeys = new Label();
		splitContainerEltText = new SplitContainer();
		textBoxContent = new TextBox();
		tabControlMain = new TabControl();
		tabPageToDatabases = new TabPage();
		tabPageProcedureText = new TabPage();
		tabPageResult = new TabPage();
		groupBoxResultFilter = new GroupBox();
		radioButtonResultOk = new RadioButton();
		radioButtonResultAlert = new RadioButton();
		radioButtonResultShowAll = new RadioButton();
		labelResultTree = new Label();
		panelTop.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)splitContainerEltAll).BeginInit();
		splitContainerEltAll.Panel1.SuspendLayout();
		splitContainerEltAll.Panel2.SuspendLayout();
		splitContainerEltAll.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)splitContainerCentral).BeginInit();
		splitContainerCentral.Panel1.SuspendLayout();
		splitContainerCentral.Panel2.SuspendLayout();
		splitContainerCentral.SuspendLayout();
		groupBoxNewDatabases.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)splitContainerSource).BeginInit();
		splitContainerSource.Panel1.SuspendLayout();
		splitContainerSource.Panel2.SuspendLayout();
		splitContainerSource.SuspendLayout();
		groupBoxManySelect.SuspendLayout();
		groupBoxShowTable.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)splitContainerEltText).BeginInit();
		splitContainerEltText.Panel1.SuspendLayout();
		splitContainerEltText.Panel2.SuspendLayout();
		splitContainerEltText.SuspendLayout();
		tabControlMain.SuspendLayout();
		tabPageToDatabases.SuspendLayout();
		tabPageResult.SuspendLayout();
		groupBoxResultFilter.SuspendLayout();
		SuspendLayout();
		// 
		// panelTop
		// 
		panelTop.Controls.Add(labelInfo);
		panelTop.Controls.Add(buttonSave);
		panelTop.Controls.Add(buttonCreate);
		panelTop.Controls.Add(buttonSetup);
		panelTop.Controls.Add(buttonLoad);
		panelTop.Dock = DockStyle.Top;
		panelTop.Location = new Point(0, 0);
		panelTop.Name = "panelTop";
		panelTop.Size = new Size(1045, 34);
		panelTop.TabIndex = 0;
		// 
		// labelInfo
		// 
		labelInfo.AutoSize = true;
		labelInfo.Location = new Point(230, 10);
		labelInfo.Name = "labelInfo";
		labelInfo.Size = new Size(444, 15);
		labelInfo.TabIndex = 4;
		labelInfo.Text = "Разнести элементы по БД, изменить тексты процедур, сформировать скрипты.";
		// 
		// buttonSave
		// 
		buttonSave.Dock = DockStyle.Left;
		buttonSave.Enabled = false;
		buttonSave.ForeColor = Color.Blue;
		buttonSave.Location = new Point(149, 0);
		buttonSave.Name = "buttonSave";
		buttonSave.Size = new Size(75, 34);
		buttonSave.TabIndex = 2;
		buttonSave.Text = "Сохранить";
		buttonSave.UseVisualStyleBackColor = true;
		buttonSave.Click += ButtonSave_Click;
		// 
		// buttonCreate
		// 
		buttonCreate.Dock = DockStyle.Right;
		buttonCreate.Enabled = false;
		buttonCreate.Location = new Point(930, 0);
		buttonCreate.Name = "buttonCreate";
		buttonCreate.Size = new Size(115, 34);
		buttonCreate.TabIndex = 1;
		buttonCreate.Text = "Сформировать";
		buttonCreate.UseVisualStyleBackColor = true;
		// 
		// buttonSetup
		// 
		buttonSetup.BackColor = SystemColors.ControlDark;
		buttonSetup.Dock = DockStyle.Left;
		buttonSetup.Location = new Point(74, 0);
		buttonSetup.Name = "buttonSetup";
		buttonSetup.Size = new Size(75, 34);
		buttonSetup.TabIndex = 3;
		buttonSetup.Text = "Настройки";
		buttonSetup.UseVisualStyleBackColor = false;
		buttonSetup.Click += ButtonSetup_Click;
		// 
		// buttonLoad
		// 
		buttonLoad.Dock = DockStyle.Left;
		buttonLoad.Location = new Point(0, 0);
		buttonLoad.Name = "buttonLoad";
		buttonLoad.Size = new Size(74, 34);
		buttonLoad.TabIndex = 0;
		buttonLoad.Text = "Открыть";
		buttonLoad.UseVisualStyleBackColor = true;
		buttonLoad.Click += ButtonLoad_Click;
		// 
		// splitContainerEltAll
		// 
		splitContainerEltAll.Dock = DockStyle.Fill;
		splitContainerEltAll.Location = new Point(0, 0);
		splitContainerEltAll.Name = "splitContainerEltAll";
		// 
		// splitContainerEltAll.Panel1
		// 
		splitContainerEltAll.Panel1.Controls.Add(splitContainerCentral);
		// 
		// splitContainerEltAll.Panel2
		// 
		splitContainerEltAll.Panel2.Controls.Add(splitContainerSource);
		splitContainerEltAll.Size = new Size(1031, 360);
		splitContainerEltAll.SplitterDistance = 311;
		splitContainerEltAll.TabIndex = 1;
		// 
		// splitContainerCentral
		// 
		splitContainerCentral.Dock = DockStyle.Fill;
		splitContainerCentral.Location = new Point(0, 0);
		splitContainerCentral.Name = "splitContainerCentral";
		// 
		// splitContainerCentral.Panel1
		// 
		splitContainerCentral.Panel1.Controls.Add(groupBoxNewDatabases);
		splitContainerCentral.Panel1.Controls.Add(buttonDelete);
		splitContainerCentral.Panel1.Controls.Add(buttonAdd);
		// 
		// splitContainerCentral.Panel2
		// 
		splitContainerCentral.Panel2.Controls.Add(groupBoxCheckElmType);
		splitContainerCentral.Size = new Size(311, 360);
		splitContainerCentral.SplitterDistance = 151;
		splitContainerCentral.TabIndex = 7;
		// 
		// groupBoxNewDatabases
		// 
		groupBoxNewDatabases.Controls.Add(radioButtonNoDatabase);
		groupBoxNewDatabases.Dock = DockStyle.Fill;
		groupBoxNewDatabases.ForeColor = SystemColors.ControlText;
		groupBoxNewDatabases.Location = new Point(0, 56);
		groupBoxNewDatabases.Name = "groupBoxNewDatabases";
		groupBoxNewDatabases.Size = new Size(151, 304);
		groupBoxNewDatabases.TabIndex = 4;
		groupBoxNewDatabases.TabStop = false;
		groupBoxNewDatabases.Text = "Новые базы данных";
		// 
		// radioButtonNoDatabase
		// 
		radioButtonNoDatabase.AutoSize = true;
		radioButtonNoDatabase.Dock = DockStyle.Top;
		radioButtonNoDatabase.ForeColor = SystemColors.ControlText;
		radioButtonNoDatabase.Location = new Point(3, 19);
		radioButtonNoDatabase.Name = "radioButtonNoDatabase";
		radioButtonNoDatabase.Size = new Size(145, 19);
		radioButtonNoDatabase.TabIndex = 7;
		radioButtonNoDatabase.Text = "Неопределен";
		radioButtonNoDatabase.UseVisualStyleBackColor = true;
		radioButtonNoDatabase.CheckedChanged += RadioButtonNoDatabase_CheckedChanged;
		// 
		// buttonDelete
		// 
		buttonDelete.Dock = DockStyle.Top;
		buttonDelete.Enabled = false;
		buttonDelete.ForeColor = Color.Red;
		buttonDelete.Location = new Point(0, 28);
		buttonDelete.Name = "buttonDelete";
		buttonDelete.Size = new Size(151, 28);
		buttonDelete.TabIndex = 6;
		buttonDelete.Text = "Удалить";
		buttonDelete.UseVisualStyleBackColor = true;
		buttonDelete.Click += ButtonDelete_Click;
		// 
		// buttonAdd
		// 
		buttonAdd.Dock = DockStyle.Top;
		buttonAdd.Enabled = false;
		buttonAdd.ForeColor = Color.Green;
		buttonAdd.Location = new Point(0, 0);
		buttonAdd.Name = "buttonAdd";
		buttonAdd.Size = new Size(151, 28);
		buttonAdd.TabIndex = 7;
		buttonAdd.Text = "Добавить";
		buttonAdd.UseVisualStyleBackColor = true;
		buttonAdd.Click += ButtonAdd_Click;
		// 
		// groupBoxCheckElmType
		// 
		groupBoxCheckElmType.Dock = DockStyle.Fill;
		groupBoxCheckElmType.ForeColor = SystemColors.ControlText;
		groupBoxCheckElmType.Location = new Point(0, 0);
		groupBoxCheckElmType.Name = "groupBoxCheckElmType";
		groupBoxCheckElmType.Size = new Size(156, 360);
		groupBoxCheckElmType.TabIndex = 3;
		groupBoxCheckElmType.TabStop = false;
		groupBoxCheckElmType.Text = "Типы элементов";
		// 
		// splitContainerSource
		// 
		splitContainerSource.Dock = DockStyle.Fill;
		splitContainerSource.Location = new Point(0, 0);
		splitContainerSource.Name = "splitContainerSource";
		splitContainerSource.Orientation = Orientation.Horizontal;
		// 
		// splitContainerSource.Panel1
		// 
		splitContainerSource.Panel1.Controls.Add(checkedListBoxElements);
		splitContainerSource.Panel1.Controls.Add(groupBoxManySelect);
		splitContainerSource.Panel1.Controls.Add(groupBoxShowTable);
		splitContainerSource.Panel1.Controls.Add(labelSourceElte);
		// 
		// splitContainerSource.Panel2
		// 
		splitContainerSource.Panel2.Controls.Add(treeView);
		splitContainerSource.Panel2.Controls.Add(labelFkeys);
		splitContainerSource.Size = new Size(716, 360);
		splitContainerSource.SplitterDistance = 209;
		splitContainerSource.TabIndex = 5;
		// 
		// checkedListBoxElements
		// 
		checkedListBoxElements.Dock = DockStyle.Fill;
		checkedListBoxElements.FormattingEnabled = true;
		checkedListBoxElements.Location = new Point(0, 97);
		checkedListBoxElements.Name = "checkedListBoxElements";
		checkedListBoxElements.Size = new Size(716, 112);
		checkedListBoxElements.TabIndex = 5;
		checkedListBoxElements.SelectedIndexChanged += CheckedListBoxTable_SelectedValueChanged;
		// 
		// groupBoxManySelect
		// 
		groupBoxManySelect.Controls.Add(linkLabelInvertSelect);
		groupBoxManySelect.Controls.Add(linkLabelSelectAll);
		groupBoxManySelect.Dock = DockStyle.Top;
		groupBoxManySelect.Location = new Point(0, 56);
		groupBoxManySelect.Name = "groupBoxManySelect";
		groupBoxManySelect.Size = new Size(716, 41);
		groupBoxManySelect.TabIndex = 4;
		groupBoxManySelect.TabStop = false;
		groupBoxManySelect.Text = "Множественный выбор";
		// 
		// linkLabelInvertSelect
		// 
		linkLabelInvertSelect.Anchor = AnchorStyles.Top | AnchorStyles.Right;
		linkLabelInvertSelect.AutoSize = true;
		linkLabelInvertSelect.Location = new Point(580, 19);
		linkLabelInvertSelect.Name = "linkLabelInvertSelect";
		linkLabelInvertSelect.Size = new Size(130, 15);
		linkLabelInvertSelect.TabIndex = 1;
		linkLabelInvertSelect.TabStop = true;
		linkLabelInvertSelect.Text = "Инвертировать выбор";
		linkLabelInvertSelect.LinkClicked += LinkLabelInvertSelect_LinkClicked;
		// 
		// linkLabelSelectAll
		// 
		linkLabelSelectAll.AutoSize = true;
		linkLabelSelectAll.Location = new Point(6, 19);
		linkLabelSelectAll.Name = "linkLabelSelectAll";
		linkLabelSelectAll.Size = new Size(75, 15);
		linkLabelSelectAll.TabIndex = 0;
		linkLabelSelectAll.TabStop = true;
		linkLabelSelectAll.Text = "Выбрать все";
		linkLabelSelectAll.LinkClicked += LinkLabelSelectAll_LinkClicked;
		// 
		// groupBoxShowTable
		// 
		groupBoxShowTable.Controls.Add(radioButtonShowTablesCreate);
		groupBoxShowTable.Controls.Add(radioButtonShowTablesAll);
		groupBoxShowTable.Dock = DockStyle.Top;
		groupBoxShowTable.Enabled = false;
		groupBoxShowTable.Location = new Point(0, 15);
		groupBoxShowTable.Name = "groupBoxShowTable";
		groupBoxShowTable.Size = new Size(716, 41);
		groupBoxShowTable.TabIndex = 3;
		groupBoxShowTable.TabStop = false;
		groupBoxShowTable.Text = "Показывать таблицы:";
		// 
		// radioButtonShowTablesCreate
		// 
		radioButtonShowTablesCreate.AutoSize = true;
		radioButtonShowTablesCreate.Checked = true;
		radioButtonShowTablesCreate.Location = new Point(56, 16);
		radioButtonShowTablesCreate.Name = "radioButtonShowTablesCreate";
		radioButtonShowTablesCreate.Size = new Size(117, 19);
		radioButtonShowTablesCreate.TabIndex = 1;
		radioButtonShowTablesCreate.TabStop = true;
		radioButtonShowTablesCreate.Text = "Только создание";
		radioButtonShowTablesCreate.UseVisualStyleBackColor = true;
		radioButtonShowTablesCreate.CheckedChanged += RadioButtonShowTables_CheckedChanged;
		// 
		// radioButtonShowTablesAll
		// 
		radioButtonShowTablesAll.AutoSize = true;
		radioButtonShowTablesAll.Location = new Point(6, 16);
		radioButtonShowTablesAll.Name = "radioButtonShowTablesAll";
		radioButtonShowTablesAll.Size = new Size(44, 19);
		radioButtonShowTablesAll.TabIndex = 0;
		radioButtonShowTablesAll.Text = "Все";
		radioButtonShowTablesAll.UseVisualStyleBackColor = true;
		radioButtonShowTablesAll.CheckedChanged += RadioButtonShowTables_CheckedChanged;
		// 
		// labelSourceElte
		// 
		labelSourceElte.BackColor = Color.FromArgb(255, 255, 192);
		labelSourceElte.Dock = DockStyle.Top;
		labelSourceElte.Location = new Point(0, 0);
		labelSourceElte.Name = "labelSourceElte";
		labelSourceElte.Size = new Size(716, 15);
		labelSourceElte.TabIndex = 2;
		labelSourceElte.Text = "Исходные элементы";
		labelSourceElte.TextAlign = ContentAlignment.TopCenter;
		// 
		// treeView
		// 
		treeView.Dock = DockStyle.Fill;
		treeView.Location = new Point(0, 15);
		treeView.Name = "treeView";
		treeView.Size = new Size(716, 132);
		treeView.TabIndex = 6;
		treeView.AfterSelect += TreeView_AfterSelect;
		// 
		// labelFkeys
		// 
		labelFkeys.BackColor = Color.FromArgb(255, 255, 192);
		labelFkeys.Dock = DockStyle.Top;
		labelFkeys.Location = new Point(0, 0);
		labelFkeys.Name = "labelFkeys";
		labelFkeys.Size = new Size(716, 15);
		labelFkeys.TabIndex = 5;
		labelFkeys.Text = "Зависимые элементы";
		labelFkeys.TextAlign = ContentAlignment.TopCenter;
		// 
		// splitContainerEltText
		// 
		splitContainerEltText.Dock = DockStyle.Fill;
		splitContainerEltText.Location = new Point(3, 3);
		splitContainerEltText.Name = "splitContainerEltText";
		splitContainerEltText.Orientation = Orientation.Horizontal;
		// 
		// splitContainerEltText.Panel1
		// 
		splitContainerEltText.Panel1.Controls.Add(splitContainerEltAll);
		// 
		// splitContainerEltText.Panel2
		// 
		splitContainerEltText.Panel2.Controls.Add(textBoxContent);
		splitContainerEltText.Size = new Size(1031, 521);
		splitContainerEltText.SplitterDistance = 360;
		splitContainerEltText.TabIndex = 5;
		// 
		// textBoxContent
		// 
		textBoxContent.Dock = DockStyle.Fill;
		textBoxContent.Location = new Point(0, 0);
		textBoxContent.Multiline = true;
		textBoxContent.Name = "textBoxContent";
		textBoxContent.ReadOnly = true;
		textBoxContent.ScrollBars = ScrollBars.Both;
		textBoxContent.Size = new Size(1031, 157);
		textBoxContent.TabIndex = 0;
		// 
		// tabControlMain
		// 
		tabControlMain.Controls.Add(tabPageToDatabases);
		tabControlMain.Controls.Add(tabPageProcedureText);
		tabControlMain.Controls.Add(tabPageResult);
		tabControlMain.Dock = DockStyle.Fill;
		tabControlMain.Location = new Point(0, 34);
		tabControlMain.Name = "tabControlMain";
		tabControlMain.SelectedIndex = 0;
		tabControlMain.Size = new Size(1045, 555);
		tabControlMain.TabIndex = 6;
		// 
		// tabPageToDatabases
		// 
		tabPageToDatabases.Controls.Add(splitContainerEltText);
		tabPageToDatabases.Location = new Point(4, 24);
		tabPageToDatabases.Name = "tabPageToDatabases";
		tabPageToDatabases.Padding = new Padding(3);
		tabPageToDatabases.Size = new Size(1037, 527);
		tabPageToDatabases.TabIndex = 0;
		tabPageToDatabases.Text = "Распределение по базам данных";
		tabPageToDatabases.UseVisualStyleBackColor = true;
		// 
		// tabPageProcedureText
		// 
		tabPageProcedureText.Location = new Point(4, 24);
		tabPageProcedureText.Name = "tabPageProcedureText";
		tabPageProcedureText.Padding = new Padding(3);
		tabPageProcedureText.Size = new Size(1037, 527);
		tabPageProcedureText.TabIndex = 1;
		tabPageProcedureText.Text = "Доработка текстов процедур";
		tabPageProcedureText.UseVisualStyleBackColor = true;
		// 
		// tabPageResult
		// 
		tabPageResult.Controls.Add(groupBoxResultFilter);
		tabPageResult.Controls.Add(labelResultTree);
		tabPageResult.Location = new Point(4, 24);
		tabPageResult.Name = "tabPageResult";
		tabPageResult.Size = new Size(1037, 527);
		tabPageResult.TabIndex = 2;
		tabPageResult.Text = "Результат";
		tabPageResult.UseVisualStyleBackColor = true;
		// 
		// groupBoxResultFilter
		// 
		groupBoxResultFilter.Controls.Add(radioButtonResultOk);
		groupBoxResultFilter.Controls.Add(radioButtonResultAlert);
		groupBoxResultFilter.Controls.Add(radioButtonResultShowAll);
		groupBoxResultFilter.Dock = DockStyle.Top;
		groupBoxResultFilter.Location = new Point(0, 15);
		groupBoxResultFilter.Name = "groupBoxResultFilter";
		groupBoxResultFilter.Size = new Size(1037, 41);
		groupBoxResultFilter.TabIndex = 4;
		groupBoxResultFilter.TabStop = false;
		groupBoxResultFilter.Text = "Показать результат";
		// 
		// radioButtonResultOk
		// 
		radioButtonResultOk.AutoSize = true;
		radioButtonResultOk.Dock = DockStyle.Left;
		radioButtonResultOk.Location = new Point(196, 19);
		radioButtonResultOk.Name = "radioButtonResultOk";
		radioButtonResultOk.Size = new Size(106, 19);
		radioButtonResultOk.TabIndex = 3;
		radioButtonResultOk.Text = "Утверждённые";
		radioButtonResultOk.UseVisualStyleBackColor = true;
		// 
		// radioButtonResultAlert
		// 
		radioButtonResultAlert.AutoSize = true;
		radioButtonResultAlert.Dock = DockStyle.Left;
		radioButtonResultAlert.Location = new Point(47, 19);
		radioButtonResultAlert.Name = "radioButtonResultAlert";
		radioButtonResultAlert.Size = new Size(149, 19);
		radioButtonResultAlert.TabIndex = 2;
		radioButtonResultAlert.Text = "Требующие внимания";
		radioButtonResultAlert.UseVisualStyleBackColor = true;
		// 
		// radioButtonResultShowAll
		// 
		radioButtonResultShowAll.AutoSize = true;
		radioButtonResultShowAll.Checked = true;
		radioButtonResultShowAll.Dock = DockStyle.Left;
		radioButtonResultShowAll.Location = new Point(3, 19);
		radioButtonResultShowAll.Name = "radioButtonResultShowAll";
		radioButtonResultShowAll.Size = new Size(44, 19);
		radioButtonResultShowAll.TabIndex = 1;
		radioButtonResultShowAll.TabStop = true;
		radioButtonResultShowAll.Text = "Все";
		radioButtonResultShowAll.UseVisualStyleBackColor = true;
		// 
		// labelResultTree
		// 
		labelResultTree.BackColor = Color.FromArgb(192, 255, 192);
		labelResultTree.Dock = DockStyle.Top;
		labelResultTree.Location = new Point(0, 0);
		labelResultTree.Name = "labelResultTree";
		labelResultTree.Size = new Size(1037, 15);
		labelResultTree.TabIndex = 3;
		labelResultTree.Text = "Результат (порядок важен)";
		labelResultTree.TextAlign = ContentAlignment.TopCenter;
		// 
		// FormMain
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(1045, 589);
		Controls.Add(tabControlMain);
		Controls.Add(panelTop);
		Name = "FormMain";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "Преобразование из MS SQL в PostgreSQL";
		panelTop.ResumeLayout(false);
		panelTop.PerformLayout();
		splitContainerEltAll.Panel1.ResumeLayout(false);
		splitContainerEltAll.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)splitContainerEltAll).EndInit();
		splitContainerEltAll.ResumeLayout(false);
		splitContainerCentral.Panel1.ResumeLayout(false);
		splitContainerCentral.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)splitContainerCentral).EndInit();
		splitContainerCentral.ResumeLayout(false);
		groupBoxNewDatabases.ResumeLayout(false);
		groupBoxNewDatabases.PerformLayout();
		splitContainerSource.Panel1.ResumeLayout(false);
		splitContainerSource.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)splitContainerSource).EndInit();
		splitContainerSource.ResumeLayout(false);
		groupBoxManySelect.ResumeLayout(false);
		groupBoxManySelect.PerformLayout();
		groupBoxShowTable.ResumeLayout(false);
		groupBoxShowTable.PerformLayout();
		splitContainerEltText.Panel1.ResumeLayout(false);
		splitContainerEltText.Panel2.ResumeLayout(false);
		splitContainerEltText.Panel2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)splitContainerEltText).EndInit();
		splitContainerEltText.ResumeLayout(false);
		tabControlMain.ResumeLayout(false);
		tabPageToDatabases.ResumeLayout(false);
		tabPageResult.ResumeLayout(false);
		groupBoxResultFilter.ResumeLayout(false);
		groupBoxResultFilter.PerformLayout();
		ResumeLayout(false);
	}

	#endregion

	private Panel panelTop;
	private Button buttonSave;
	private Button buttonCreate;
	private Button buttonLoad;
	private SplitContainer splitContainerEltAll;
	private Button buttonSetup;
	private SplitContainer splitContainerEltText;
	private TextBox textBoxContent;
	private Label labelInfo;
	private SplitContainer splitContainerSource;
	private CheckedListBox checkedListBoxElements;
	private GroupBox groupBoxManySelect;
	private LinkLabel linkLabelInvertSelect;
	private LinkLabel linkLabelSelectAll;
	private GroupBox groupBoxShowTable;
	private RadioButton radioButtonShowTablesCreate;
	private RadioButton radioButtonShowTablesAll;
	private Label labelSourceElte;
	private TreeView treeView;
	private Label labelFkeys;
	private SplitContainer splitContainerCentral;
	private GroupBox groupBoxNewDatabases;
	private RadioButton radioButtonNoDatabase;
	private Button buttonDelete;
	private Button buttonAdd;
	private GroupBox groupBoxCheckElmType;
	private TabControl tabControlMain;
	private TabPage tabPageToDatabases;
	private TabPage tabPageProcedureText;
	private TabPage tabPageResult;
	private GroupBox groupBoxResultFilter;
	private RadioButton radioButtonResultOk;
	private RadioButton radioButtonResultAlert;
	private RadioButton radioButtonResultShowAll;
	private Label labelResultTree;
}