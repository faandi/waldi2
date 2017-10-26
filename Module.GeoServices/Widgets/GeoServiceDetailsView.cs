using System;
using Xwt;

namespace Modules.GeoServices
{
	public class GeoServiceDetailsView : VBox
	{
		HPaned layerlistpane;

		IGeoService service;
		public IGeoService Service {
			get {
				return this.service;
			}
			set {
				if (value == null) {
					throw new ArgumentNullException ();
				}
				this.service = value;
				this.AddChilds ();
			}
		}

		public GeoServiceDetailsView ()
		{
		}

		public GeoServiceDetailsView (IGeoService gs)
		{
			this.Service = gs;
		}

		void AddChilds()
		{
			GeoServiceOverview ov = new GeoServiceOverview (this.Service) {
				Margin = new WidgetSpacing(10,10,10,10)
			};
			GeoServiceLayerList ll = new GeoServiceLayerList (this.Service.RootLayers);
			ll.LayerSelected += HandleLayerSelected;

			layerlistpane = new HPaned ();
			layerlistpane.Panel1.Content = ll;
			layerlistpane.Panel2.Content = new Label ("Select a service to view its details.");

			Notebook noteb = new Notebook ();
			noteb.Add (ov, "Overview");
			noteb.Add (layerlistpane, "Layers");

			this.PackStart(noteb, true);
		}

		void HandleLayerSelected (object sender, LayerEventArgs e)
		{
			LayerDetailsView ldetails = new LayerDetailsView (this.Service, e.Layer);
			this.layerlistpane.Panel2.Content = ldetails;
		}
	}
}