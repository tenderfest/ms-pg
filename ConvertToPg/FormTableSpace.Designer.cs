namespace ConvertToPg
{
	partial class FormTableSpace
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
			labelInfo = new Label();
			buttonSave = new Button();
			buttonCancel = new Button();
			textBoxTableSpace = new TextBox();
			panelButtons.SuspendLayout();
			SuspendLayout();
			// 
			// panelButtons
			// 
			panelButtons.Controls.Add(labelInfo);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Controls.Add(buttonCancel);
			panelButtons.Dock = DockStyle.Top;
			panelButtons.Location = new Point(0, 0);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new Size(800, 24);
			panelButtons.TabIndex = 1;
			// 
			// labelInfo
			// 
			labelInfo.Dock = DockStyle.Fill;
			labelInfo.Location = new Point(75, 0);
			labelInfo.Name = "labelInfo";
			labelInfo.Size = new Size(650, 24);
			labelInfo.TabIndex = 2;
			labelInfo.Text = "Укажите путь до табличного пространства в файловой системе сервера баз данных";
			labelInfo.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// buttonSave
			// 
			buttonSave.DialogResult = DialogResult.OK;
			buttonSave.Dock = DockStyle.Right;
			buttonSave.Enabled = false;
			buttonSave.Location = new Point(725, 0);
			buttonSave.Name = "buttonSave";
			buttonSave.Size = new Size(75, 24);
			buttonSave.TabIndex = 0;
			buttonSave.Text = "Сохранить";
			buttonSave.UseVisualStyleBackColor = true;
			// 
			// buttonCancel
			// 
			buttonCancel.DialogResult = DialogResult.Cancel;
			buttonCancel.Dock = DockStyle.Left;
			buttonCancel.Location = new Point(0, 0);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new Size(75, 24);
			buttonCancel.TabIndex = 1;
			buttonCancel.Text = "Отменить";
			buttonCancel.UseVisualStyleBackColor = true;
			// 
			// textBoxTableSpace
			// 
			textBoxTableSpace.Dock = DockStyle.Top;
			textBoxTableSpace.Location = new Point(0, 24);
			textBoxTableSpace.Name = "textBoxTableSpace";
			textBoxTableSpace.Size = new Size(800, 23);
			textBoxTableSpace.TabIndex = 0;
			textBoxTableSpace.TextChanged += TextBoxTableSpace_TextChanged;
			// 
			// FormTableSpace
			// 
			AcceptButton = buttonSave;
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			CancelButton = buttonCancel;
			ClientSize = new Size(800, 49);
			ControlBox = false;
			Controls.Add(textBoxTableSpace);
			Controls.Add(panelButtons);
			FormBorderStyle = FormBorderStyle.Fixed3D;
			Name = "FormTableSpace";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Расположение табличного пространства";
			panelButtons.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Panel panelButtons;
		private Label labelInfo;
		private Button buttonSave;
		private Button buttonCancel;
		private TextBox textBoxTableSpace;
	}
}