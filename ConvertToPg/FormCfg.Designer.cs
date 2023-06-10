namespace ConvertToPg
{
	partial class FormCfg
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			panel1 = new Panel();
			buttonSave = new Button();
			buttonCancel = new Button();
			groupBoxConnectionStrings = new GroupBox();
			textBoxPathToAct = new TextBox();
			labelPathToAct = new Label();
			textBoxPathToArc = new TextBox();
			labelPathToArc = new Label();
			textBoxPathToDic = new TextBox();
			labelPathToDic = new Label();
			groupBoxSkipOperations = new GroupBox();
			textBoxSkipOperations = new TextBox();
			splitContainerSkips = new SplitContainer();
			groupBoxSkipElement = new GroupBox();
			textBoxSkipElement = new TextBox();
			panel1.SuspendLayout();
			groupBoxConnectionStrings.SuspendLayout();
			groupBoxSkipOperations.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)splitContainerSkips).BeginInit();
			splitContainerSkips.Panel1.SuspendLayout();
			splitContainerSkips.Panel2.SuspendLayout();
			splitContainerSkips.SuspendLayout();
			groupBoxSkipElement.SuspendLayout();
			SuspendLayout();
			// 
			// panel1
			// 
			panel1.Controls.Add(buttonSave);
			panel1.Controls.Add(buttonCancel);
			panel1.Dock = DockStyle.Top;
			panel1.Location = new Point(0, 0);
			panel1.Name = "panel1";
			panel1.Size = new Size(528, 24);
			panel1.TabIndex = 0;
			// 
			// buttonSave
			// 
			buttonSave.DialogResult = DialogResult.OK;
			buttonSave.Dock = DockStyle.Right;
			buttonSave.Location = new Point(453, 0);
			buttonSave.Name = "buttonSave";
			buttonSave.Size = new Size(75, 24);
			buttonSave.TabIndex = 1;
			buttonSave.Text = "Сохранить";
			buttonSave.UseVisualStyleBackColor = true;
			buttonSave.Click += ButtonSave_Click;
			// 
			// buttonCancel
			// 
			buttonCancel.DialogResult = DialogResult.Cancel;
			buttonCancel.Dock = DockStyle.Left;
			buttonCancel.Location = new Point(0, 0);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new Size(75, 24);
			buttonCancel.TabIndex = 0;
			buttonCancel.Text = "Отменить";
			buttonCancel.UseVisualStyleBackColor = true;
			// 
			// groupBoxConnectionStrings
			// 
			groupBoxConnectionStrings.Controls.Add(textBoxPathToAct);
			groupBoxConnectionStrings.Controls.Add(labelPathToAct);
			groupBoxConnectionStrings.Controls.Add(textBoxPathToArc);
			groupBoxConnectionStrings.Controls.Add(labelPathToArc);
			groupBoxConnectionStrings.Controls.Add(textBoxPathToDic);
			groupBoxConnectionStrings.Controls.Add(labelPathToDic);
			groupBoxConnectionStrings.Dock = DockStyle.Top;
			groupBoxConnectionStrings.Location = new Point(0, 24);
			groupBoxConnectionStrings.Name = "groupBoxConnectionStrings";
			groupBoxConnectionStrings.Size = new Size(528, 107);
			groupBoxConnectionStrings.TabIndex = 1;
			groupBoxConnectionStrings.TabStop = false;
			groupBoxConnectionStrings.Text = "Строки подключения к БД";
			// 
			// textBoxPathToAct
			// 
			textBoxPathToAct.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			textBoxPathToAct.Location = new Point(91, 45);
			textBoxPathToAct.Name = "textBoxPathToAct";
			textBoxPathToAct.Size = new Size(431, 23);
			textBoxPathToAct.TabIndex = 5;
			textBoxPathToAct.TextChanged += TextBoxPathToAct_TextChanged;
			// 
			// labelPathToAct
			// 
			labelPathToAct.AutoSize = true;
			labelPathToAct.Location = new Point(12, 48);
			labelPathToAct.Name = "labelPathToAct";
			labelPathToAct.Size = new Size(73, 15);
			labelPathToAct.TabIndex = 4;
			labelPathToAct.Text = "Актуальная:";
			// 
			// textBoxPathToArc
			// 
			textBoxPathToArc.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			textBoxPathToArc.Location = new Point(91, 74);
			textBoxPathToArc.Name = "textBoxPathToArc";
			textBoxPathToArc.Size = new Size(431, 23);
			textBoxPathToArc.TabIndex = 7;
			textBoxPathToArc.TextChanged += TextBoxPathToArc_TextChanged;
			// 
			// labelPathToArc
			// 
			labelPathToArc.AutoSize = true;
			labelPathToArc.Location = new Point(12, 77);
			labelPathToArc.Name = "labelPathToArc";
			labelPathToArc.Size = new Size(44, 15);
			labelPathToArc.TabIndex = 6;
			labelPathToArc.Text = "Архив:";
			// 
			// textBoxPathToDic
			// 
			textBoxPathToDic.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			textBoxPathToDic.Location = new Point(91, 16);
			textBoxPathToDic.Name = "textBoxPathToDic";
			textBoxPathToDic.Size = new Size(431, 23);
			textBoxPathToDic.TabIndex = 3;
			textBoxPathToDic.TextChanged += TextBoxPathToPg_TextChanged;
			// 
			// labelPathToDic
			// 
			labelPathToDic.AutoSize = true;
			labelPathToDic.Location = new Point(12, 19);
			labelPathToDic.Name = "labelPathToDic";
			labelPathToDic.Size = new Size(57, 15);
			labelPathToDic.TabIndex = 2;
			labelPathToDic.Text = "Словарь:";
			// 
			// groupBoxSkipOperations
			// 
			groupBoxSkipOperations.Controls.Add(textBoxSkipOperations);
			groupBoxSkipOperations.Dock = DockStyle.Fill;
			groupBoxSkipOperations.Location = new Point(0, 0);
			groupBoxSkipOperations.Name = "groupBoxSkipOperations";
			groupBoxSkipOperations.Size = new Size(252, 172);
			groupBoxSkipOperations.TabIndex = 2;
			groupBoxSkipOperations.TabStop = false;
			groupBoxSkipOperations.Text = "Пропускаемые операции";
			// 
			// textBoxSkipOperations
			// 
			textBoxSkipOperations.Dock = DockStyle.Fill;
			textBoxSkipOperations.Location = new Point(3, 19);
			textBoxSkipOperations.Multiline = true;
			textBoxSkipOperations.Name = "textBoxSkipOperations";
			textBoxSkipOperations.Size = new Size(246, 150);
			textBoxSkipOperations.TabIndex = 0;
			// 
			// splitContainerSkips
			// 
			splitContainerSkips.Dock = DockStyle.Fill;
			splitContainerSkips.Location = new Point(0, 131);
			splitContainerSkips.Name = "splitContainerSkips";
			// 
			// splitContainerSkips.Panel1
			// 
			splitContainerSkips.Panel1.Controls.Add(groupBoxSkipOperations);
			// 
			// splitContainerSkips.Panel2
			// 
			splitContainerSkips.Panel2.Controls.Add(groupBoxSkipElement);
			splitContainerSkips.Size = new Size(528, 172);
			splitContainerSkips.SplitterDistance = 252;
			splitContainerSkips.TabIndex = 3;
			// 
			// groupBoxSkipElement
			// 
			groupBoxSkipElement.Controls.Add(textBoxSkipElement);
			groupBoxSkipElement.Dock = DockStyle.Fill;
			groupBoxSkipElement.Location = new Point(0, 0);
			groupBoxSkipElement.Name = "groupBoxSkipElement";
			groupBoxSkipElement.Size = new Size(272, 172);
			groupBoxSkipElement.TabIndex = 3;
			groupBoxSkipElement.TabStop = false;
			groupBoxSkipElement.Text = "Пропускаемые элементы";
			// 
			// textBoxSkipElement
			// 
			textBoxSkipElement.Dock = DockStyle.Fill;
			textBoxSkipElement.Location = new Point(3, 19);
			textBoxSkipElement.Multiline = true;
			textBoxSkipElement.Name = "textBoxSkipElement";
			textBoxSkipElement.Size = new Size(266, 150);
			textBoxSkipElement.TabIndex = 0;
			// 
			// FormCfg
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			CancelButton = buttonCancel;
			ClientSize = new Size(528, 303);
			Controls.Add(splitContainerSkips);
			Controls.Add(groupBoxConnectionStrings);
			Controls.Add(panel1);
			Name = "FormCfg";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Настройки";
			Load += FormCfg_Load;
			panel1.ResumeLayout(false);
			groupBoxConnectionStrings.ResumeLayout(false);
			groupBoxConnectionStrings.PerformLayout();
			groupBoxSkipOperations.ResumeLayout(false);
			groupBoxSkipOperations.PerformLayout();
			splitContainerSkips.Panel1.ResumeLayout(false);
			splitContainerSkips.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainerSkips).EndInit();
			splitContainerSkips.ResumeLayout(false);
			groupBoxSkipElement.ResumeLayout(false);
			groupBoxSkipElement.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private Panel panel1;
		private Button buttonSave;
		private Button buttonCancel;
		private GroupBox groupBoxConnectionStrings;
		private TextBox textBoxPathToDic;
		private Label labelPathToDic;
		private TextBox textBoxPathToAct;
		private Label labelPathToAct;
		private TextBox textBoxPathToArc;
		private Label labelPathToArc;
		private GroupBox groupBoxSkipOperations;
		private TextBox textBoxSkipOperations;
		private SplitContainer splitContainerSkips;
		private GroupBox groupBoxSkipElement;
		private TextBox textBoxSkipElement;
	}
}