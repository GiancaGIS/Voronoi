using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Runtime.InteropServices;

namespace LibVoronoiDiagram_ArcGISDesktop
{
    [Guid("d123de5e-46da-45c4-ba23-d0f9a46217fe")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("LibVoronoiDiagram_ArcGISDesktop.Main")]
    /// <summary>
    /// triangulation class
    /// </summary>
    public class VoronoiPolygon : VoronoiPolygonBase
    {
        /// <summary>
        /// Costruttore con input Feature Layer del layer di destinazione
        /// </summary>
        /// <param name="featureLayer"></param>
        public VoronoiPolygon(IFeatureLayer2 featureLayer)
        {
            this._fcTarget = featureLayer.FeatureClass;
            this.ValidateFeatureClassTarget();
        }

        /// <summary>
        /// Costruttore con Feature Class di target
        /// </summary>
        /// <param name="featureClass"></param>
        public VoronoiPolygon(IFeatureClass featureClass)
        {           
            this._fcTarget = featureClass;
            this.ValidateFeatureClassTarget();
        }
    }
}
