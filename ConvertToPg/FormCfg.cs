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
					DeleteDatabase,
					ConnectionStringChanged)
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
			int height = needAdd ? panelDatabaseHeight : panelDatabaseHeight * -1;
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
					MessageBox.Show("OK");
				else
				{
					var tryCreateDatabaseAnswer = MessageBox
						.Show($"{result}\n\nНадо ли попытаться создать базу данных?", "Ошибка", MessageBoxButtons.YesNo);
					if (tryCreateDatabaseAnswer == DialogResult.Yes)
					{
						result = db.TryCreate();
						MessageBox.Show(result);
					}
				}
			});

		private void DeleteDatabase(object sender, EventArgs e) =>
			DatabaseFromControl(sender, (db) =>
			{
				if (db.IsDefault) return;

				if (Cfg.AddDelDatabase(db, false) == ResultChangeDatabaseList.Ok)
				{
					HeightChange(false);
					ShowDatabases(false);
				}
			});

		private void ConnectionStringChanged(object sender, EventArgs e) =>
			DatabaseFromControl(sender, (db) =>
				db.ConnectionString = ((TextBox)sender).Text);

		private void ButtonSave_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}

		private void ButtonAddDatabase_Click(object sender, EventArgs e)
		{
			var newDbForm = new FormNewDatabase();
			if (newDbForm.ShowDialog(this) != DialogResult.OK)
				return;

			var newDatabase = new OnePgDatabase(newDbForm.DatabaseName);
			if (!string.IsNullOrEmpty(newDbForm.ConnectionString))
				newDatabase.ConnectionString = newDbForm.ConnectionString;
			else
			{
				var errSetConnectionString = newDatabase.SetConnectionString(
						newDbForm.BdServer,
						newDbForm.BdPort,
						newDbForm.BdName,
						newDbForm.BdLogin,
						newDbForm.BdPassword);
				if (!string.IsNullOrEmpty(errSetConnectionString))
				{
					MessageBox.Show(errSetConnectionString, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
			}

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

		public string[] SkipOperation =>
			ConvertMsToPgCfg.GetStringArrayFromText(textBoxSkipOperations.Text);
		public string[] SkipElement =>
			ConvertMsToPgCfg.GetStringArrayFromText(textBoxSkipElement.Text);
	}
}
