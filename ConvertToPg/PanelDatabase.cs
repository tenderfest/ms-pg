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
			EventHandler editDatabase,
			EventHandler deleteDatabase)
			: this()
		{
			Tag = database;
			textBoxConnStr.Text = connectString;
			buttonCheckConnect.Click += checkConnect;
			buttonEditDatabase.Click += editDatabase;
			buttonDelDatabase.Click += deleteDatabase;
		}

		public override string Text
		{
			set =>
				labelConnStr.Text = value;
		}

		public event EventHandler ClickButtonCheckConnect
		{
			add =>
				Events.AddHandler(nameof(ClickButtonCheckConnect), value);

			remove =>
				Events.RemoveHandler(nameof(ClickButtonCheckConnect), value);
		}
	}
}
