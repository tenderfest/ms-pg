using PgConvert.Config;
using System.Windows.Forms;

namespace ConvertToPg
{
	public partial class FormCfg : Form
	{
		private readonly ConvertMsToPgCfg _cfg;
		private int panelDatabaseHeight;

		public FormCfg() => InitializeComponent();

		public FormCfg(ConvertMsToPgCfg cfg) : this() =>
			_cfg = cfg;

		private ConvertMsToPgCfg Cfg =>
			_cfg ?? new ConvertMsToPgCfg();

		private void FormCfg_Load(object sender, EventArgs e)
		{
			textBoxSkipOperations.Text = Cfg.GetSkipOperationAsText();
			textBoxSkipElement.Text = Cfg.GetSkipElementAsText();

			ShowDatabases(true);
		}

		private void ShowDatabases(bool addHeight)
		{
			groupBoxTargetDatabases.Controls.Clear();
			foreach (var db in Cfg.Databases)
			{
				PanelDatabase panel = new(
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
				MessageBox.Show(string.IsNullOrEmpty(result) ? "OK" : result);
			});

		private void DeleteDatabase(object sender, EventArgs e) =>
			DatabaseFromControl(sender, (db) =>
			{
				if (db.IsDefault) return;

				Cfg.AddDelDatabase(db, false);
				HeightChange(false);
				ShowDatabases(false);
			});

		private void ConnectionStringChanged(object sender, EventArgs e) =>
			DatabaseFromControl(sender, (db) => db.ConnectionString = ((TextBox)sender).Text);

		private void ButtonSave_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}

		private void ButtonAddDatabase_Click(object sender, EventArgs e)
		{
			var newDBForm = new FormNewDatabase();
			newDBForm.ShowDialog(this);
			var dr = newDBForm.DialogResult;
			if (dr != DialogResult.OK)
				return;

			Cfg.AddDelDatabase(new OnePgDatabase
			{
				Name = newDBForm.DbName,
				ConnectionString = newDBForm.DbConnectionString
			}, true);
			HeightChange(true);
			ShowDatabases(true);
		}

		public string[] SkipOperation =>
			ConvertMsToPgCfg.GetSkipArrayFromText(textBoxSkipOperations.Text);
		public string[] SkipElement =>
			ConvertMsToPgCfg.GetSkipArrayFromText(textBoxSkipElement.Text);
	}
}
