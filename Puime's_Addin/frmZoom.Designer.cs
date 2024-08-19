namespace PuimesAddin
{
    partial class frmZoom
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
            this.nUD_ZoomFactor = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_less = new System.Windows.Forms.Button();
            this.btn_more = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_ZoomFactor)).BeginInit();
            this.SuspendLayout();
            // 
            // nUD_ZoomFactor
            // 
            this.nUD_ZoomFactor.Location = new System.Drawing.Point(206, 127);
            this.nUD_ZoomFactor.Name = "nUD_ZoomFactor";
            this.nUD_ZoomFactor.Size = new System.Drawing.Size(51, 20);
            this.nUD_ZoomFactor.TabIndex = 2;
            this.nUD_ZoomFactor.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nUD_ZoomFactor.ValueChanged += new System.EventHandler(this.nUD_ZoomFactor_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(136, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Zoom factor";
            // 
            // btn_less
            // 
            this.btn_less.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_less.Image = global::PuimesAddin.Properties.Resources.less;
            this.btn_less.Location = new System.Drawing.Point(172, 23);
            this.btn_less.Name = "btn_less";
            this.btn_less.Size = new System.Drawing.Size(85, 85);
            this.btn_less.TabIndex = 1;
            this.btn_less.UseVisualStyleBackColor = true;
            this.btn_less.Click += new System.EventHandler(this.btn_less_Click);
            // 
            // btn_more
            // 
            this.btn_more.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_more.Image = global::PuimesAddin.Properties.Resources.more;
            this.btn_more.Location = new System.Drawing.Point(33, 23);
            this.btn_more.Name = "btn_more";
            this.btn_more.Size = new System.Drawing.Size(85, 85);
            this.btn_more.TabIndex = 0;
            this.btn_more.UseVisualStyleBackColor = true;
            this.btn_more.Click += new System.EventHandler(this.btn_more_Click);
            // 
            // frmZoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 166);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nUD_ZoomFactor);
            this.Controls.Add(this.btn_less);
            this.Controls.Add(this.btn_more);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "frmZoom";
            this.Text = "Zoom - Puime\'s Addin";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.nUD_ZoomFactor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_more;
        private System.Windows.Forms.Button btn_less;
        private System.Windows.Forms.NumericUpDown nUD_ZoomFactor;
        private System.Windows.Forms.Label label1;
    }
}