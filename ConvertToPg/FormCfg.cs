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

			MakeDatabases(true);
		}

		private void MakeDatabases(bool addHeight)
		{
			groupBoxTargetDatabases.Controls.Clear();
			foreach (var db in Cfg.Databases)
			{
				PanelDatabase panel = new(
					db,
					db.ConnectionString,
					TestConnect,
					DeleteDatabase,
					ConnectionStringChanged);
				if (addHeight)
				{
					panelDatabaseHeight = panel.Height;
					HeightChange(true);
				}
				groupBoxTargetDatabases.Controls.Add(panel);
				panel.Text = db.Name;
				panel.Dock = DockStyle.Top;
				panel.Location = new Point(1, 1);
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
				var databases = Cfg.Databases.ToList();
				databases.Remove(db);
				Cfg.Databases = databases.ToArray();
				HeightChange(false);
				MakeDatabases(false);
			});

		private void ConnectionStringChanged(object sender, EventArgs e) =>
			DatabaseFromControl(sender, (db) => db.ConnectionString = ((TextBox)sender).Text);

		private void ButtonSave_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}

		private void ButtonAddDatabase_Click(object sender, EventArgs e)
		{

		}

		public string[] SkipOperation =>
			ConvertMsToPgCfg.GetSkipArrayFromText(textBoxSkipOperations.Text);
		public string[] SkipElement =>
			ConvertMsToPgCfg.GetSkipArrayFromText(textBoxSkipElement.Text);
	}
}
