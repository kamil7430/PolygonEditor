using PolygonEditor.View.Controls;

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
            components = new System.ComponentModel.Container();
            menuStrip = new MenuStrip();
            helpToolStripMenuItem = new ToolStripMenuItem();
            libraryAlgorithmRadioButton = new RadioButton();
            bresenhamAlgorithmRadioButton = new RadioButton();
            polygonPanel = new DoubleBufferedPanel();
            vertexContextMenuStrip = new ContextMenuStrip(components);
            usuńWierzchołekToolStripMenuItem = new ToolStripMenuItem();
            ustawCiągłośćToolStripMenuItem = new ToolStripMenuItem();
            ciągłośćG0ToolStripMenuItem = new ToolStripMenuItem();
            ciągłośćC0ToolStripMenuItem = new ToolStripMenuItem();
            ciągłośćC0ToolStripMenuItem1 = new ToolStripMenuItem();
            edgeContextMenuStrip = new ContextMenuStrip(components);
            dodajWierzchołekToolStripMenuItem = new ToolStripMenuItem();
            dodajOgraniczenieToolStripMenuItem = new ToolStripMenuItem();
            krawędźPoziomaToolStripMenuItem = new ToolStripMenuItem();
            krawędźSkośnaToolStripMenuItem = new ToolStripMenuItem();
            stałaDługośćKrawędziToolStripMenuItem = new ToolStripMenuItem();
            krzywaBezieraToolStripMenuItem = new ToolStripMenuItem();
            łukOkręguToolStripMenuItem = new ToolStripMenuItem();
            usuńOgraniczenieToolStripMenuItem = new ToolStripMenuItem();
            menuStrip.SuspendLayout();
            vertexContextMenuStrip.SuspendLayout();
            edgeContextMenuStrip.SuspendLayout();
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
            polygonPanel.MouseMove += polygonPanel_MouseMove;
            polygonPanel.MouseUp += polygonPanel_MouseUp;
            // 
            // vertexContextMenuStrip
            // 
            vertexContextMenuStrip.ImageScalingSize = new Size(24, 24);
            vertexContextMenuStrip.Items.AddRange(new ToolStripItem[] { usuńWierzchołekToolStripMenuItem, ustawCiągłośćToolStripMenuItem });
            vertexContextMenuStrip.Name = "vertexContextMenuStrip";
            vertexContextMenuStrip.Size = new Size(222, 68);
            vertexContextMenuStrip.Closing += vertexContextMenuStrip_Closing;
            // 
            // usuńWierzchołekToolStripMenuItem
            // 
            usuńWierzchołekToolStripMenuItem.Name = "usuńWierzchołekToolStripMenuItem";
            usuńWierzchołekToolStripMenuItem.Size = new Size(221, 32);
            usuńWierzchołekToolStripMenuItem.Text = "Usuń wierzchołek";
            usuńWierzchołekToolStripMenuItem.Click += usuńWierzchołekToolStripMenuItem_Click;
            // 
            // ustawCiągłośćToolStripMenuItem
            // 
            ustawCiągłośćToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { ciągłośćG0ToolStripMenuItem, ciągłośćC0ToolStripMenuItem, ciągłośćC0ToolStripMenuItem1 });
            ustawCiągłośćToolStripMenuItem.Name = "ustawCiągłośćToolStripMenuItem";
            ustawCiągłośćToolStripMenuItem.Size = new Size(221, 32);
            ustawCiągłośćToolStripMenuItem.Text = "Ustaw ciągłość";
            // 
            // ciągłośćG0ToolStripMenuItem
            // 
            ciągłośćG0ToolStripMenuItem.Name = "ciągłośćG0ToolStripMenuItem";
            ciągłośćG0ToolStripMenuItem.Size = new Size(208, 34);
            ciągłośćG0ToolStripMenuItem.Text = "Ciągłość G0";
            ciągłośćG0ToolStripMenuItem.Click += ciągłośćG0ToolStripMenuItem_Click;
            // 
            // ciągłośćC0ToolStripMenuItem
            // 
            ciągłośćC0ToolStripMenuItem.Name = "ciągłośćC0ToolStripMenuItem";
            ciągłośćC0ToolStripMenuItem.Size = new Size(208, 34);
            ciągłośćC0ToolStripMenuItem.Text = "Ciągłość G1";
            ciągłośćC0ToolStripMenuItem.Click += ciągłośćC0ToolStripMenuItem_Click;
            // 
            // ciągłośćC0ToolStripMenuItem1
            // 
            ciągłośćC0ToolStripMenuItem1.Name = "ciągłośćC0ToolStripMenuItem1";
            ciągłośćC0ToolStripMenuItem1.Size = new Size(208, 34);
            ciągłośćC0ToolStripMenuItem1.Text = "Ciągłość C1";
            ciągłośćC0ToolStripMenuItem1.Click += ciągłośćC0ToolStripMenuItem1_Click;
            // 
            // edgeContextMenuStrip
            // 
            edgeContextMenuStrip.ImageScalingSize = new Size(24, 24);
            edgeContextMenuStrip.Items.AddRange(new ToolStripItem[] { dodajWierzchołekToolStripMenuItem, dodajOgraniczenieToolStripMenuItem, usuńOgraniczenieToolStripMenuItem });
            edgeContextMenuStrip.Name = "edgeContextMenuStrip";
            edgeContextMenuStrip.Size = new Size(237, 100);
            edgeContextMenuStrip.Closing += edgeContextMenuStrip_Closing;
            // 
            // dodajWierzchołekToolStripMenuItem
            // 
            dodajWierzchołekToolStripMenuItem.Name = "dodajWierzchołekToolStripMenuItem";
            dodajWierzchołekToolStripMenuItem.Size = new Size(236, 32);
            dodajWierzchołekToolStripMenuItem.Text = "Dodaj wierzchołek";
            dodajWierzchołekToolStripMenuItem.Click += dodajWierzchołekToolStripMenuItem_Click;
            // 
            // dodajOgraniczenieToolStripMenuItem
            // 
            dodajOgraniczenieToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { krawędźPoziomaToolStripMenuItem, krawędźSkośnaToolStripMenuItem, stałaDługośćKrawędziToolStripMenuItem, krzywaBezieraToolStripMenuItem, łukOkręguToolStripMenuItem });
            dodajOgraniczenieToolStripMenuItem.Name = "dodajOgraniczenieToolStripMenuItem";
            dodajOgraniczenieToolStripMenuItem.Size = new Size(236, 32);
            dodajOgraniczenieToolStripMenuItem.Text = "Ustaw ograniczenie";
            // 
            // krawędźPoziomaToolStripMenuItem
            // 
            krawędźPoziomaToolStripMenuItem.Name = "krawędźPoziomaToolStripMenuItem";
            krawędźPoziomaToolStripMenuItem.Size = new Size(337, 34);
            krawędźPoziomaToolStripMenuItem.Text = "Krawędź pozioma";
            krawędźPoziomaToolStripMenuItem.Click += krawędźPoziomaToolStripMenuItem_Click;
            // 
            // krawędźSkośnaToolStripMenuItem
            // 
            krawędźSkośnaToolStripMenuItem.Name = "krawędźSkośnaToolStripMenuItem";
            krawędźSkośnaToolStripMenuItem.Size = new Size(337, 34);
            krawędźSkośnaToolStripMenuItem.Text = "Krawędź skośna";
            krawędźSkośnaToolStripMenuItem.Click += krawędźSkośnaToolStripMenuItem_Click;
            // 
            // stałaDługośćKrawędziToolStripMenuItem
            // 
            stałaDługośćKrawędziToolStripMenuItem.Name = "stałaDługośćKrawędziToolStripMenuItem";
            stałaDługośćKrawędziToolStripMenuItem.Size = new Size(337, 34);
            stałaDługośćKrawędziToolStripMenuItem.Text = "Stała długość krawędzi";
            stałaDługośćKrawędziToolStripMenuItem.Click += stałaDługośćKrawędziToolStripMenuItem_Click;
            // 
            // krzywaBezieraToolStripMenuItem
            // 
            krzywaBezieraToolStripMenuItem.Name = "krzywaBezieraToolStripMenuItem";
            krzywaBezieraToolStripMenuItem.Size = new Size(337, 34);
            krzywaBezieraToolStripMenuItem.Text = "Krzywa Beziera 3-go stopnia";
            krzywaBezieraToolStripMenuItem.Click += krzywaBezieraToolStripMenuItem_Click;
            // 
            // łukOkręguToolStripMenuItem
            // 
            łukOkręguToolStripMenuItem.Name = "łukOkręguToolStripMenuItem";
            łukOkręguToolStripMenuItem.Size = new Size(337, 34);
            łukOkręguToolStripMenuItem.Text = "Łuk okręgu";
            łukOkręguToolStripMenuItem.Click += łukOkręguToolStripMenuItem_Click;
            // 
            // usuńOgraniczenieToolStripMenuItem
            // 
            usuńOgraniczenieToolStripMenuItem.Name = "usuńOgraniczenieToolStripMenuItem";
            usuńOgraniczenieToolStripMenuItem.Size = new Size(236, 32);
            usuńOgraniczenieToolStripMenuItem.Text = "Usuń ograniczenie";
            usuńOgraniczenieToolStripMenuItem.Click += usuńOgraniczenieToolStripMenuItem_Click;
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
            FormBorderStyle = FormBorderStyle.FixedSingle;
            KeyPreview = true;
            MainMenuStrip = menuStrip;
            MaximizeBox = false;
            Name = "PolygonsForm";
            Text = "Edytor wielokątów";
            KeyDown += PolygonsForm_KeyDown;
            KeyUp += PolygonsForm_KeyUp;
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            vertexContextMenuStrip.ResumeLayout(false);
            edgeContextMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip;
        private ToolStripMenuItem helpToolStripMenuItem;
        private RadioButton libraryAlgorithmRadioButton;
        private RadioButton bresenhamAlgorithmRadioButton;
        private DoubleBufferedPanel polygonPanel;
        private ContextMenuStrip vertexContextMenuStrip;
        private ToolStripMenuItem usuńWierzchołekToolStripMenuItem;
        private ToolStripMenuItem ustawCiągłośćToolStripMenuItem;
        private ToolStripMenuItem ciągłośćG0ToolStripMenuItem;
        private ToolStripMenuItem ciągłośćC0ToolStripMenuItem;
        private ToolStripMenuItem ciągłośćC0ToolStripMenuItem1;
        private ContextMenuStrip edgeContextMenuStrip;
        private ToolStripMenuItem dodajWierzchołekToolStripMenuItem;
        private ToolStripMenuItem dodajOgraniczenieToolStripMenuItem;
        private ToolStripMenuItem krawędźPoziomaToolStripMenuItem;
        private ToolStripMenuItem krawędźSkośnaToolStripMenuItem;
        private ToolStripMenuItem stałaDługośćKrawędziToolStripMenuItem;
        private ToolStripMenuItem krzywaBezieraToolStripMenuItem;
        private ToolStripMenuItem usuńOgraniczenieToolStripMenuItem;
        private ToolStripMenuItem łukOkręguToolStripMenuItem;
    }
}