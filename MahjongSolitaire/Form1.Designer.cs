namespace MahjongSolitaire
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.TileCount = new System.Windows.Forms.Label();
            this.avlpairs = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TileCount
            // 
            this.TileCount.AutoSize = true;
            this.TileCount.Location = new System.Drawing.Point(805, 674);
            this.TileCount.Name = "TileCount";
            this.TileCount.Size = new System.Drawing.Size(61, 13);
            this.TileCount.TabIndex = 0;
            this.TileCount.Text = "No of Tiles:";
            // 
            // avlpairs
            // 
            this.avlpairs.AutoSize = true;
            this.avlpairs.Location = new System.Drawing.Point(676, 674);
            this.avlpairs.Name = "avlpairs";
            this.avlpairs.Size = new System.Drawing.Size(79, 13);
            this.avlpairs.TabIndex = 1;
            this.avlpairs.Text = "Available Pairs:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(944, 712);
            this.Controls.Add(this.avlpairs);
            this.Controls.Add(this.TileCount);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TileCount;
        private System.Windows.Forms.Label avlpairs;
    }
}

