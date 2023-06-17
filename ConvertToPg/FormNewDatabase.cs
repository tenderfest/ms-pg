namespace ConvertToPg
{
	public partial class FormNewDatabase : Form
	{
		public FormNewDatabase() =>
			InitializeComponent();

		public string DbName =>
			textBoxName.Text;
		public string DbConnectionString =>
			textBoxConnString.Text;

		private void TextBoxName_TextChanged(object sender, EventArgs e) =>
			buttonSave.Enabled = !string.IsNullOrEmpty(textBoxName.Text);

		private void ButtonCancel_Click(object sender, EventArgs e) =>
			Close();

		private void ButtonSave_Click(object sender, EventArgs e) => 
			DialogResult = DialogResult.OK;
	}
}
