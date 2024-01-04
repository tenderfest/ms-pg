using PgConvert.Config;

namespace ConvertToPg;

public partial class FormNewDatabase : Form
{
	public readonly OnePgDatabase Database;
	public readonly PgConnectionString connString;

	public FormNewDatabase()
	{
		InitializeComponent();
		comboBoxTableSpace.SelectedIndex = 0;
	}

	public FormNewDatabase(OnePgDatabase db) : this()
	{
		if (null != db && !string.IsNullOrEmpty(db.ConnectionString))
		{
			Database = db;
		}
		else
		{
			Database = new OnePgDatabase();
		}
		connString = Database.PgConnectionString;
	}

	/// <summary>
	/// Удобное для человека название базы данных
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

	private void TextBoxServer_TextChanged(object sender, EventArgs e)
	{
		connString.Server = textBoxServer.Text.Trim();
		EnableElementsShowConnString();
	}

	private void TextBoxPort_TextChanged(object sender, EventArgs e)
	{
		connString.Port = textBoxPort.Text.Trim();
		EnableElementsShowConnString();
	}

	private void TextBoxLogin_TextChanged(object sender, EventArgs e)
	{
		connString.Login = textBoxLogin.Text.Trim();
		EnableElementsShowConnString();
	}

	private void TextBoxPassword_TextChanged(object sender, EventArgs e)
	{
		connString.Password = textBoxPassword.Text.Trim();
		EnableElementsShowConnString();
	}

	private void TextBoxBdName_TextChanged(object sender, EventArgs e)
	{
		connString.DatabaseName = textBoxBdName.Text.Trim();
		EnableElementsShowConnString();
	}

	private void TextBoxName_TextChanged(object sender, EventArgs e)
	{
		Database.Name = textBoxName.Text.Trim();
	}

	private void EnableElementsShowConnString()
	{
		comboBoxTableSpace.Enabled =
		buttonTestConnect.Enabled =
		buttonCreateDatabase.Enabled =
		buttonSave.Enabled = !string.IsNullOrEmpty(textBoxName.Text) &&
		(!string.IsNullOrEmpty(textBoxServer.Text) &&
		!string.IsNullOrEmpty(textBoxPort.Text) &&
		!string.IsNullOrEmpty(textBoxLogin.Text) &&
		!string.IsNullOrEmpty(textBoxPassword.Text));

		textBoxConnectionString.Text = connString.GetConnectionString(Database.IsDefault);
	}

	private void TextBoxTableSpace_TextChanged(object sender, EventArgs e)
	{
		buttonAddTableSpace.Enabled = textBoxTableSpace.Text.Trim().Length > 0;
	}

	private void ButtonAddTableSpace_Click(object sender, EventArgs e)
	{
		var tableSpace = textBoxTableSpace.Text.Trim();
		if (string.IsNullOrEmpty(tableSpace))
			return;

		if (tableSpace.ToLower().StartsWith("pg_"))
		{
			MessageBox.Show(
				"Имя создаваемого табличного пространства не может начинаться с 'pg_'",
				"Ошибка",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error);
			return;
		}

		// TODO здесь создание ТП на сервере
		MessageBox.Show("сделать!");
	}

	private void FormNewDatabase_Load(object sender, EventArgs e)
	{
		if (null == Database)
			return;

		textBoxName.Text = Database.Name;
		textBoxBdName.Text = Database.GetBdName;
		textBoxServer.Text = Database.GetServer;
		textBoxPort.Text = Database.GetPort;
		textBoxLogin.Text = Database.GetLogin;
		textBoxPassword.Text = Database.GetPassword;
		textBoxConnectionString.Text = Database.ConnectionString;
	}
}
