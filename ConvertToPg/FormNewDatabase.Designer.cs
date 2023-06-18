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
			buttonCancel = new Button();
			buttonSave = new Button();
			textBoxName = new TextBox();
			labelName = new Label();
			labelBdName = new Label();
			textBoxBdName = new TextBox();
			labelPassword = new Label();
			textBoxPassword = new TextBox();
			labelLogin = new Label();
			textBoxLogin = new TextBox();
			labelPort = new Label();
			textBoxPort = new TextBox();
			labelServer = new Label();
			textBoxServer = new TextBox();
			panelButtons.SuspendLayout();
			SuspendLayout();
			// 
			// panelButtons
			// 
			panelButtons.Controls.Add(buttonCancel);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = DockStyle.Top;
			panelButtons.Location = new Point(0, 0);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new Size(495, 24);
			panelButtons.TabIndex = 0;
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
			// 
			// buttonSave
			// 
			buttonSave.DialogResult = DialogResult.OK;
			buttonSave.Dock = DockStyle.Right;
			buttonSave.Enabled = false;
			buttonSave.Location = new Point(420, 0);
			buttonSave.Name = "buttonSave";
			buttonSave.Size = new Size(75, 24);
			buttonSave.TabIndex = 1;
			buttonSave.Text = "Сохранить";
			buttonSave.UseVisualStyleBackColor = true;
			// 
			// textBoxName
			// 
			textBoxName.Location = new Point(77, 30);
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new Size(164, 23);
			textBoxName.TabIndex = 2;
			textBoxName.TextChanged += TextBoxName_TextChanged;
			// 
			// labelName
			// 
			labelName.AutoSize = true;
			labelName.Location = new Point(12, 33);
			labelName.Name = "labelName";
			labelName.Size = new Size(59, 15);
			labelName.TabIndex = 1;
			labelName.Text = "Название";
			// 
			// labelBdName
			// 
			labelBdName.AutoSize = true;
			labelBdName.Location = new Point(256, 33);
			labelBdName.Name = "labelBdName";
			labelBdName.Size = new Size(49, 15);
			labelBdName.TabIndex = 3;
			labelBdName.Text = "Имя БД";
			// 
			// textBoxBdName
			// 
			textBoxBdName.Location = new Point(311, 30);
			textBoxBdName.Name = "textBoxBdName";
			textBoxBdName.Size = new Size(164, 23);
			textBoxBdName.TabIndex = 4;
			// 
			// labelPassword
			// 
			labelPassword.AutoSize = true;
			labelPassword.Location = new Point(256, 91);
			labelPassword.Name = "labelPassword";
			labelPassword.Size = new Size(49, 15);
			labelPassword.TabIndex = 11;
			labelPassword.Text = "Пароль";
			// 
			// textBoxPassword
			// 
			textBoxPassword.Location = new Point(311, 88);
			textBoxPassword.Name = "textBoxPassword";
			textBoxPassword.Size = new Size(164, 23);
			textBoxPassword.TabIndex = 12;
			// 
			// labelLogin
			// 
			labelLogin.AutoSize = true;
			labelLogin.Location = new Point(12, 91);
			labelLogin.Name = "labelLogin";
			labelLogin.Size = new Size(41, 15);
			labelLogin.TabIndex = 9;
			labelLogin.Text = "Логин";
			// 
			// textBoxLogin
			// 
			textBoxLogin.Location = new Point(77, 88);
			textBoxLogin.Name = "textBoxLogin";
			textBoxLogin.Size = new Size(164, 23);
			textBoxLogin.TabIndex = 10;
			// 
			// labelPort
			// 
			labelPort.AutoSize = true;
			labelPort.Location = new Point(256, 62);
			labelPort.Name = "labelPort";
			labelPort.Size = new Size(35, 15);
			labelPort.TabIndex = 7;
			labelPort.Text = "Порт";
			// 
			// textBoxPort
			// 
			textBoxPort.Location = new Point(311, 59);
			textBoxPort.Name = "textBoxPort";
			textBoxPort.Size = new Size(164, 23);
			textBoxPort.TabIndex = 8;
			// 
			// labelServer
			// 
			labelServer.AutoSize = true;
			labelServer.Location = new Point(12, 62);
			labelServer.Name = "labelServer";
			labelServer.Size = new Size(47, 15);
			labelServer.TabIndex = 5;
			labelServer.Text = "Сервер";
			// 
			// textBoxServer
			// 
			textBoxServer.Location = new Point(77, 59);
			textBoxServer.Name = "textBoxServer";
			textBoxServer.Size = new Size(164, 23);
			textBoxServer.TabIndex = 6;
			// 
			// FormNewDatabase
			// 
			AcceptButton = buttonSave;
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			CancelButton = buttonCancel;
			ClientSize = new Size(495, 128);
			ControlBox = false;
			Controls.Add(labelPort);
			Controls.Add(textBoxPort);
			Controls.Add(labelServer);
			Controls.Add(textBoxServer);
			Controls.Add(labelPassword);
			Controls.Add(textBoxPassword);
			Controls.Add(labelLogin);
			Controls.Add(textBoxLogin);
			Controls.Add(labelBdName);
			Controls.Add(textBoxBdName);
			Controls.Add(labelName);
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
		private Label labelName;
		private Label labelBdName;
		private TextBox textBoxBdName;
		private Label labelPassword;
		private TextBox textBoxPassword;
		private Label labelLogin;
		private TextBox textBoxLogin;
		private Label labelPort;
		private TextBox textBoxPort;
		private Label labelServer;
		private TextBox textBoxServer;
	}
}