
namespace ArcMapAddinVoronoi.Forms
{
    partial class FormChooseFeatureLayer
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
            this.listBoxFeatureLayers = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxFeatureLayers
            // 
            this.listBoxFeatureLayers.FormattingEnabled = true;
            this.listBoxFeatureLayers.ItemHeight = 16;
            this.listBoxFeatureLayers.Location = new System.Drawing.Point(12, 12);
            this.listBoxFeatureLayers.Name = "listBoxFeatureLayers";
            this.listBoxFeatureLayers.Size = new System.Drawing.Size(529, 212);
            this.listBoxFeatureLayers.TabIndex = 0;
            this.listBoxFeatureLayers.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(359, 248);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(182, 34);
            this.button1.TabIndex = 1;
            this.button1.Text = "Select Feature Layer";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormChooseFeatureLayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 294);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBoxFeatureLayers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormChooseFeatureLayer";
            this.Text = "FormChooseFeatureLayer";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxFeatureLayers;
        private System.Windows.Forms.Button button1;
    }
}