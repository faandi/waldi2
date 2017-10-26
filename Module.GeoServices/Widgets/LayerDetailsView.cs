using System;
using Xwt;

namespace Modules.GeoServices
{
	public class LayerDetailsView : VBox
	{
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

		ILayer layer;
		public ILayer Layer {
			get {
				return this.layer;
			}
			set {
				if (value == null) {
					throw new ArgumentNullException ();
				}
				this.layer = value;
				this.AddChilds ();
			}
		}


		public LayerDetailsView ()
		{
		}

		public LayerDetailsView (IGeoService gs, ILayer layer)
		{
			this.Service = gs;
			this.Layer = layer;
		}

		void AddChilds()
		{
			this.Clear ();
			Notebook noteb = new Notebook ();
			if (this.Layer != null && this.Service != null) {
				LayerOverview ov = new LayerOverview () {
					Margin = new WidgetSpacing(10,10,10,10),
					Service = this.Service,
					Layer = this.Layer
				};
				noteb.Add (ov, "Overview");
			}
			if (this.Layer != null) {
				LayerCrsList crs = new LayerCrsList (this.Layer);
				noteb.Add (crs, "Coordinate Systems");
			}
			this.PackStart(noteb, true);
		}
	}
}