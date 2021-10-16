using ESRI.ArcGIS.Carto;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ArcMapAddinVoronoi.Forms
{
    public partial class FormChooseFeatureLayer : Form
    {
        private IFeatureLayer2 _fLayerChosen = null;
        public event EventHandler FeatureLayerChosenEventHandler;

        /// <summary>
        /// Costruttore della form
        /// </summary>
        public FormChooseFeatureLayer(List<ILayer2> listFeatureLayerName)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.listBoxFeatureLayers.DisplayMember = "Name";
            this.listBoxFeatureLayers.ValueMember = "FeatureLayer";

            foreach (ILayer2 layer in listFeatureLayerName)
            {
                this.listBoxFeatureLayers.Items.Add(new FeatureLayerItemListBox()
                {
                    Name = layer.Name,
                    FeatureLayer = (IFeatureLayer2)layer
                });
            }
            this.Refresh();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._fLayerChosen = ((FeatureLayerItemListBox)this.listBoxFeatureLayers.SelectedItem).FeatureLayer;
        }

        public IFeatureLayer2 FeatureLayer()
        {
            return this._fLayerChosen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.FeatureLayerChosenEventHandler?.Invoke(this, e);
        }
    }

    public class FeatureLayerItemListBox
    {
        public string Name { get; set; }
        public IFeatureLayer2 FeatureLayer { get; set; }
    }
}
