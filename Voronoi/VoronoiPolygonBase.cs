using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using LibVoronoiDiagram_ArcGISDesktop.Engine;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;


namespace LibVoronoiDiagram_ArcGISDesktop
{
    [ClassInterface(ClassInterfaceType.None), Guid("d123de5e-46da-45c4-ba23-d0f9a46217fe"), ProgId("LibVoronoiDiagram_ArcGISDesktop.Main")]
    internal class VoronoiPolygonBase : IVoronoiPolygons
    {
        public IFeatureClass _fcTarget = null;
        public List<IGeometry> _geometrieVoronoiCalculated = null;

        public void ValidateFeatureClassTarget()
        {
            if (_fcTarget != null && _fcTarget.ShapeType != esriGeometryType.esriGeometryPolygon)
                throw new ArgumentException("The feature class must be a polygon feature class!");
        }

        /// <summary>
        /// Calculate VoronoiPolygons
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public List<IGeometry> CalculateVoronoiPolygons(List<IPoint> points)
        {
            this._geometrieVoronoiCalculated = new EngineVoronoi().CalculateVoronoiPolygons(points);
            return this._geometrieVoronoiCalculated;
        }


        /// <summary>
        /// Insert calculated Voronoi polygons in FeatureClass used in costructor method
        /// </summary>
        public void InsertPolygonsInFc()
        {
            this.InsertPolygonsInFc_engine(ref this._fcTarget);
        }

        /// <summary>
        /// Insert geometry in a specified other feature class
        /// </summary>
        /// <param name="featureClass"></param>
        public void InsertPolygonsInFc(IFeatureClass featureClass, bool useEditSession)
        {
            this.InsertPolygonsInFc_engine(ref featureClass, useEditSession);
        }


        private void InsertPolygonsInFc_engine(ref IFeatureClass featureClass, bool useEditSession=false)
        {
            if (featureClass != null) throw new ArgumentNullException("The feature class specified is not valid!");

            if (featureClass.ShapeType != esriGeometryType.esriGeometryPolygon)
                throw new ArgumentException("The feature class must be a polygon feature class!");

            if (this._geometrieVoronoiCalculated is null) return;
            if (this._geometrieVoronoiCalculated.Count == 0) return;

            IWorkspace workspace = null;
            IWorkspaceEdit workspaceEdit = null;

            if (useEditSession)
            {
                workspace = ((IDataset)featureClass).Workspace;
                workspaceEdit = (IWorkspaceEdit)workspace;
                workspaceEdit.StartEditing(false);
                workspaceEdit.StartEditOperation();
            }
            IFeatureBuffer featureBuffer = featureClass.CreateFeatureBuffer();
            IFeatureCursor featureCursor = featureClass.Insert(true);

            foreach (IGeometry geometry in this._geometrieVoronoiCalculated)
            {
                featureBuffer.Shape = geometry;
                featureCursor.InsertFeature(featureBuffer);
            }

            featureCursor.Flush();

            if (useEditSession)
            {
                workspaceEdit.StartEditOperation();
                workspaceEdit.StopEditing(true);
            }

            int reseft;
            do
            {
                reseft = Marshal.ReleaseComObject(workspace);
            }
            while (reseft > 0);
        }
    }
}