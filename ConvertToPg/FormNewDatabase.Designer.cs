
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
			textBoxConnectionString = new TextBox();
			groupBoxDbParams = new GroupBox();
			buttonTestConnect = new Button();
			buttonCreateDatabase = new Button();
			comboBoxTableSpace = new ComboBox();
			groupBoxTableSpace = new GroupBox();
			textBoxTableSpace = new TextBox();
			buttonAddTableSpace = new Button();
			groupBoxConnectionString = new GroupBox();
			panelButtons.SuspendLayout();
			groupBoxDbParams.SuspendLayout();
			groupBoxTableSpace.SuspendLayout();
			groupBoxConnectionString.SuspendLayout();
			SuspendLayout();
			// 
			// panelButtons
			// 
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Controls.Add(buttonCancel);
			panelButtons.Dock = DockStyle.Top;
			panelButtons.Location = new Point(0, 0);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new Size(509, 24);
			panelButtons.TabIndex = 0;
			// 
			// buttonSave
			// 
			buttonSave.DialogResult = DialogResult.OK;
			buttonSave.Dock = DockStyle.Right;
			buttonSave.Enabled = false;
			buttonSave.Location = new Point(434, 0);
			buttonSave.Name = "buttonSave";
			buttonSave.Size = new Size(75, 24);
			buttonSave.TabIndex = 1;
			buttonSave.Text = "Сохранить";
			buttonSave.UseVisualStyleBackColor = true;
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
			// textBoxName
			// 
			textBoxName.Dock = DockStyle.Top;
			textBoxName.Location = new Point(0, 39);
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new Size(509, 23);
			textBoxName.TabIndex = 1;
			textBoxName.TextChanged += TextBoxName_TextChanged;
			// 
			// labelName
			// 
			labelName.AutoSize = true;
			labelName.Dock = DockStyle.Top;
			labelName.Location = new Point(0, 24);
			labelName.Name = "labelName";
			labelName.Size = new Size(257, 15);
			labelName.TabIndex = 0;
			labelName.Text = "Удобное для человека название базы данных";
			labelName.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// labelBdName
			// 
			labelBdName.AutoSize = true;
			labelBdName.Location = new Point(5, 143);
			labelBdName.Name = "labelBdName";
			labelBdName.Size = new Size(49, 15);
			labelBdName.TabIndex = 9;
			labelBdName.Text = "Имя БД";
			// 
			// textBoxBdName
			// 
			textBoxBdName.Location = new Point(60, 140);
			textBoxBdName.Name = "textBoxBdName";
			textBoxBdName.Size = new Size(181, 23);
			textBoxBdName.TabIndex = 10;
			textBoxBdName.TextChanged += TextBoxBdName_TextChanged;
			// 
			// labelPassword
			// 
			labelPassword.AutoSize = true;
			labelPassword.Location = new Point(247, 54);
			labelPassword.Name = "labelPassword";
			labelPassword.Size = new Size(49, 15);
			labelPassword.TabIndex = 6;
			labelPassword.Text = "Пароль";
			// 
			// textBoxPassword
			// 
			textBoxPassword.Location = new Point(321, 51);
			textBoxPassword.Name = "textBoxPassword";
			textBoxPassword.Size = new Size(181, 23);
			textBoxPassword.TabIndex = 7;
			textBoxPassword.TextChanged += TextBoxPassword_TextChanged;
			// 
			// labelLogin
			// 
			labelLogin.AutoSize = true;
			labelLogin.Location = new Point(5, 54);
			labelLogin.Name = "labelLogin";
			labelLogin.Size = new Size(41, 15);
			labelLogin.TabIndex = 4;
			labelLogin.Text = "Логин";
			// 
			// textBoxLogin
			// 
			textBoxLogin.Location = new Point(60, 51);
			textBoxLogin.Name = "textBoxLogin";
			textBoxLogin.Size = new Size(181, 23);
			textBoxLogin.TabIndex = 5;
			textBoxLogin.TextChanged += TextBoxLogin_TextChanged;
			// 
			// labelPort
			// 
			labelPort.AutoSize = true;
			labelPort.Location = new Point(247, 25);
			labelPort.Name = "labelPort";
			labelPort.Size = new Size(35, 15);
			labelPort.TabIndex = 2;
			labelPort.Text = "Порт";
			// 
			// textBoxPort
			// 
			textBoxPort.Location = new Point(321, 22);
			textBoxPort.Name = "textBoxPort";
			textBoxPort.Size = new Size(181, 23);
			textBoxPort.TabIndex = 3;
			textBoxPort.TextChanged += TextBoxPort_TextChanged;
			// 
			// labelServer
			// 
			labelServer.AutoSize = true;
			labelServer.Location = new Point(5, 25);
			labelServer.Name = "labelServer";
			labelServer.Size = new Size(47, 15);
			labelServer.TabIndex = 0;
			labelServer.Text = "Сервер";
			// 
			// textBoxServer
			// 
			textBoxServer.Location = new Point(60, 22);
			textBoxServer.Name = "textBoxServer";
			textBoxServer.Size = new Size(181, 23);
			textBoxServer.TabIndex = 1;
			textBoxServer.TextChanged += TextBoxServer_TextChanged;
			// 
			// textBoxConnectionString
			// 
			textBoxConnectionString.Location = new Point(6, 22);
			textBoxConnectionString.Name = "textBoxConnectionString";
			textBoxConnectionString.ReadOnly = true;
			textBoxConnectionString.Size = new Size(497, 23);
			textBoxConnectionString.TabIndex = 0;
			// 
			// groupBoxDbParams
			// 
			groupBoxDbParams.Controls.Add(buttonTestConnect);
			groupBoxDbParams.Controls.Add(buttonCreateDatabase);
			groupBoxDbParams.Controls.Add(comboBoxTableSpace);
			groupBoxDbParams.Controls.Add(groupBoxTableSpace);
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
			groupBoxDbParams.Location = new Point(0, 62);
			groupBoxDbParams.Name = "groupBoxDbParams";
			groupBoxDbParams.Size = new Size(509, 202);
			groupBoxDbParams.TabIndex = 2;
			groupBoxDbParams.TabStop = false;
			groupBoxDbParams.Text = "Параметры подключения";
			// 
			// buttonTestConnect
			// 
			buttonTestConnect.Enabled = false;
			buttonTestConnect.Location = new Point(60, 169);
			buttonTestConnect.Name = "buttonTestConnect";
			buttonTestConnect.Size = new Size(181, 23);
			buttonTestConnect.TabIndex = 12;
			buttonTestConnect.Text = "Проверить подключение";
			buttonTestConnect.UseVisualStyleBackColor = true;
			// 
			// buttonCreateDatabase
			// 
			buttonCreateDatabase.Enabled = false;
			buttonCreateDatabase.Location = new Point(247, 169);
			buttonCreateDatabase.Name = "buttonCreateDatabase";
			buttonCreateDatabase.Size = new Size(255, 23);
			buttonCreateDatabase.TabIndex = 13;
			buttonCreateDatabase.Text = "Создать базу данных";
			buttonCreateDatabase.UseVisualStyleBackColor = true;
			// 
			// comboBoxTableSpace
			// 
			comboBoxTableSpace.DropDownStyle = ComboBoxStyle.DropDownList;
			comboBoxTableSpace.Enabled = false;
			comboBoxTableSpace.FormattingEnabled = true;
			comboBoxTableSpace.Items.AddRange(new object[] { "Табличное пространство по умолчанию" });
			comboBoxTableSpace.Location = new Point(247, 140);
			comboBoxTableSpace.Name = "comboBoxTableSpace";
			comboBoxTableSpace.Size = new Size(255, 23);
			comboBoxTableSpace.TabIndex = 11;
			comboBoxTableSpace.SelectedIndexChanged += ComboBoxTableSpace_SelectedIndexChanged;
			// 
			// groupBoxTableSpace
			// 
			groupBoxTableSpace.Controls.Add(textBoxTableSpace);
			groupBoxTableSpace.Controls.Add(buttonAddTableSpace);
			groupBoxTableSpace.Location = new Point(-1, 80);
			groupBoxTableSpace.Name = "groupBoxTableSpace";
			groupBoxTableSpace.Size = new Size(509, 54);
			groupBoxTableSpace.TabIndex = 8;
			groupBoxTableSpace.TabStop = false;
			groupBoxTableSpace.Text = "Табличное пространство";
			// 
			// textBoxTableSpace
			// 
			textBoxTableSpace.Location = new Point(6, 22);
			textBoxTableSpace.Name = "textBoxTableSpace";
			textBoxTableSpace.Size = new Size(222, 23);
			textBoxTableSpace.TabIndex = 0;
			textBoxTableSpace.TextChanged += TextBoxTableSpace_TextChanged;
			// 
			// buttonAddTableSpace
			// 
			buttonAddTableSpace.Enabled = false;
			buttonAddTableSpace.Location = new Point(234, 21);
			buttonAddTableSpace.Name = "buttonAddTableSpace";
			buttonAddTableSpace.Size = new Size(269, 23);
			buttonAddTableSpace.TabIndex = 1;
			buttonAddTableSpace.Text = "Добавить табличное пространство на сервер";
			buttonAddTableSpace.UseVisualStyleBackColor = true;
			buttonAddTableSpace.Click += ButtonAddTableSpace_Click;
			// 
			// groupBoxConnectionString
			// 
			groupBoxConnectionString.Controls.Add(textBoxConnectionString);
			groupBoxConnectionString.Dock = DockStyle.Top;
			groupBoxConnectionString.Location = new Point(0, 264);
			groupBoxConnectionString.Name = "groupBoxConnectionString";
			groupBoxConnectionString.Size = new Size(509, 54);
			groupBoxConnectionString.TabIndex = 3;
			groupBoxConnectionString.TabStop = false;
			groupBoxConnectionString.Text = "Строка подключения";
			// 
			// FormNewDatabase
			// 
			AcceptButton = buttonSave;
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			CancelButton = buttonCancel;
			ClientSize = new Size(509, 318);
			ControlBox = false;
			Controls.Add(groupBoxConnectionString);
			Controls.Add(groupBoxDbParams);
			Controls.Add(textBoxName);
			Controls.Add(labelName);
			Controls.Add(panelButtons);
			FormBorderStyle = FormBorderStyle.Fixed3D;
			Name = "FormNewDatabase";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Новая база данных";
			Load += FormNewDatabase_Load;
			panelButtons.ResumeLayout(false);
			groupBoxDbParams.ResumeLayout(false);
			groupBoxDbParams.PerformLayout();
			groupBoxTableSpace.ResumeLayout(false);
			groupBoxTableSpace.PerformLayout();
			groupBoxConnectionString.ResumeLayout(false);
			groupBoxConnectionString.PerformLayout();
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
		private TextBox textBoxConnectionString;
		private GroupBox groupBoxDbParams;
		private GroupBox groupBoxConnectionString;
		private ComboBox comboBoxTableSpace;
		private Button buttonAddTableSpace;
		private Label labelTableSpace;
		private GroupBox groupBoxTableSpace;
		private TextBox textBoxTableSpace;
		private Button buttonTestConnect;
		private Button buttonCreateDatabase;
	}
}