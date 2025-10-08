namespace PolygonEditor.View
{
    partial class PolygonsForm
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
            menuStrip = new MenuStrip();
            helpToolStripMenuItem = new ToolStripMenuItem();
            libraryAlgorithmRadioButton = new RadioButton();
            bresenhamAlgorithmRadioButton = new RadioButton();
            polygonPanel = new Panel();
            menuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.ImageScalingSize = new Size(24, 24);
            menuStrip.Items.AddRange(new ToolStripItem[] { helpToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(978, 33);
            menuStrip.TabIndex = 0;
            menuStrip.Text = "menuStrip1";
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(83, 29);
            helpToolStripMenuItem.Text = "Pomoc";
            helpToolStripMenuItem.Click += helpToolStripMenuItem_Click;
            // 
            // libraryAlgorithmRadioButton
            // 
            libraryAlgorithmRadioButton.AutoSize = true;
            libraryAlgorithmRadioButton.Location = new Point(12, 36);
            libraryAlgorithmRadioButton.Name = "libraryAlgorithmRadioButton";
            libraryAlgorithmRadioButton.Size = new Size(212, 29);
            libraryAlgorithmRadioButton.TabIndex = 1;
            libraryAlgorithmRadioButton.TabStop = true;
            libraryAlgorithmRadioButton.Text = "Algorytm biblioteczny";
            libraryAlgorithmRadioButton.UseVisualStyleBackColor = true;
            libraryAlgorithmRadioButton.CheckedChanged += libraryAlgorithmRadioButton_CheckedChanged;
            // 
            // bresenhamAlgorithmRadioButton
            // 
            bresenhamAlgorithmRadioButton.AutoSize = true;
            bresenhamAlgorithmRadioButton.Location = new Point(230, 36);
            bresenhamAlgorithmRadioButton.Name = "bresenhamAlgorithmRadioButton";
            bresenhamAlgorithmRadioButton.Size = new Size(213, 29);
            bresenhamAlgorithmRadioButton.TabIndex = 2;
            bresenhamAlgorithmRadioButton.TabStop = true;
            bresenhamAlgorithmRadioButton.Text = "Algorytm Bresenhama";
            bresenhamAlgorithmRadioButton.UseVisualStyleBackColor = true;
            bresenhamAlgorithmRadioButton.CheckedChanged += bresenhamAlgorithmRadioButton_CheckedChanged;
            // 
            // polygonPanel
            // 
            polygonPanel.BackColor = SystemColors.ControlLightLight;
            polygonPanel.Location = new Point(12, 71);
            polygonPanel.Name = "polygonPanel";
            polygonPanel.Size = new Size(954, 561);
            polygonPanel.TabIndex = 3;
            polygonPanel.Paint += polygonPanel_Paint;
            polygonPanel.MouseDown += polygonPanel_MouseDown;
            polygonPanel.MouseUp += polygonPanel_MouseUp;
            // 
            // PolygonsForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(978, 644);
            Controls.Add(polygonPanel);
            Controls.Add(bresenhamAlgorithmRadioButton);
            Controls.Add(libraryAlgorithmRadioButton);
            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
            Name = "PolygonsForm";
            Text = "Edytor wielokątów";
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip;
        private ToolStripMenuItem helpToolStripMenuItem;
        private RadioButton libraryAlgorithmRadioButton;
        private RadioButton bresenhamAlgorithmRadioButton;
        private Panel polygonPanel;
    }
}