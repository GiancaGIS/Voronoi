using ArcMapAddinVoronoi.Forms;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Catalog;
using ESRI.ArcGIS.CatalogUI;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using LibVoronoiDiagram_ArcGISDesktop;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ArcMapAddinVoronoi
{
    /// <summary>
    /// Designer class of the dockable window add-in. It contains user interfaces that
    /// make up the dockable window.
    /// </summary>
    public partial class DockableWindowVoronoi : UserControl
    {
        private IFeatureClass _fcTarget = null;
        private IFeatureDataset _featureDataset = null;
        private readonly string _nameFcTarget = "VoronoiPoligon";
        private readonly IFeatureClassDescription _fcDescription = new FeatureClassDescriptionClass();
        private readonly IObjectClassDescription _ocDescription = null;
        private FormChooseFeatureLayer _formChooseFeatureLayer;
        private IDocument _processoArcMap = ArcMap.Application.Document;
        private IMxDocument _mxdDoc = null;
        private IMap _map = null;

        public DockableWindowVoronoi(object hook)
        {
            InitializeComponent();
            this.Hook = hook;
            this.comboBoxScelta.Enabled = false;
            this.comboBoxScelta.SelectedItem = null;
            this.comboBoxScelta.SelectedText = "--Select--";
            this.comboBoxScelta.Items.Add("Feature Layer");
            this.comboBoxScelta.Items.Add("Feature Dataset");
            this.Refresh();           
            _ocDescription = (IObjectClassDescription)_fcDescription;
        }

        private void _formChooseFeatureLayer_FeatureLayerChosenEventHandler(object sender, EventArgs e)
        {
            _fcTarget = this._formChooseFeatureLayer.FeatureLayer().FeatureClass;
            if (!this._formChooseFeatureLayer.IsDisposed) this._formChooseFeatureLayer.Dispose();
        }

        private void GetEnviromentVariables()
        {
            _processoArcMap = ArcMap.Application.Document;
            _mxdDoc = (IMxDocument)_processoArcMap;
            _map = _mxdDoc.FocusMap;
        }

        /// <summary>
        /// Host object of the dockable window
        /// </summary>
        private object Hook
        {
            get;
            set;
        }

        /// <summary>
        /// Implementation class of the dockable window add-in. It is responsible for 
        /// creating and disposing the user interface class of the dockable window.
        /// </summary>
        public class AddinImpl : ESRI.ArcGIS.Desktop.AddIns.DockableWindow
        {
            private DockableWindowVoronoi m_windowUI;
            
            public AddinImpl()
            { }

            protected override IntPtr OnCreateChild()
            {
                m_windowUI = new DockableWindowVoronoi(this.Hook);
                return m_windowUI.Handle;
            }

            protected override void Dispose(bool disposing)
            {
                if (m_windowUI != null)
                    m_windowUI.Dispose(disposing);

                base.Dispose(disposing);
            }
        }
        
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked) this.comboBoxScelta.Enabled = true;
            else
            {
                this._fcTarget = null;
                this.comboBoxScelta.Enabled = false;
            }

            this.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.GetEnviromentVariables();

            try
            {
                if (this._fcTarget is null)
                {
                    this._fcTarget = Helper.CreateInMemoryFC(this._nameFcTarget, null, esriGeometryType.esriGeometryPolygon,
                        _map.SpatialReference);
                }

                IVoronoiPolygons voronoiPolygons = new VoronoiPolygon(this._fcTarget);

                IFeatureSelection fSelection = null;
                IEnumLayer enumLayer = _map.Layers;
                ILayer2 layer = null;

                List<IPoint> listaPunti = new List<IPoint>();

                while ((layer = (ILayer2)enumLayer.Next()) != null)
                {
                    if (layer is IFeatureLayer2 fLayer && fLayer.ShapeType == esriGeometryType.esriGeometryPoint)
                    {
#if DEBUG
                        string nomeLayer = layer.Name;
#endif
                        fSelection = (IFeatureSelection)fLayer;
                        ISelectionSet2 selectioSet = (ISelectionSet2)fSelection.SelectionSet;
                        int intOggettiSelezionati = selectioSet.Count;
                        if (intOggettiSelezionati > 0)
                        {
                            selectioSet.Search(new QueryFilter() { WhereClause = "1 = 1" }, false, out ICursor cursor);

                            IFeatureCursor featureCursor = (IFeatureCursor)cursor;
                            IFeature feature = null;

                            while ((feature = featureCursor.NextFeature()) != null)
                            {
                                listaPunti.Add((IPoint)feature.ShapeCopy);
                            }
                        }
                    }
                }

                if (listaPunti.Count > 0)
                {
                    voronoiPolygons.CalculateVoronoiPolygons(listaPunti);
                    voronoiPolygons.InsertPolygonsInFc(true);
                }

                IFeatureLayer featureLayerNew = new FeatureLayer
                {
                    FeatureClass = this._fcTarget
                };

                featureLayerNew.Name = ((IDataset)this._fcTarget).Name;
                _mxdDoc.AddLayer(featureLayerNew);
                _mxdDoc.ActivatedView.Refresh();
                _mxdDoc.UpdateContents();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void comboBoxScelta_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.comboBoxScelta.SelectedIndex == 0)
                {
                    this.GetEnviromentVariables();
                    IEnumLayer enumLayer = this._map.Layers;
                    ILayer2 layer = null;
                    List<ILayer2> featureLayers = new List<ILayer2>();

                    while ((layer = (ILayer2)enumLayer.Next()) != null)
                    {
                        if (layer is IFeatureLayer2 featureLayer && featureLayer.ShapeType == esriGeometryType.esriGeometryPolygon)
                            featureLayers.Add(layer);
                    }

                    if (featureLayers.Count == 0)
                    {
                        MessageBox.Show("No Polygon Feature Layer in TOC!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    _formChooseFeatureLayer = new FormChooseFeatureLayer(featureLayers);                  
                    _formChooseFeatureLayer.FeatureLayerChosenEventHandler += _formChooseFeatureLayer_FeatureLayerChosenEventHandler;
                    _formChooseFeatureLayer.ShowDialog();
                }
                else if (this.comboBoxScelta.SelectedIndex == 1)
                {
                    IGxDialog finestra_caricatore = new GxDialog
                    {
                        AllowMultiSelect = true,
                        ButtonCaption = "Scegli GDB-SDE",
                        Title = "Scegli GDB-SDE",
                        RememberLocation = true
                    };

                    IGxObjectFilter oggetto_filtro = new GxFilterGeoDatasetsClass();
                    finestra_caricatore.ObjectFilter = oggetto_filtro; // Quindi gli sto dicendo: la finestra caricatore può filtrare 
                                                                       // solamente gli oggetti layer

                CHOOSE:
                    finestra_caricatore.DoModalOpen(ArcMap.Application.hWnd, out IEnumGxObject enumGxObject); // Vedere il libro a pagina 224 del
                                                                                                              // libro sul significato!

                    IGxObject cursore_enum = enumGxObject.Next();
                    if (cursore_enum is null) return;
                    IGxDataset gxDataset = (IGxDataset)cursore_enum;
                    try
                    {
                        this._featureDataset = (IFeatureDataset)gxDataset.Dataset;
                    }
                    catch
                    {
                        DialogResult dialogResult =
                        MessageBox.Show("Select a valid Feature Dataset!", "Attention!", MessageBoxButtons.RetryCancel);
                        if (dialogResult == DialogResult.Cancel) return;
                        goto CHOOSE;
                    }

                    try
                    {
                        IFeatureClass featureClass = ((IFeatureWorkspace)this._featureDataset.Workspace).OpenFeatureClass(this._nameFcTarget);
                        if (featureClass is null) throw new Exception("Fc existing");
                        else this._fcTarget = featureClass;
                    }
                    catch
                    {
                        IGeometryDef geometryDef = new GeometryDef();
                        IGeometryDefEdit geometryDefEdit = (IGeometryDefEdit)geometryDef;
                        geometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolygon;

                        IFields fields = new Fields();
                        IFieldsEdit fieldsEdit = (IFieldsEdit)fields;

                        IField oidField = new Field();
                        IFieldEdit oidFieldEdit = (IFieldEdit)oidField;
                        oidFieldEdit.Name_2 = "OID";
                        oidFieldEdit.Type_2 = esriFieldType.esriFieldTypeOID;
                        fieldsEdit.AddField(oidField);

                        IField geometryField = new Field();
                        IFieldEdit geometryFieldEdit = (IFieldEdit)geometryField;
                        geometryFieldEdit.Name_2 = "Shape";
                        geometryFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
                        geometryFieldEdit.GeometryDef_2 = geometryDef;
                        geometryField = geometryFieldEdit;
                        fieldsEdit.AddField(geometryField);

                        // Use IFieldChecker to create a validated fields collection.
                        IFieldChecker fieldChecker = new FieldChecker
                        {
                            ValidateWorkspace = this._featureDataset.Workspace
                        };

                        fieldChecker.Validate(fields, out _, out IFields validatedFields);

                        this._fcTarget = this._featureDataset.CreateFeatureClass(this._nameFcTarget, validatedFields,
                            _ocDescription.InstanceCLSID, _ocDescription.ClassExtensionCLSID,
                            esriFeatureType.esriFTSimple, "Shape", string.Empty);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

    }
}
