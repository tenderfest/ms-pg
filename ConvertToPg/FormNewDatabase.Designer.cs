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
			textBoxName = new TextBox();
			buttonSave = new Button();
			labelName = new Label();
			buttonCancel = new Button();
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
			textBoxConnectionString = new TextBox();
			groupBoxDbParams = new GroupBox();
			groupBoxConnectionString = new GroupBox();
			panelButtons.SuspendLayout();
			groupBoxDbParams.SuspendLayout();
			groupBoxConnectionString.SuspendLayout();
			SuspendLayout();
			// 
			// panelButtons
			// 
			panelButtons.Controls.Add(textBoxName);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Controls.Add(labelName);
			panelButtons.Controls.Add(buttonCancel);
			panelButtons.Dock = DockStyle.Top;
			panelButtons.Location = new Point(0, 0);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new Size(480, 24);
			panelButtons.TabIndex = 0;
			// 
			// textBoxName
			// 
			textBoxName.Dock = DockStyle.Fill;
			textBoxName.Location = new Point(142, 0);
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new Size(263, 23);
			textBoxName.TabIndex = 2;
			textBoxName.TextChanged += TextBoxName_TextChanged;
			// 
			// buttonSave
			// 
			buttonSave.DialogResult = DialogResult.OK;
			buttonSave.Dock = DockStyle.Right;
			buttonSave.Enabled = false;
			buttonSave.Location = new Point(405, 0);
			buttonSave.Name = "buttonSave";
			buttonSave.Size = new Size(75, 24);
			buttonSave.TabIndex = 1;
			buttonSave.Text = "Сохранить";
			buttonSave.UseVisualStyleBackColor = true;
			// 
			// labelName
			// 
			labelName.Dock = DockStyle.Left;
			labelName.Location = new Point(75, 0);
			labelName.Name = "labelName";
			labelName.Size = new Size(67, 24);
			labelName.TabIndex = 1;
			labelName.Text = "Название";
			labelName.TextAlign = ContentAlignment.MiddleLeft;
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
			// labelBdName
			// 
			labelBdName.AutoSize = true;
			labelBdName.Location = new Point(6, 48);
			labelBdName.Name = "labelBdName";
			labelBdName.Size = new Size(49, 15);
			labelBdName.TabIndex = 3;
			labelBdName.Text = "Имя БД";
			// 
			// textBoxBdName
			// 
			textBoxBdName.Location = new Point(71, 45);
			textBoxBdName.Name = "textBoxBdName";
			textBoxBdName.Size = new Size(164, 23);
			textBoxBdName.TabIndex = 4;
			// 
			// labelPassword
			// 
			labelPassword.AutoSize = true;
			labelPassword.Location = new Point(250, 77);
			labelPassword.Name = "labelPassword";
			labelPassword.Size = new Size(49, 15);
			labelPassword.TabIndex = 11;
			labelPassword.Text = "Пароль";
			// 
			// textBoxPassword
			// 
			textBoxPassword.Location = new Point(305, 74);
			textBoxPassword.Name = "textBoxPassword";
			textBoxPassword.Size = new Size(164, 23);
			textBoxPassword.TabIndex = 12;
			textBoxPassword.TextChanged += TextBoxName_TextChanged;
			// 
			// labelLogin
			// 
			labelLogin.AutoSize = true;
			labelLogin.Location = new Point(6, 77);
			labelLogin.Name = "labelLogin";
			labelLogin.Size = new Size(41, 15);
			labelLogin.TabIndex = 9;
			labelLogin.Text = "Логин";
			// 
			// textBoxLogin
			// 
			textBoxLogin.Location = new Point(71, 74);
			textBoxLogin.Name = "textBoxLogin";
			textBoxLogin.Size = new Size(164, 23);
			textBoxLogin.TabIndex = 10;
			textBoxLogin.TextChanged += TextBoxName_TextChanged;
			// 
			// labelPort
			// 
			labelPort.AutoSize = true;
			labelPort.Location = new Point(250, 19);
			labelPort.Name = "labelPort";
			labelPort.Size = new Size(35, 15);
			labelPort.TabIndex = 7;
			labelPort.Text = "Порт";
			// 
			// textBoxPort
			// 
			textBoxPort.Location = new Point(305, 16);
			textBoxPort.Name = "textBoxPort";
			textBoxPort.Size = new Size(164, 23);
			textBoxPort.TabIndex = 8;
			textBoxPort.TextChanged += TextBoxName_TextChanged;
			// 
			// labelServer
			// 
			labelServer.AutoSize = true;
			labelServer.Location = new Point(6, 19);
			labelServer.Name = "labelServer";
			labelServer.Size = new Size(47, 15);
			labelServer.TabIndex = 5;
			labelServer.Text = "Сервер";
			// 
			// textBoxServer
			// 
			textBoxServer.Location = new Point(71, 16);
			textBoxServer.Name = "textBoxServer";
			textBoxServer.Size = new Size(164, 23);
			textBoxServer.TabIndex = 6;
			textBoxServer.TextChanged += TextBoxName_TextChanged;
			// 
			// textBoxConnectionString
			// 
			textBoxConnectionString.Location = new Point(6, 22);
			textBoxConnectionString.Name = "textBoxConnectionString";
			textBoxConnectionString.Size = new Size(463, 23);
			textBoxConnectionString.TabIndex = 14;
			textBoxConnectionString.TextChanged += TextBoxName_TextChanged;
			// 
			// groupBoxDbParams
			// 
			groupBoxDbParams.Controls.Add(labelServer);
			groupBoxDbParams.Controls.Add(textBoxBdName);
			groupBoxDbParams.Controls.Add(labelBdName);
			groupBoxDbParams.Controls.Add(labelPort);
			groupBoxDbParams.Controls.Add(textBoxLogin);
			groupBoxDbParams.Controls.Add(textBoxPort);
			groupBoxDbParams.Controls.Add(labelLogin);
			groupBoxDbParams.Controls.Add(textBoxPassword);
			groupBoxDbParams.Controls.Add(textBoxServer);
			groupBoxDbParams.Controls.Add(labelPassword);
			groupBoxDbParams.Dock = DockStyle.Top;
			groupBoxDbParams.Location = new Point(0, 24);
			groupBoxDbParams.Name = "groupBoxDbParams";
			groupBoxDbParams.Size = new Size(480, 105);
			groupBoxDbParams.TabIndex = 15;
			groupBoxDbParams.TabStop = false;
			groupBoxDbParams.Text = "Параметры подключения";
			// 
			// groupBoxConnectionString
			// 
			groupBoxConnectionString.Controls.Add(textBoxConnectionString);
			groupBoxConnectionString.Dock = DockStyle.Top;
			groupBoxConnectionString.Location = new Point(0, 129);
			groupBoxConnectionString.Name = "groupBoxConnectionString";
			groupBoxConnectionString.Size = new Size(480, 54);
			groupBoxConnectionString.TabIndex = 16;
			groupBoxConnectionString.TabStop = false;
			groupBoxConnectionString.Text = "Строка подключения";
			// 
			// FormNewDatabase
			// 
			AcceptButton = buttonSave;
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			CancelButton = buttonCancel;
			ClientSize = new Size(480, 182);
			ControlBox = false;
			Controls.Add(groupBoxConnectionString);
			Controls.Add(groupBoxDbParams);
			Controls.Add(panelButtons);
			FormBorderStyle = FormBorderStyle.Fixed3D;
			Name = "FormNewDatabase";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Новая база данных";
			panelButtons.ResumeLayout(false);
			panelButtons.PerformLayout();
			groupBoxDbParams.ResumeLayout(false);
			groupBoxDbParams.PerformLayout();
			groupBoxConnectionString.ResumeLayout(false);
			groupBoxConnectionString.PerformLayout();
			ResumeLayout(false);
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
		private TextBox textBoxConnectionString;
		private GroupBox groupBoxDbParams;
		private GroupBox groupBoxConnectionString;
	}
}