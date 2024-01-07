namespace ConvertToPg
{
	public partial class FormTableSpace : Form
	{
		public FormTableSpace() =>
			InitializeComponent();

		public string TableSpaceLocation =>
			textBoxTableSpace.Text;

		private void TextBoxTableSpace_TextChanged(object sender, EventArgs e) =>
			buttonSave.Enabled = !string.IsNullOrWhiteSpace(textBoxTableSpace.Text);
	}
}
