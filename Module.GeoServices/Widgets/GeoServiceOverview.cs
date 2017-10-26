using System;
using Xwt;
using Xwt.Drawing;
using Xwt.Formats;
using System.IO;

namespace Modules.GeoServices
{
	public class GeoServiceOverview : VBox
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

		public GeoServiceOverview ()
		{
		}

		public GeoServiceOverview (IGeoService service)
		{
			this.Service = service;
		}

		void AddChilds()
		{
			Image img;
			Stream imgstream = service.Render (service.InitializeExtents.Get ("epsg", 4326), 200, 200);
			if (imgstream != null) {
				img = Image.FromStream (imgstream);
			} else {
				img = Image.FromResource ("Modules.GeoServices.Widgets.nopreview.png");
			}
			img = img.WithBoxSize (200, 200);
			ImageView imgView = new ImageView (img);
			imgView.VerticalPlacement = WidgetPlacement.Start;

			RichTextView tview = new RichTextView ();
			tview.ExpandHorizontal = true;
			tview.LoadText (service.Description ?? "", TextFormat.Markdown);

			ScrollView scrolled = new ScrollView (tview) {
				//MinHeight = 300
			};

			//Label lbl = new Label ("skook ooooo kkkkk kkkkk kkkkk kkkkk kkkkk kkkkk kkkkk kkkkk kkkkk kkkkk kkkkk kkkkk kkkkk");
			//lbl.Wrap = WrapMode.Word;
			//lbl.ExpandVertical = false;
			//lbl.ExpandHorizontal = false;

			/*
			HPaned hpane = new HPaned ();
			hpane.Panel1.Content = imgView;
			hpane.Panel2.Content = tview;
			this.PackStart (hpane, true);
			*/
			this.PackStart (imgView);
			this.PackStart (scrolled, true);
		}

	}
}