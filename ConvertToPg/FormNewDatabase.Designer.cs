namespace ConvertToPg
{
	partial class FormNewDatabase
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
			buttonSave = new Button();
			buttonCancel = new Button();
			textBoxName = new TextBox();
			textBoxConnString = new TextBox();
			labelName = new Label();
			labelConnString = new Label();
			panelButtons.SuspendLayout();
			SuspendLayout();
			// 
			// panelButtons
			// 
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Controls.Add(buttonCancel);
			panelButtons.Dock = DockStyle.Top;
			panelButtons.Location = new Point(0, 0);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new Size(564, 24);
			panelButtons.TabIndex = 1;
			// 
			// buttonSave
			// 
			buttonSave.Dock = DockStyle.Right;
			buttonSave.Enabled = false;
			buttonSave.Location = new Point(489, 0);
			buttonSave.Name = "buttonSave";
			buttonSave.Size = new Size(75, 24);
			buttonSave.TabIndex = 1;
			buttonSave.Text = "Сохранить";
			buttonSave.UseVisualStyleBackColor = true;
			buttonSave.Click += ButtonSave_Click;
			// 
			// buttonCancel
			// 
			buttonCancel.Dock = DockStyle.Left;
			buttonCancel.Location = new Point(0, 0);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new Size(75, 24);
			buttonCancel.TabIndex = 0;
			buttonCancel.Text = "Отменить";
			buttonCancel.UseVisualStyleBackColor = true;
			buttonCancel.Click += ButtonCancel_Click;
			// 
			// textBoxName
			// 
			textBoxName.Location = new Point(143, 30);
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new Size(403, 23);
			textBoxName.TabIndex = 2;
			textBoxName.TextChanged += TextBoxName_TextChanged;
			// 
			// textBoxConnString
			// 
			textBoxConnString.Location = new Point(143, 59);
			textBoxConnString.Name = "textBoxConnString";
			textBoxConnString.Size = new Size(403, 23);
			textBoxConnString.TabIndex = 3;
			// 
			// labelName
			// 
			labelName.AutoSize = true;
			labelName.Location = new Point(12, 33);
			labelName.Name = "labelName";
			labelName.Size = new Size(59, 15);
			labelName.TabIndex = 4;
			labelName.Text = "Название";
			// 
			// labelConnString
			// 
			labelConnString.AutoSize = true;
			labelConnString.Location = new Point(12, 62);
			labelConnString.Name = "labelConnString";
			labelConnString.Size = new Size(125, 15);
			labelConnString.TabIndex = 5;
			labelConnString.Text = "Строка подключения";
			// 
			// FormNewDatabase
			// 
			AcceptButton = buttonSave;
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			CancelButton = buttonCancel;
			ClientSize = new Size(564, 93);
			ControlBox = false;
			Controls.Add(labelConnString);
			Controls.Add(labelName);
			Controls.Add(textBoxConnString);
			Controls.Add(textBoxName);
			Controls.Add(panelButtons);
			FormBorderStyle = FormBorderStyle.Fixed3D;
			Name = "FormNewDatabase";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Новая база данных";
			panelButtons.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Panel panelButtons;
		private Button buttonSave;
		private Button buttonCancel;
		private TextBox textBoxName;
		private TextBox textBoxConnString;
		private Label labelName;
		private Label labelConnString;
	}
}