namespace PolygonEditor.View.Forms
{
    public partial class FixedEdgeLengthForm : Form
    {
        public int NewEdgeLength
            => (int)lengthNumericSelector.Value;

        public FixedEdgeLengthForm(int actualLength)
        {
            InitializeComponent();
            lengthNumericSelector.Value = actualLength;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
