using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcMapAddinVoronoi
{
    public static class Helper
    {
        /// <summary>
        /// Crea una fc a partire da una lista di campi e da un tipo di geometria, ovverride del metodo precedente.
        /// </summary>
        /// <param name="fcName">Nome della fc di output</param>
        /// <param name="inFields">IFields con struttura, se null, crea solamente Attributi ObjectID e SHAPE</param>
        /// <param name="geometryType"></param>
        /// <param name="EPSG"></param>
        /// <returns></returns>
        public static IFeatureClass CreateInMemoryFC(string fcName, IFields inFields, esriGeometryType geometryType,
            ISpatialReference spatialReference)
        {
            UID CLSID = null;

            //create and open in memory workspace
            string timeStamp = DateTime.UtcNow.ToString("dd-MM-yyyy", CultureInfo.CurrentUICulture);

            //create and open in memory workspace
            IWorkspaceFactory workspaceFactory = new InMemoryWorkspaceFactory();

            IWorkspaceName workspaceName = workspaceFactory.Create(null, $"MyWorkspace_{timeStamp}", null, 0);
            IName name = (IName)workspaceName;

            // Open the workspace through the name object.
            IWorkspace workspace = (IWorkspace)name.Open();

            // assign the class id value if not assigned  
            if (CLSID is null)
            {
                CLSID = new UID
                {
                    Value = "esriGeoDatabase.Feature"
                };
            }

            // Create a fields collection for the feature class.
            IFields fields = new Fields();
            //fields = ((IClone)inFields).Clone() as IFields;

            IFieldsEdit fieldsEdit = (IFieldsEdit)fields;
            if (inFields != null)
            {
                for (int j = 0; j < inFields.FieldCount; j++)
                {
                    if (inFields.get_Field(j).Type != esriFieldType.esriFieldTypeGeometry && inFields.get_Field(j).Type != esriFieldType.esriFieldTypeOID)
                    {
                        fieldsEdit.AddField(((IClone)inFields.get_Field(j)).Clone() as IField);
                    }
                }
            }

            // Add an object ID field to the fields collection. This is mandatory for feature classes.
            IField oidField = new Field();
            IFieldEdit oidFieldEdit = (IFieldEdit)oidField;
            oidFieldEdit.Name_2 = "OID";
            oidFieldEdit.Type_2 = esriFieldType.esriFieldTypeOID;
            fieldsEdit.AddField(oidField);


            // Create a geometry definition (and spatial reference) for the feature class.
            IGeometryDef geometryDef = new GeometryDef();
            IGeometryDefEdit geometryDefEdit = (IGeometryDefEdit)geometryDef;
            geometryDefEdit.GeometryType_2 = geometryType;

            ISpatialReferenceResolution spatialReferenceResolution = (ISpatialReferenceResolution)spatialReference;
            spatialReferenceResolution.ConstructFromHorizon();
            ISpatialReferenceTolerance spatialReferenceTolerance = (ISpatialReferenceTolerance)spatialReference;
            spatialReferenceTolerance.SetDefaultXYTolerance();
            geometryDefEdit.SpatialReference_2 = spatialReference;
            geometryDef = geometryDefEdit;

            // Add a geometry field to the fields collection. This is where the geometry definition is applied.
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
                ValidateWorkspace = workspace
            };
            fieldChecker.Validate(fields, out _, out IFields validatedFields);

            // The enumFieldError enumerator can be inspected at this point to determine 
            // which fields were modified during validation.

            // Create the feature class. Note that the CLSID parameter is null - this indicates to use the
            // default CLSID, esriGeodatabase.Feature (acceptable in most cases for feature classes).
            IFeatureClass featureClass = ((IFeatureWorkspace)workspace).CreateFeatureClass(fcName, validatedFields,
              null, CLSID, esriFeatureType.esriFTSimple, "Shape", "");

            return featureClass;
        }
    }
}
