namespace ConvertToPg
{
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
			buttonSave = new Button();
			buttonDelete = new Button();
			buttonAdd = new Button();
			buttonCreate = new Button();
			buttonSetup = new Button();
			buttonLoad = new Button();
			splitContainerEltAll = new SplitContainer();
			splitContainerEltSelect = new SplitContainer();
			checkedListBoxTable = new CheckedListBox();
			labelSourceElte = new Label();
			checkedListBoxFkey = new CheckedListBox();
			labelFkeys = new Label();
			treeView = new TreeView();
			labelResultTree = new Label();
			splitContainerEltText = new SplitContainer();
			textBoxContent = new TextBox();
			splitContainerLeft = new SplitContainer();
			groupBoxNewDatabases = new GroupBox();
			radioButtonNone = new RadioButton();
			groupBoxCheckElmType = new GroupBox();
			panelTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)splitContainerEltAll).BeginInit();
			splitContainerEltAll.Panel1.SuspendLayout();
			splitContainerEltAll.Panel2.SuspendLayout();
			splitContainerEltAll.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)splitContainerEltSelect).BeginInit();
			splitContainerEltSelect.Panel1.SuspendLayout();
			splitContainerEltSelect.Panel2.SuspendLayout();
			splitContainerEltSelect.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)splitContainerEltText).BeginInit();
			splitContainerEltText.Panel1.SuspendLayout();
			splitContainerEltText.Panel2.SuspendLayout();
			splitContainerEltText.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)splitContainerLeft).BeginInit();
			splitContainerLeft.Panel1.SuspendLayout();
			splitContainerLeft.Panel2.SuspendLayout();
			splitContainerLeft.SuspendLayout();
			groupBoxNewDatabases.SuspendLayout();
			SuspendLayout();
			// 
			// panelTop
			// 
			panelTop.Controls.Add(buttonSave);
			panelTop.Controls.Add(buttonDelete);
			panelTop.Controls.Add(buttonAdd);
			panelTop.Controls.Add(buttonCreate);
			panelTop.Controls.Add(buttonSetup);
			panelTop.Controls.Add(buttonLoad);
			panelTop.Dock = DockStyle.Top;
			panelTop.Location = new Point(0, 0);
			panelTop.Name = "panelTop";
			panelTop.Size = new Size(1045, 34);
			panelTop.TabIndex = 0;
			// 
			// buttonSave
			// 
			buttonSave.Dock = DockStyle.Fill;
			buttonSave.Enabled = false;
			buttonSave.Location = new Point(283, 0);
			buttonSave.Name = "buttonSave";
			buttonSave.Size = new Size(647, 34);
			buttonSave.TabIndex = 2;
			buttonSave.Text = "Сохранить";
			buttonSave.UseVisualStyleBackColor = true;
			buttonSave.Click += ButtonSave_Click;
			// 
			// buttonDelete
			// 
			buttonDelete.Dock = DockStyle.Left;
			buttonDelete.Enabled = false;
			buttonDelete.ForeColor = Color.Red;
			buttonDelete.Location = new Point(216, 0);
			buttonDelete.Name = "buttonDelete";
			buttonDelete.Size = new Size(67, 34);
			buttonDelete.TabIndex = 5;
			buttonDelete.Text = "Удалить";
			buttonDelete.UseVisualStyleBackColor = true;
			// 
			// buttonAdd
			// 
			buttonAdd.Dock = DockStyle.Left;
			buttonAdd.Enabled = false;
			buttonAdd.ForeColor = Color.Green;
			buttonAdd.Location = new Point(149, 0);
			buttonAdd.Name = "buttonAdd";
			buttonAdd.Size = new Size(67, 34);
			buttonAdd.TabIndex = 4;
			buttonAdd.Text = "Добавить";
			buttonAdd.UseVisualStyleBackColor = true;
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
			splitContainerEltAll.Panel1.Controls.Add(splitContainerEltSelect);
			// 
			// splitContainerEltAll.Panel2
			// 
			splitContainerEltAll.Panel2.Controls.Add(treeView);
			splitContainerEltAll.Panel2.Controls.Add(labelResultTree);
			splitContainerEltAll.Size = new Size(895, 409);
			splitContainerEltAll.SplitterDistance = 585;
			splitContainerEltAll.TabIndex = 1;
			// 
			// splitContainerEltSelect
			// 
			splitContainerEltSelect.Dock = DockStyle.Fill;
			splitContainerEltSelect.Location = new Point(0, 0);
			splitContainerEltSelect.Name = "splitContainerEltSelect";
			// 
			// splitContainerEltSelect.Panel1
			// 
			splitContainerEltSelect.Panel1.Controls.Add(checkedListBoxTable);
			splitContainerEltSelect.Panel1.Controls.Add(labelSourceElte);
			// 
			// splitContainerEltSelect.Panel2
			// 
			splitContainerEltSelect.Panel2.Controls.Add(checkedListBoxFkey);
			splitContainerEltSelect.Panel2.Controls.Add(labelFkeys);
			splitContainerEltSelect.Size = new Size(585, 409);
			splitContainerEltSelect.SplitterDistance = 283;
			splitContainerEltSelect.TabIndex = 0;
			// 
			// checkedListBoxTable
			// 
			checkedListBoxTable.Dock = DockStyle.Fill;
			checkedListBoxTable.FormattingEnabled = true;
			checkedListBoxTable.Location = new Point(0, 15);
			checkedListBoxTable.Name = "checkedListBoxTable";
			checkedListBoxTable.Size = new Size(283, 394);
			checkedListBoxTable.TabIndex = 0;
			checkedListBoxTable.SelectedValueChanged += CheckedListBoxTable_SelectedValueChanged;
			// 
			// labelSourceElte
			// 
			labelSourceElte.BackColor = Color.FromArgb(255, 255, 192);
			labelSourceElte.Dock = DockStyle.Top;
			labelSourceElte.Location = new Point(0, 0);
			labelSourceElte.Name = "labelSourceElte";
			labelSourceElte.Size = new Size(283, 15);
			labelSourceElte.TabIndex = 1;
			labelSourceElte.Text = "Исходные элементы";
			labelSourceElte.TextAlign = ContentAlignment.TopCenter;
			// 
			// checkedListBoxFkey
			// 
			checkedListBoxFkey.Dock = DockStyle.Fill;
			checkedListBoxFkey.FormattingEnabled = true;
			checkedListBoxFkey.Location = new Point(0, 15);
			checkedListBoxFkey.Name = "checkedListBoxFkey";
			checkedListBoxFkey.Size = new Size(298, 394);
			checkedListBoxFkey.TabIndex = 0;
			// 
			// labelFkeys
			// 
			labelFkeys.BackColor = Color.FromArgb(255, 255, 192);
			labelFkeys.Dock = DockStyle.Top;
			labelFkeys.Location = new Point(0, 0);
			labelFkeys.Name = "labelFkeys";
			labelFkeys.Size = new Size(298, 15);
			labelFkeys.TabIndex = 2;
			labelFkeys.Text = "Зависимые элементы";
			labelFkeys.TextAlign = ContentAlignment.TopCenter;
			// 
			// treeView
			// 
			treeView.Dock = DockStyle.Fill;
			treeView.Location = new Point(0, 15);
			treeView.Name = "treeView";
			treeView.Size = new Size(306, 394);
			treeView.TabIndex = 0;
			treeView.AfterSelect += TreeView_AfterSelect;
			// 
			// labelResultTree
			// 
			labelResultTree.BackColor = Color.FromArgb(192, 255, 192);
			labelResultTree.Dock = DockStyle.Top;
			labelResultTree.Location = new Point(0, 0);
			labelResultTree.Name = "labelResultTree";
			labelResultTree.Size = new Size(306, 15);
			labelResultTree.TabIndex = 2;
			labelResultTree.Text = "Результат";
			labelResultTree.TextAlign = ContentAlignment.TopCenter;
			// 
			// splitContainerEltText
			// 
			splitContainerEltText.Dock = DockStyle.Fill;
			splitContainerEltText.Location = new Point(150, 34);
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
			splitContainerEltText.Size = new Size(895, 591);
			splitContainerEltText.SplitterDistance = 409;
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
			textBoxContent.Size = new Size(895, 178);
			textBoxContent.TabIndex = 0;
			// 
			// splitContainerLeft
			// 
			splitContainerLeft.Dock = DockStyle.Left;
			splitContainerLeft.Location = new Point(0, 34);
			splitContainerLeft.Name = "splitContainerLeft";
			splitContainerLeft.Orientation = Orientation.Horizontal;
			// 
			// splitContainerLeft.Panel1
			// 
			splitContainerLeft.Panel1.Controls.Add(groupBoxNewDatabases);
			// 
			// splitContainerLeft.Panel2
			// 
			splitContainerLeft.Panel2.Controls.Add(groupBoxCheckElmType);
			splitContainerLeft.Size = new Size(150, 591);
			splitContainerLeft.SplitterDistance = 295;
			splitContainerLeft.TabIndex = 6;
			// 
			// groupBoxNewDatabases
			// 
			groupBoxNewDatabases.Controls.Add(radioButtonNone);
			groupBoxNewDatabases.Dock = DockStyle.Fill;
			groupBoxNewDatabases.ForeColor = SystemColors.ControlText;
			groupBoxNewDatabases.Location = new Point(0, 0);
			groupBoxNewDatabases.Name = "groupBoxNewDatabases";
			groupBoxNewDatabases.Size = new Size(150, 295);
			groupBoxNewDatabases.TabIndex = 4;
			groupBoxNewDatabases.TabStop = false;
			groupBoxNewDatabases.Text = "Новые базы данных";
			// 
			// radioButtonNone
			// 
			radioButtonNone.AutoSize = true;
			radioButtonNone.Checked = true;
			radioButtonNone.Dock = DockStyle.Top;
			radioButtonNone.ForeColor = SystemColors.ControlText;
			radioButtonNone.Location = new Point(3, 19);
			radioButtonNone.Name = "radioButtonNone";
			radioButtonNone.Size = new Size(144, 19);
			radioButtonNone.TabIndex = 7;
			radioButtonNone.TabStop = true;
			radioButtonNone.Text = "Неопределен";
			radioButtonNone.UseVisualStyleBackColor = true;
			radioButtonNone.CheckedChanged += RadioButtonNone_CheckedChanged;
			// 
			// groupBoxCheckElmType
			// 
			groupBoxCheckElmType.Dock = DockStyle.Fill;
			groupBoxCheckElmType.Enabled = false;
			groupBoxCheckElmType.ForeColor = SystemColors.ControlText;
			groupBoxCheckElmType.Location = new Point(0, 0);
			groupBoxCheckElmType.Name = "groupBoxCheckElmType";
			groupBoxCheckElmType.Size = new Size(150, 292);
			groupBoxCheckElmType.TabIndex = 3;
			groupBoxCheckElmType.TabStop = false;
			groupBoxCheckElmType.Text = "Фильтр";
			// 
			// FormMain
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1045, 625);
			Controls.Add(splitContainerEltText);
			Controls.Add(splitContainerLeft);
			Controls.Add(panelTop);
			Name = "FormMain";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Преобразование из MS SQL в PostgreSQL";
			panelTop.ResumeLayout(false);
			splitContainerEltAll.Panel1.ResumeLayout(false);
			splitContainerEltAll.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainerEltAll).EndInit();
			splitContainerEltAll.ResumeLayout(false);
			splitContainerEltSelect.Panel1.ResumeLayout(false);
			splitContainerEltSelect.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainerEltSelect).EndInit();
			splitContainerEltSelect.ResumeLayout(false);
			splitContainerEltText.Panel1.ResumeLayout(false);
			splitContainerEltText.Panel2.ResumeLayout(false);
			splitContainerEltText.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)splitContainerEltText).EndInit();
			splitContainerEltText.ResumeLayout(false);
			splitContainerLeft.Panel1.ResumeLayout(false);
			splitContainerLeft.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainerLeft).EndInit();
			splitContainerLeft.ResumeLayout(false);
			groupBoxNewDatabases.ResumeLayout(false);
			groupBoxNewDatabases.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private Panel panelTop;
		private Button buttonSave;
		private Button buttonCreate;
		private Button buttonLoad;
		private SplitContainer splitContainerEltAll;
		private SplitContainer splitContainerEltSelect;
		private CheckedListBox checkedListBoxTable;
		private CheckedListBox checkedListBoxFkey;
		private TreeView treeView;
		private Button buttonSetup;
		private Button buttonDelete;
		private Button buttonAdd;
		private SplitContainer splitContainerEltText;
		private TextBox textBoxContent;
		private Label labelSourceElte;
		private Label labelFkeys;
		private Label labelResultTree;
		private SplitContainer splitContainerLeft;
		private GroupBox groupBoxNewDatabases;
		private RadioButton radioButtonNone;
		private GroupBox groupBoxCheckElmType;
	}
}