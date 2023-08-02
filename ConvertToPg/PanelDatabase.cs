namespace ConvertToPg
{
	public partial class PanelDatabase : UserControl
	{
		public PanelDatabase()
		{
			InitializeComponent();
		}

		public PanelDatabase(
			object database,
			string connectString,
			EventHandler checkConnect,
			EventHandler deleteDatabase,
			EventHandler connectStringChanged)
			: this()
		{
			Tag = database;
			textBoxConnStr.Text = connectString;
			buttonCheckConnect.Click += checkConnect;
			buttonDelDatabase.Click += deleteDatabase;
			textBoxConnStr.TextChanged += connectStringChanged;
		}

		public override string Text { set { labelConnStr.Text = value; } }

		public event EventHandler ClickButtonCheckConnect
		{
			add =>
				Events.AddHandler(nameof(ClickButtonCheckConnect), value);

			remove =>
				Events.RemoveHandler(nameof(ClickButtonCheckConnect), value);
		}
	}
}
