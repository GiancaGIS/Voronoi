
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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.comboBoxScelta = new System.Windows.Forms.ComboBox();
            this.buttonGenerateVoronoiPolygons = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(11, 23);
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
            this.comboBoxScelta.Location = new System.Drawing.Point(11, 60);
            this.comboBoxScelta.Name = "comboBoxScelta";
            this.comboBoxScelta.Size = new System.Drawing.Size(209, 24);
            this.comboBoxScelta.TabIndex = 2;
            this.comboBoxScelta.SelectedIndexChanged += new System.EventHandler(this.comboBoxScelta_SelectedIndexChanged);
            // 
            // buttonGenerateVoronoiPolygons
            // 
            this.buttonGenerateVoronoiPolygons.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonGenerateVoronoiPolygons.Location = new System.Drawing.Point(54, 142);
            this.buttonGenerateVoronoiPolygons.Name = "buttonGenerateVoronoiPolygons";
            this.buttonGenerateVoronoiPolygons.Size = new System.Drawing.Size(114, 111);
            this.buttonGenerateVoronoiPolygons.TabIndex = 0;
            this.buttonGenerateVoronoiPolygons.Text = "Generate Voronoi Polygons";
            this.buttonGenerateVoronoiPolygons.UseVisualStyleBackColor = true;
            this.buttonGenerateVoronoiPolygons.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonGenerateVoronoiPolygons);
            this.groupBox1.Controls.Add(this.comboBoxScelta);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Location = new System.Drawing.Point(12, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(237, 267);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Voronoi Polygons";
            // 
            // DockableWindowVoronoi
            // 
            this.Controls.Add(this.groupBox1);
            this.Name = "DockableWindowVoronoi";
            this.Size = new System.Drawing.Size(266, 594);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ComboBox comboBoxScelta;
        private System.Windows.Forms.Button buttonGenerateVoronoiPolygons;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
