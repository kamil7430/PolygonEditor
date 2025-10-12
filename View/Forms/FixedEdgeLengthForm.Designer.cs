namespace PolygonEditor.View.Forms
{
    partial class FixedEdgeLengthForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            okButton = new Button();
            cancelButton = new Button();
            lengthNumericSelector = new NumericUpDown();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)lengthNumericSelector).BeginInit();
            SuspendLayout();
            // 
            // okButton
            // 
            okButton.Location = new Point(254, 98);
            okButton.Name = "okButton";
            okButton.Size = new Size(112, 34);
            okButton.TabIndex = 0;
            okButton.Text = "OK";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(136, 98);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(112, 34);
            cancelButton.TabIndex = 1;
            cancelButton.Text = "Anuluj";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // lengthNumericSelector
            // 
            lengthNumericSelector.Location = new Point(221, 38);
            lengthNumericSelector.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            lengthNumericSelector.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            lengthNumericSelector.Name = "lengthNumericSelector";
            lengthNumericSelector.Size = new Size(145, 31);
            lengthNumericSelector.TabIndex = 2;
            lengthNumericSelector.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 40);
            label1.Name = "label1";
            label1.Size = new Size(203, 25);
            label1.TabIndex = 3;
            label1.Text = "Podaj długość krawędzi:";
            // 
            // FixedEdgeLengthForm
            // 
            AcceptButton = okButton;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new Size(378, 144);
            Controls.Add(label1);
            Controls.Add(lengthNumericSelector);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "FixedEdgeLengthForm";
            Text = "Długość krawędzi";
            ((System.ComponentModel.ISupportInitialize)lengthNumericSelector).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button okButton;
        private Button cancelButton;
        private NumericUpDown lengthNumericSelector;
        private Label label1;
    }
}