using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System.Collections.Generic;


namespace LibVoronoiDiagram_ArcGISDesktop
{
    internal interface IVoronoiPolygons
    {
        List<IGeometry> CalculateVoronoiPolygons(List<IPoint> points);

        void InsertPolygonsInFc();

        void InsertPolygonsInFc(IFeatureClass featureClass, bool useEditSession);
    }
}
