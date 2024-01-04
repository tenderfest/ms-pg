namespace ConvertToPg
{
	partial class PanelDatabase
	{
		/// <summary> 
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором компонентов

		/// <summary> 
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			panel = new Panel();
			textBoxConnStr = new TextBox();
			labelConnStr = new Label();
			buttonCheckConnect = new Button();
			buttonEditDatabase = new Button();
			buttonDelDatabase = new Button();
			panel.SuspendLayout();
			SuspendLayout();
			// 
			// panel
			// 
			panel.BorderStyle = BorderStyle.Fixed3D;
			panel.Controls.Add(textBoxConnStr);
			panel.Controls.Add(labelConnStr);
			panel.Controls.Add(buttonCheckConnect);
			panel.Controls.Add(buttonEditDatabase);
			panel.Controls.Add(buttonDelDatabase);
			panel.Dock = DockStyle.Top;
			panel.Location = new Point(0, 0);
			panel.Name = "panel";
			panel.Size = new Size(567, 28);
			panel.TabIndex = 11;
			// 
			// textBoxConnStr
			// 
			textBoxConnStr.Dock = DockStyle.Fill;
			textBoxConnStr.Location = new Point(133, 0);
			textBoxConnStr.Name = "textBoxConnStr";
			textBoxConnStr.ReadOnly = true;
			textBoxConnStr.Size = new Size(361, 23);
			textBoxConnStr.TabIndex = 3;
			// 
			// labelConnStr
			// 
			labelConnStr.AutoSize = true;
			labelConnStr.Dock = DockStyle.Left;
			labelConnStr.Location = new Point(0, 0);
			labelConnStr.Name = "labelConnStr";
			labelConnStr.Size = new Size(133, 15);
			labelConnStr.TabIndex = 2;
			labelConnStr.Text = "Название базы данных";
			// 
			// buttonCheckConnect
			// 
			buttonCheckConnect.Dock = DockStyle.Right;
			buttonCheckConnect.Location = new Point(494, 0);
			buttonCheckConnect.Name = "buttonCheckConnect";
			buttonCheckConnect.Size = new Size(23, 24);
			buttonCheckConnect.TabIndex = 9;
			buttonCheckConnect.Text = "?";
			buttonCheckConnect.UseVisualStyleBackColor = true;
			// 
			// buttonEditDatabase
			// 
			buttonEditDatabase.Dock = DockStyle.Right;
			buttonEditDatabase.Location = new Point(517, 0);
			buttonEditDatabase.Name = "buttonEditDatabase";
			buttonEditDatabase.Size = new Size(23, 24);
			buttonEditDatabase.TabIndex = 10;
			buttonEditDatabase.Text = "E";
			buttonEditDatabase.UseVisualStyleBackColor = true;
			// 
			// buttonDelDatabase
			// 
			buttonDelDatabase.Dock = DockStyle.Right;
			buttonDelDatabase.Location = new Point(540, 0);
			buttonDelDatabase.Name = "buttonDelDatabase";
			buttonDelDatabase.Size = new Size(23, 24);
			buttonDelDatabase.TabIndex = 8;
			buttonDelDatabase.Text = "X";
			buttonDelDatabase.UseVisualStyleBackColor = true;
			// 
			// PanelDatabase
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(panel);
			Name = "PanelDatabase";
			Size = new Size(567, 30);
			panel.ResumeLayout(false);
			panel.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private Panel panel;
		private Label labelConnStr;
		private Button buttonCheckConnect;
		private TextBox textBoxConnStr;
		private Button buttonDelDatabase;
		private Button buttonEditDatabase;
	}
}
