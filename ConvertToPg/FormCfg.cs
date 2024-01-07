using PgConvert.Config;
using PgConvert.Enums;

namespace ConvertToPg
{
	public partial class FormCfg : Form
	{
		private readonly ConvertMsToPgCfg _cfg;
		private int panelDatabaseHeight;

		public FormCfg() =>
			InitializeComponent();

		public FormCfg(ConvertMsToPgCfg cfg) : this() =>
			_cfg = cfg;

		private ConvertMsToPgCfg Cfg =>
			_cfg ?? new ConvertMsToPgCfg();

		private void FormCfg_Load(object sender, EventArgs e)
		{
			textBoxSkipOperations.Text = Cfg.SkipOperationAsText;
			textBoxSkipElement.Text = Cfg.SkipElementAsText;

			ShowDatabases(true);
		}

		private void ShowDatabases(bool addHeight)
		{
			groupBoxTargetDatabases.Controls.Clear();
			foreach (var db in Cfg.Databases)
			{
				PanelDatabase panel =
					new(
					db,
					db.ConnectionString,
					TestConnect,
					EditDatabase,
					DeleteDatabase)
					{
						Text = db.Name,
						Dock = DockStyle.Top,
						Location = new Point(1, 1),
						Enabled = !db.IsDefault,
					};
				if (addHeight)
				{
					panelDatabaseHeight = panel.Height;
					HeightChange(true);
				}
				groupBoxTargetDatabases.Controls.Add(panel);
			}
		}

		private void HeightChange(bool needAdd)
		{
			int height = needAdd
				? panelDatabaseHeight
				: panelDatabaseHeight * -1;

			groupBoxTargetDatabases.Height += height;
			Height += height;
		}

		private static void DatabaseFromControl(object sender, Action<OnePgDatabase> action)
		{
			var db = (sender as Control)?.Parent?.Parent?.Tag as OnePgDatabase;
			if (!string.IsNullOrEmpty(db?.ConnectionString))
			{
				action(db);
			}
		}

		private void TestConnect(object sender, EventArgs e) =>
			DatabaseFromControl(sender, (db) =>
			{
				var result = db.TestConnectDatabase();
				if (string.IsNullOrEmpty(result))
				{
					MessageBox.Show("OK");
					return;
				}

				if (DialogResult.Yes == MessageBox.Show(
						$"{result}\n\nНадо ли попытаться создать базу данных?",
						"Вопрос",
						MessageBoxButtons.YesNo))
				{
					MessageBox.Show(db.TryCreate());
				}
			});

		private void ButtonAddDatabase_Click(object sender, EventArgs e)
		{
			var newDbForm = Cfg.Databases.Count > 1
				? new FormNewDatabase(Cfg.Databases[^1].PartCopy)
				: new FormNewDatabase(null);
			if (newDbForm.ShowDialog(this) != DialogResult.OK)
				return;

			var newDatabase = newDbForm.Database;
			var addResult = Cfg.AddDelDatabase(newDatabase, true);
			if (addResult == ResultChangeDatabaseList.Ok)
			{
				HeightChange(true);
				ShowDatabases(false);
			}
			else if (addResult == ResultChangeDatabaseList.Error)
			{
				MessageBox.Show($"База данных с именем '{newDatabase.Name}' уже существует.");
			}
		}

		private void EditDatabase(object sender, EventArgs e) =>
			DatabaseFromControl(sender, (db) =>
			{
				var newDbForm = new FormNewDatabase(db);
				if (newDbForm.ShowDialog(this) != DialogResult.OK)
					return;

				if (db.PgConnectionString.IsError)
				{
					MessageBox.Show(db.PgConnectionString.Error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				ShowDatabases(false);
			});

		private void DeleteDatabase(object sender, EventArgs e) =>
			DatabaseFromControl(sender, (db) =>
			{
				if (db.IsDefault)
					return;

				if (Cfg.AddDelDatabase(db, false) == ResultChangeDatabaseList.Ok)
				{
					HeightChange(false);
					ShowDatabases(false);
				}
			});

		private void ButtonSave_Click(object sender, EventArgs e) =>
			DialogResult = DialogResult.OK;

		public string[] SkipOperation =>
			ConvertMsToPgCfg.GetStringArrayFromText(textBoxSkipOperations.Text);

		public string[] SkipElement =>
			ConvertMsToPgCfg.GetStringArrayFromText(textBoxSkipElement.Text);
	}
}
