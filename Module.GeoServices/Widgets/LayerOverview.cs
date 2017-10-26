using System;
using Xwt;
using Xwt.Drawing;
using Xwt.Formats;
using System.IO;

namespace Modules.GeoServices
{
	public class LayerOverview : VBox
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


		public LayerOverview ()
		{
		}

		public LayerOverview (ILayer layer)
		{
			this.Layer = layer;
		}

		public LayerOverview (IGeoService service, ILayer layer)
		{
			this.Service = service;
			this.Layer = layer;
		}

		void AddChilds()
		{
			this.Clear ();
			if (this.Service != null && this.Layer != null) {
				Image img;
				Stream imgstream = this.Service.Render (this.Layer.DataExtents.Get ("epsg", 4326), 200, 200, this.Layer.Name);
				if (imgstream != null) {
					img = Image.FromStream (imgstream);
				} else {
					img = Image.FromResource ("Modules.GeoServices.Widgets.nopreview.png");
				}
				img = img.WithBoxSize (200, 200);
				ImageView imgView = new ImageView (img);
				imgView.VerticalPlacement = WidgetPlacement.Start;
				this.PackStart (imgView);
			}
			if (this.Layer != null) {
				RichTextView tview = new RichTextView ();
				tview.ExpandHorizontal = true;
				tview.LoadText (this.Layer.Description ?? "Layer has no description.", TextFormat.Plain);
				this.PackStart (tview);
			}
		}
	}
}