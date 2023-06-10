using PgConvert;

namespace ConvertToPg
{
	public partial class FormCfg : Form
	{
		private readonly ConvertMsToPgCfg _cfg;

		public FormCfg() => InitializeComponent();

		public FormCfg(ConvertMsToPgCfg cfg) : this() =>
			_cfg = cfg;

		private ConvertMsToPgCfg Cfg =>
			_cfg ?? new ConvertMsToPgCfg();

		private void TextBoxPathToPg_TextChanged(object sender, EventArgs e) =>
			Cfg.ConnectionStringToDic = textBoxPathToDic.Text;
		private void TextBoxPathToAct_TextChanged(object sender, EventArgs e) =>
			Cfg.ConnectionStringToWrk = textBoxPathToAct.Text;
		private void TextBoxPathToArc_TextChanged(object sender, EventArgs e) =>
			Cfg.ConnectionStringToArc = textBoxPathToArc.Text;

		private void FormCfg_Load(object sender, EventArgs e)
		{
			textBoxPathToDic.Text = Cfg.ConnectionStringToDic;
			textBoxPathToAct.Text = Cfg.ConnectionStringToWrk;
			textBoxPathToArc.Text = Cfg.ConnectionStringToArc;
			textBoxSkipOperations.Text = Cfg.GetSkipOperationAsText();
			textBoxSkipElement.Text = Cfg.GetSkipElementAsText();
		}

		private void ButtonSave_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}

		public string ConnectionStringToPg =>
			textBoxPathToDic.Text;
		public string ConnectionStringToAct =>
			textBoxPathToAct.Text;
		public string ConnectionStringToArc =>
			textBoxPathToArc.Text;
		public string[] SkipOperation =>
			ConvertMsToPgCfg.GetSkipArrayFromText(textBoxSkipOperations.Text);
		public string[] SkipElement =>
			ConvertMsToPgCfg.GetSkipArrayFromText(textBoxSkipElement.Text);
	}
}
