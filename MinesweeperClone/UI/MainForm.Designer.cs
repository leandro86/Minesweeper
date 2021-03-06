﻿namespace MinesweeperClone.UI
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menu = new System.Windows.Forms.MenuStrip();
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.facePicture = new System.Windows.Forms.PictureBox();
            this.gridArea = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.minePicture = new System.Windows.Forms.PictureBox();
            this.elapsedTimeLabel = new System.Windows.Forms.Label();
            this.minesLeftLabel = new System.Windows.Forms.Label();
            this.backgroundPanel = new System.Windows.Forms.Panel();
            this.menu.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.facePicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minePicture)).BeginInit();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(278, 24);
            this.menu.TabIndex = 0;
            this.menu.Text = "menuStrip1";
            // 
            // gameToolStripMenuItem
            // 
            this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameMenu,
            this.optionsMenu,
            this.toolStripSeparator1,
            this.exitMenu});
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            this.gameToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.gameToolStripMenuItem.Text = "Game";
            // 
            // newGameMenu
            // 
            this.newGameMenu.Name = "newGameMenu";
            this.newGameMenu.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.newGameMenu.Size = new System.Drawing.Size(151, 22);
            this.newGameMenu.Text = "New Game";
            this.newGameMenu.Click += new System.EventHandler(this.newGameMenu_Click);
            // 
            // optionsMenu
            // 
            this.optionsMenu.Name = "optionsMenu";
            this.optionsMenu.Size = new System.Drawing.Size(151, 22);
            this.optionsMenu.Text = "Options";
            this.optionsMenu.Click += new System.EventHandler(this.optionsMenu_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(148, 6);
            // 
            // exitMenu
            // 
            this.exitMenu.Name = "exitMenu";
            this.exitMenu.Size = new System.Drawing.Size(151, 22);
            this.exitMenu.Text = "Exit";
            this.exitMenu.Click += new System.EventHandler(this.exitMenu_Click);
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel1.BackgroundImage = global::MinesweeperClone.Properties.Resources.Background1;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.facePicture);
            this.panel1.Controls.Add(this.gridArea);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.minePicture);
            this.panel1.Controls.Add(this.elapsedTimeLabel);
            this.panel1.Controls.Add(this.minesLeftLabel);
            this.panel1.Controls.Add(this.backgroundPanel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(278, 344);
            this.panel1.TabIndex = 1;
            // 
            // facePicture
            // 
            this.facePicture.BackColor = System.Drawing.Color.Transparent;
            this.facePicture.Image = global::MinesweeperClone.Properties.Resources.PlainFace;
            this.facePicture.Location = new System.Drawing.Point(116, 3);
            this.facePicture.Name = "facePicture";
            this.facePicture.Size = new System.Drawing.Size(40, 41);
            this.facePicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.facePicture.TabIndex = 16;
            this.facePicture.TabStop = false;
            // 
            // gridArea
            // 
            this.gridArea.BackColor = System.Drawing.Color.Transparent;
            this.gridArea.Location = new System.Drawing.Point(12, 45);
            this.gridArea.Name = "gridArea";
            this.gridArea.Size = new System.Drawing.Size(254, 236);
            this.gridArea.TabIndex = 14;
            this.gridArea.TabStop = false;
            this.gridArea.Paint += new System.Windows.Forms.PaintEventHandler(this.gridArea_Paint);
            this.gridArea.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridArea_MouseDown);
            this.gridArea.MouseLeave += new System.EventHandler(this.gridArea_MouseLeave);
            this.gridArea.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gridArea_MouseMove);
            this.gridArea.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridArea_MouseUp);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = global::MinesweeperClone.Properties.Resources.Clock;
            this.pictureBox2.Location = new System.Drawing.Point(25, 9);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(30, 30);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 13;
            this.pictureBox2.TabStop = false;
            // 
            // minePicture
            // 
            this.minePicture.BackColor = System.Drawing.Color.Transparent;
            this.minePicture.Image = ((System.Drawing.Image)(resources.GetObject("minePicture.Image")));
            this.minePicture.Location = new System.Drawing.Point(197, 9);
            this.minePicture.Name = "minePicture";
            this.minePicture.Size = new System.Drawing.Size(30, 30);
            this.minePicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.minePicture.TabIndex = 12;
            this.minePicture.TabStop = false;
            // 
            // elapsedTimeLabel
            // 
            this.elapsedTimeLabel.AutoSize = true;
            this.elapsedTimeLabel.BackColor = System.Drawing.Color.Transparent;
            this.elapsedTimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.elapsedTimeLabel.Location = new System.Drawing.Point(61, 15);
            this.elapsedTimeLabel.Name = "elapsedTimeLabel";
            this.elapsedTimeLabel.Size = new System.Drawing.Size(19, 20);
            this.elapsedTimeLabel.TabIndex = 11;
            this.elapsedTimeLabel.Text = "0";
            // 
            // minesLeftLabel
            // 
            this.minesLeftLabel.AutoSize = true;
            this.minesLeftLabel.BackColor = System.Drawing.Color.Transparent;
            this.minesLeftLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minesLeftLabel.Location = new System.Drawing.Point(233, 15);
            this.minesLeftLabel.Name = "minesLeftLabel";
            this.minesLeftLabel.Size = new System.Drawing.Size(19, 20);
            this.minesLeftLabel.TabIndex = 10;
            this.minesLeftLabel.Text = "0";
            // 
            // backgroundPanel
            // 
            this.backgroundPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("backgroundPanel.BackgroundImage")));
            this.backgroundPanel.Location = new System.Drawing.Point(12, 287);
            this.backgroundPanel.Name = "backgroundPanel";
            this.backgroundPanel.Size = new System.Drawing.Size(254, 54);
            this.backgroundPanel.TabIndex = 15;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(278, 368);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MainMenuStrip = this.menu;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Minesweeper";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.facePicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minePicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label elapsedTimeLabel;
        private System.Windows.Forms.Label minesLeftLabel;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox minePicture;
        private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newGameMenu;
        private System.Windows.Forms.ToolStripMenuItem optionsMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitMenu;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.PictureBox gridArea;
        private System.Windows.Forms.Panel backgroundPanel;
        private System.Windows.Forms.PictureBox facePicture;

    }
}

