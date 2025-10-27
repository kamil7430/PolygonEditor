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
            resetWielokątaToolStripMenuItem = new ToolStripMenuItem();
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
            diagonalDownToolStripMenuItem = new ToolStripMenuItem();
            stałaDługośćKrawędziToolStripMenuItem = new ToolStripMenuItem();
            krzywaBezieraToolStripMenuItem = new ToolStripMenuItem();
            łukOkręguToolStripMenuItem = new ToolStripMenuItem();
            labBezierZOstrzemToolStripMenuItem = new ToolStripMenuItem();
            usuńOgraniczenieToolStripMenuItem = new ToolStripMenuItem();
            zarządzanieWielokątemToolStripMenuItem = new ToolStripMenuItem();
            zapiszToolStripMenuItem = new ToolStripMenuItem();
            wczytajToolStripMenuItem = new ToolStripMenuItem();
            menuStrip.SuspendLayout();
            vertexContextMenuStrip.SuspendLayout();
            edgeContextMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.ImageScalingSize = new Size(24, 24);
            menuStrip.Items.AddRange(new ToolStripItem[] { helpToolStripMenuItem, resetWielokątaToolStripMenuItem, zarządzanieWielokątemToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Padding = new Padding(4, 1, 0, 1);
            menuStrip.Size = new Size(1084, 24);
            menuStrip.TabIndex = 0;
            menuStrip.Text = "menuStrip1";
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(57, 22);
            helpToolStripMenuItem.Text = "Pomoc";
            helpToolStripMenuItem.Click += helpToolStripMenuItem_Click;
            // 
            // resetWielokątaToolStripMenuItem
            // 
            resetWielokątaToolStripMenuItem.Name = "resetWielokątaToolStripMenuItem";
            resetWielokątaToolStripMenuItem.Size = new Size(100, 22);
            resetWielokątaToolStripMenuItem.Text = "Reset wielokąta";
            resetWielokątaToolStripMenuItem.Click += resetWielokątaToolStripMenuItem_Click;
            // 
            // libraryAlgorithmRadioButton
            // 
            libraryAlgorithmRadioButton.AutoSize = true;
            libraryAlgorithmRadioButton.Location = new Point(8, 22);
            libraryAlgorithmRadioButton.Margin = new Padding(2);
            libraryAlgorithmRadioButton.Name = "libraryAlgorithmRadioButton";
            libraryAlgorithmRadioButton.Size = new Size(142, 19);
            libraryAlgorithmRadioButton.TabIndex = 1;
            libraryAlgorithmRadioButton.TabStop = true;
            libraryAlgorithmRadioButton.Text = "Algorytm biblioteczny";
            libraryAlgorithmRadioButton.UseVisualStyleBackColor = true;
            libraryAlgorithmRadioButton.CheckedChanged += libraryAlgorithmRadioButton_CheckedChanged;
            // 
            // bresenhamAlgorithmRadioButton
            // 
            bresenhamAlgorithmRadioButton.AutoSize = true;
            bresenhamAlgorithmRadioButton.Location = new Point(161, 22);
            bresenhamAlgorithmRadioButton.Margin = new Padding(2);
            bresenhamAlgorithmRadioButton.Name = "bresenhamAlgorithmRadioButton";
            bresenhamAlgorithmRadioButton.Size = new Size(143, 19);
            bresenhamAlgorithmRadioButton.TabIndex = 2;
            bresenhamAlgorithmRadioButton.TabStop = true;
            bresenhamAlgorithmRadioButton.Text = "Algorytm Bresenhama";
            bresenhamAlgorithmRadioButton.UseVisualStyleBackColor = true;
            bresenhamAlgorithmRadioButton.CheckedChanged += bresenhamAlgorithmRadioButton_CheckedChanged;
            // 
            // polygonPanel
            // 
            polygonPanel.BackColor = SystemColors.ControlLightLight;
            polygonPanel.Location = new Point(8, 43);
            polygonPanel.Margin = new Padding(2);
            polygonPanel.Name = "polygonPanel";
            polygonPanel.Size = new Size(1065, 607);
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
            vertexContextMenuStrip.Size = new Size(167, 48);
            vertexContextMenuStrip.Closing += vertexContextMenuStrip_Closing;
            // 
            // usuńWierzchołekToolStripMenuItem
            // 
            usuńWierzchołekToolStripMenuItem.Name = "usuńWierzchołekToolStripMenuItem";
            usuńWierzchołekToolStripMenuItem.Size = new Size(166, 22);
            usuńWierzchołekToolStripMenuItem.Text = "Usuń wierzchołek";
            usuńWierzchołekToolStripMenuItem.Click += usuńWierzchołekToolStripMenuItem_Click;
            // 
            // ustawCiągłośćToolStripMenuItem
            // 
            ustawCiągłośćToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { ciągłośćG0ToolStripMenuItem, ciągłośćC0ToolStripMenuItem, ciągłośćC0ToolStripMenuItem1 });
            ustawCiągłośćToolStripMenuItem.Name = "ustawCiągłośćToolStripMenuItem";
            ustawCiągłośćToolStripMenuItem.Size = new Size(166, 22);
            ustawCiągłośćToolStripMenuItem.Text = "Ustaw ciągłość";
            // 
            // ciągłośćG0ToolStripMenuItem
            // 
            ciągłośćG0ToolStripMenuItem.Name = "ciągłośćG0ToolStripMenuItem";
            ciągłośćG0ToolStripMenuItem.Size = new Size(136, 22);
            ciągłośćG0ToolStripMenuItem.Text = "Ciągłość G0";
            ciągłośćG0ToolStripMenuItem.Click += ciągłośćG0ToolStripMenuItem_Click;
            // 
            // ciągłośćC0ToolStripMenuItem
            // 
            ciągłośćC0ToolStripMenuItem.Name = "ciągłośćC0ToolStripMenuItem";
            ciągłośćC0ToolStripMenuItem.Size = new Size(136, 22);
            ciągłośćC0ToolStripMenuItem.Text = "Ciągłość G1";
            ciągłośćC0ToolStripMenuItem.Click += ciągłośćC0ToolStripMenuItem_Click;
            // 
            // ciągłośćC0ToolStripMenuItem1
            // 
            ciągłośćC0ToolStripMenuItem1.Name = "ciągłośćC0ToolStripMenuItem1";
            ciągłośćC0ToolStripMenuItem1.Size = new Size(136, 22);
            ciągłośćC0ToolStripMenuItem1.Text = "Ciągłość C1";
            ciągłośćC0ToolStripMenuItem1.Click += ciągłośćC0ToolStripMenuItem1_Click;
            // 
            // edgeContextMenuStrip
            // 
            edgeContextMenuStrip.ImageScalingSize = new Size(24, 24);
            edgeContextMenuStrip.Items.AddRange(new ToolStripItem[] { dodajWierzchołekToolStripMenuItem, dodajOgraniczenieToolStripMenuItem, usuńOgraniczenieToolStripMenuItem });
            edgeContextMenuStrip.Name = "edgeContextMenuStrip";
            edgeContextMenuStrip.Size = new Size(177, 70);
            edgeContextMenuStrip.Closing += edgeContextMenuStrip_Closing;
            // 
            // dodajWierzchołekToolStripMenuItem
            // 
            dodajWierzchołekToolStripMenuItem.Name = "dodajWierzchołekToolStripMenuItem";
            dodajWierzchołekToolStripMenuItem.Size = new Size(176, 22);
            dodajWierzchołekToolStripMenuItem.Text = "Dodaj wierzchołek";
            dodajWierzchołekToolStripMenuItem.Click += dodajWierzchołekToolStripMenuItem_Click;
            // 
            // dodajOgraniczenieToolStripMenuItem
            // 
            dodajOgraniczenieToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { krawędźPoziomaToolStripMenuItem, krawędźSkośnaToolStripMenuItem, diagonalDownToolStripMenuItem, stałaDługośćKrawędziToolStripMenuItem, krzywaBezieraToolStripMenuItem, łukOkręguToolStripMenuItem, labBezierZOstrzemToolStripMenuItem });
            dodajOgraniczenieToolStripMenuItem.Name = "dodajOgraniczenieToolStripMenuItem";
            dodajOgraniczenieToolStripMenuItem.Size = new Size(176, 22);
            dodajOgraniczenieToolStripMenuItem.Text = "Ustaw ograniczenie";
            // 
            // krawędźPoziomaToolStripMenuItem
            // 
            krawędźPoziomaToolStripMenuItem.Name = "krawędźPoziomaToolStripMenuItem";
            krawędźPoziomaToolStripMenuItem.Size = new Size(221, 22);
            krawędźPoziomaToolStripMenuItem.Text = "Krawędź pozioma";
            krawędźPoziomaToolStripMenuItem.Click += krawędźPoziomaToolStripMenuItem_Click;
            // 
            // krawędźSkośnaToolStripMenuItem
            // 
            krawędźSkośnaToolStripMenuItem.Name = "krawędźSkośnaToolStripMenuItem";
            krawędźSkośnaToolStripMenuItem.Size = new Size(221, 22);
            krawędźSkośnaToolStripMenuItem.Text = "Krawędź skośna w górę";
            krawędźSkośnaToolStripMenuItem.Click += krawędźSkośnaToolStripMenuItem_Click;
            // 
            // diagonalDownToolStripMenuItem
            // 
            diagonalDownToolStripMenuItem.Name = "diagonalDownToolStripMenuItem";
            diagonalDownToolStripMenuItem.Size = new Size(221, 22);
            diagonalDownToolStripMenuItem.Text = "Krawędź skośna w dół";
            diagonalDownToolStripMenuItem.Click += diagonalDownToolStripMenuItem_Click;
            // 
            // stałaDługośćKrawędziToolStripMenuItem
            // 
            stałaDługośćKrawędziToolStripMenuItem.Name = "stałaDługośćKrawędziToolStripMenuItem";
            stałaDługośćKrawędziToolStripMenuItem.Size = new Size(221, 22);
            stałaDługośćKrawędziToolStripMenuItem.Text = "Stała długość krawędzi";
            stałaDługośćKrawędziToolStripMenuItem.Click += stałaDługośćKrawędziToolStripMenuItem_Click;
            // 
            // krzywaBezieraToolStripMenuItem
            // 
            krzywaBezieraToolStripMenuItem.Name = "krzywaBezieraToolStripMenuItem";
            krzywaBezieraToolStripMenuItem.Size = new Size(221, 22);
            krzywaBezieraToolStripMenuItem.Text = "Krzywa Beziera 3-go stopnia";
            krzywaBezieraToolStripMenuItem.Click += krzywaBezieraToolStripMenuItem_Click;
            // 
            // łukOkręguToolStripMenuItem
            // 
            łukOkręguToolStripMenuItem.Name = "łukOkręguToolStripMenuItem";
            łukOkręguToolStripMenuItem.Size = new Size(221, 22);
            łukOkręguToolStripMenuItem.Text = "Łuk okręgu";
            łukOkręguToolStripMenuItem.Click += łukOkręguToolStripMenuItem_Click;
            // 
            // labBezierZOstrzemToolStripMenuItem
            // 
            labBezierZOstrzemToolStripMenuItem.Name = "labBezierZOstrzemToolStripMenuItem";
            labBezierZOstrzemToolStripMenuItem.Size = new Size(221, 22);
            labBezierZOstrzemToolStripMenuItem.Text = "[Lab] Bezier z ostrzem";
            labBezierZOstrzemToolStripMenuItem.Click += labBezierZOstrzemToolStripMenuItem_Click;
            // 
            // usuńOgraniczenieToolStripMenuItem
            // 
            usuńOgraniczenieToolStripMenuItem.Name = "usuńOgraniczenieToolStripMenuItem";
            usuńOgraniczenieToolStripMenuItem.Size = new Size(176, 22);
            usuńOgraniczenieToolStripMenuItem.Text = "Usuń ograniczenie";
            usuńOgraniczenieToolStripMenuItem.Click += usuńOgraniczenieToolStripMenuItem_Click;
            // 
            // zarządzanieWielokątemToolStripMenuItem
            // 
            zarządzanieWielokątemToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { zapiszToolStripMenuItem, wczytajToolStripMenuItem });
            zarządzanieWielokątemToolStripMenuItem.Name = "zarządzanieWielokątemToolStripMenuItem";
            zarządzanieWielokątemToolStripMenuItem.Size = new Size(145, 22);
            zarządzanieWielokątemToolStripMenuItem.Text = "Zarządzanie wielokątem";
            // 
            // zapiszToolStripMenuItem
            // 
            zapiszToolStripMenuItem.Name = "zapiszToolStripMenuItem";
            zapiszToolStripMenuItem.Size = new Size(180, 22);
            zapiszToolStripMenuItem.Text = "Zapisz...";
            zapiszToolStripMenuItem.Click += zapiszToolStripMenuItem_Click;
            // 
            // wczytajToolStripMenuItem
            // 
            wczytajToolStripMenuItem.Name = "wczytajToolStripMenuItem";
            wczytajToolStripMenuItem.Size = new Size(180, 22);
            wczytajToolStripMenuItem.Text = "Wczytaj...";
            wczytajToolStripMenuItem.Click += wczytajToolStripMenuItem_Click;
            // 
            // PolygonsForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1084, 661);
            Controls.Add(polygonPanel);
            Controls.Add(bresenhamAlgorithmRadioButton);
            Controls.Add(libraryAlgorithmRadioButton);
            Controls.Add(menuStrip);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            KeyPreview = true;
            MainMenuStrip = menuStrip;
            Margin = new Padding(2);
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
        private ToolStripMenuItem diagonalDownToolStripMenuItem;
        private ToolStripMenuItem resetWielokątaToolStripMenuItem;
        private ToolStripMenuItem labBezierZOstrzemToolStripMenuItem;
        private ToolStripMenuItem zarządzanieWielokątemToolStripMenuItem;
        private ToolStripMenuItem zapiszToolStripMenuItem;
        private ToolStripMenuItem wczytajToolStripMenuItem;
    }
}