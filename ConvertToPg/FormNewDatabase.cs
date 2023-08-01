namespace ConvertToPg;

public partial class FormNewDatabase : Form
{
	public FormNewDatabase() =>
		InitializeComponent();

	/// <summary>
	/// Удобное для человека название базы двнных
	/// </summary>
	public string DatabaseName =>
		textBoxName.Text;
	/// <summary>
	/// Технологическое название базы двнных
	/// </summary>
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
	public string ConnectionString =>
		textBoxConnectionString.Text;

	private void TextBoxName_TextChanged(object sender, EventArgs e) =>
		buttonSave.Enabled = !string.IsNullOrEmpty(textBoxName.Text) &&
		(!string.IsNullOrEmpty(textBoxConnectionString.Text) ||
			(!string.IsNullOrEmpty(textBoxServer.Text) &&
			!string.IsNullOrEmpty(textBoxPort.Text) &&
			!string.IsNullOrEmpty(textBoxLogin.Text) &&
			!string.IsNullOrEmpty(textBoxPassword.Text))
		);
}
