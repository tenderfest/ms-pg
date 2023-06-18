namespace ConvertToPg
{
	public partial class FormNewDatabase : Form
	{
		public FormNewDatabase() =>
			InitializeComponent();

		/// <summary>
		/// Удобное для человека название базы двнных
		/// </summary>
		public string DatabaseName =>
			textBoxName.Text;
		public string BdName =>
			textBoxBdName.Text;
		public string BdServer =>
			textBoxServer.Text;
		public string BdPort =>
			textBoxPort.Text;
		public string BdLogin =>
			textBoxLogin.Text;
		public string BdPassword =>
			textBoxPassword.Text;

		private void TextBoxName_TextChanged(object sender, EventArgs e) =>
			buttonSave.Enabled = !string.IsNullOrEmpty(textBoxName.Text);
	}
}
