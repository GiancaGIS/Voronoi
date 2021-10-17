
namespace ArcMapAddinVoronoi
{
    partial class DockableWindowVoronoi
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
            this.buttonGenerateVoronoiPolygons = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.comboBoxScelta = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // buttonGenerateVoronoiPolygons
            // 
            this.buttonGenerateVoronoiPolygons.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonGenerateVoronoiPolygons.Location = new System.Drawing.Point(59, 207);
            this.buttonGenerateVoronoiPolygons.Name = "buttonGenerateVoronoiPolygons";
            this.buttonGenerateVoronoiPolygons.Size = new System.Drawing.Size(114, 111);
            this.buttonGenerateVoronoiPolygons.TabIndex = 0;
            this.buttonGenerateVoronoiPolygons.Text = "Generate Voronoi Polygons";
            this.buttonGenerateVoronoiPolygons.UseVisualStyleBackColor = true;
            this.buttonGenerateVoronoiPolygons.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(19, 54);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(177, 21);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "Save Voronoi Polygons";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // comboBoxScelta
            // 
            this.comboBoxScelta.AutoCompleteCustomSource.AddRange(new string[] {
            "Feature Layer",
            "Feature Dataset"});
            this.comboBoxScelta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBoxScelta.FormattingEnabled = true;
            this.comboBoxScelta.Location = new System.Drawing.Point(19, 123);
            this.comboBoxScelta.Name = "comboBoxScelta";
            this.comboBoxScelta.Size = new System.Drawing.Size(209, 24);
            this.comboBoxScelta.TabIndex = 2;
            this.comboBoxScelta.SelectedIndexChanged += new System.EventHandler(this.comboBoxScelta_SelectedIndexChanged);
            // 
            // DockableWindowVoronoi
            // 
            this.Controls.Add(this.comboBoxScelta);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.buttonGenerateVoronoiPolygons);
            this.Name = "DockableWindowVoronoi";
            this.Size = new System.Drawing.Size(298, 594);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonGenerateVoronoiPolygons;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ComboBox comboBoxScelta;
    }
}
