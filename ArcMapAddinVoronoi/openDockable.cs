using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;

namespace ArcMapAddinVoronoi
{
    public class openDockable : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        // Inizializzo classe per dockabke, con questo metodo mi permette di passargli i valori
        readonly DockableWindowVoronoi.AddinImpl classe_per_implementare_dockable =
            ESRI.ArcGIS.Desktop.AddIns.AddIn.FromID<DockableWindowVoronoi.AddinImpl>(ThisAddIn.IDs.DockableWindowVoronoi);

        // Parte di codice per mostrare la Dockable Window
        readonly UID dockableVoronoi = new UIDClass(); // UID = Unique Identifier Object.
        IDockableWindow DockableConvertitore;

        public openDockable()
        { }

        protected override void OnClick()
        {
            dockableVoronoi.Value = ThisAddIn.IDs.DockableWindowVoronoi; // Lo valorizzo con l'identificativo della Dockable
            DockableConvertitore = ArcMap.DockableWindowManager.GetDockableWindow(dockableVoronoi);
            DockableConvertitore.Show(true);
        }

        protected override void OnUpdate()
        { }
    }
}
