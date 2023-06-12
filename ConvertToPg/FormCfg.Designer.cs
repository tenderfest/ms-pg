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
			panelButtons = new Panel();
			buttonAddDatabase = new Button();
			buttonSave = new Button();
			buttonCancel = new Button();
			groupBoxTargetDatabases = new GroupBox();
			groupBoxSkipOperations = new GroupBox();
			textBoxSkipOperations = new TextBox();
			splitContainerSkips = new SplitContainer();
			groupBoxSkipElement = new GroupBox();
			textBoxSkipElement = new TextBox();
			panelButtons.SuspendLayout();
			groupBoxSkipOperations.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)splitContainerSkips).BeginInit();
			splitContainerSkips.Panel1.SuspendLayout();
			splitContainerSkips.Panel2.SuspendLayout();
			splitContainerSkips.SuspendLayout();
			groupBoxSkipElement.SuspendLayout();
			SuspendLayout();
			// 
			// panelButtons
			// 
			panelButtons.Controls.Add(buttonAddDatabase);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Controls.Add(buttonCancel);
			panelButtons.Dock = DockStyle.Top;
			panelButtons.Location = new Point(0, 0);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new Size(528, 24);
			panelButtons.TabIndex = 0;
			// 
			// buttonAddDatabase
			// 
			buttonAddDatabase.DialogResult = DialogResult.OK;
			buttonAddDatabase.Dock = DockStyle.Right;
			buttonAddDatabase.Location = new Point(356, 0);
			buttonAddDatabase.Name = "buttonAddDatabase";
			buttonAddDatabase.Size = new Size(97, 24);
			buttonAddDatabase.TabIndex = 2;
			buttonAddDatabase.Text = "Добавить БД";
			buttonAddDatabase.UseVisualStyleBackColor = true;
			buttonAddDatabase.Click += ButtonAddDatabase_Click;
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
			// groupBoxTargetDatabases
			// 
			groupBoxTargetDatabases.Dock = DockStyle.Top;
			groupBoxTargetDatabases.Location = new Point(0, 24);
			groupBoxTargetDatabases.Name = "groupBoxTargetDatabases";
			groupBoxTargetDatabases.Size = new Size(528, 19);
			groupBoxTargetDatabases.TabIndex = 1;
			groupBoxTargetDatabases.TabStop = false;
			groupBoxTargetDatabases.Text = "Целевые базы данных";
			// 
			// groupBoxSkipOperations
			// 
			groupBoxSkipOperations.Controls.Add(textBoxSkipOperations);
			groupBoxSkipOperations.Dock = DockStyle.Fill;
			groupBoxSkipOperations.Location = new Point(0, 0);
			groupBoxSkipOperations.Name = "groupBoxSkipOperations";
			groupBoxSkipOperations.Size = new Size(252, 265);
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
			textBoxSkipOperations.Size = new Size(246, 243);
			textBoxSkipOperations.TabIndex = 0;
			// 
			// splitContainerSkips
			// 
			splitContainerSkips.Dock = DockStyle.Fill;
			splitContainerSkips.Location = new Point(0, 43);
			splitContainerSkips.Name = "splitContainerSkips";
			// 
			// splitContainerSkips.Panel1
			// 
			splitContainerSkips.Panel1.Controls.Add(groupBoxSkipOperations);
			// 
			// splitContainerSkips.Panel2
			// 
			splitContainerSkips.Panel2.Controls.Add(groupBoxSkipElement);
			splitContainerSkips.Size = new Size(528, 265);
			splitContainerSkips.SplitterDistance = 252;
			splitContainerSkips.TabIndex = 3;
			// 
			// groupBoxSkipElement
			// 
			groupBoxSkipElement.Controls.Add(textBoxSkipElement);
			groupBoxSkipElement.Dock = DockStyle.Fill;
			groupBoxSkipElement.Location = new Point(0, 0);
			groupBoxSkipElement.Name = "groupBoxSkipElement";
			groupBoxSkipElement.Size = new Size(272, 265);
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
			textBoxSkipElement.Size = new Size(266, 243);
			textBoxSkipElement.TabIndex = 0;
			// 
			// FormCfg
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			CancelButton = buttonCancel;
			ClientSize = new Size(528, 308);
			Controls.Add(splitContainerSkips);
			Controls.Add(groupBoxTargetDatabases);
			Controls.Add(panelButtons);
			Name = "FormCfg";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Настройки";
			Load += FormCfg_Load;
			panelButtons.ResumeLayout(false);
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

		private Panel panelButtons;
		private Button buttonSave;
		private Button buttonCancel;
		private GroupBox groupBoxTargetDatabases;
		private GroupBox groupBoxSkipOperations;
		private TextBox textBoxSkipOperations;
		private SplitContainer splitContainerSkips;
		private GroupBox groupBoxSkipElement;
		private TextBox textBoxSkipElement;
		private Button buttonAddDatabase;
	}
}