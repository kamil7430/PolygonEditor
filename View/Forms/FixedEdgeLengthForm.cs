namespace PolygonEditor.View.Forms
{
    public partial class FixedEdgeLengthForm : Form
    {
        public float NewEdgeLength
            => (float)lengthNumericSelector.Value;

        public FixedEdgeLengthForm(decimal actualLength)
        {
            InitializeComponent();
            if (actualLength < lengthNumericSelector.Minimum)
                lengthNumericSelector.Value = lengthNumericSelector.Minimum;
            else if (actualLength > lengthNumericSelector.Maximum)
                lengthNumericSelector.Value = lengthNumericSelector.Maximum;
            else
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
