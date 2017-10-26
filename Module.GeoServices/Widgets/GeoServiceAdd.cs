using System;
using Xwt;
using Xwt.Drawing;
using System.Collections.Generic;

namespace Modules.GeoServices
{
	public class GeoServiceAdd : HBox
	{
		public event EventHandler FinishRequested;
		public event EventHandler<GeoServiceServiceEventArgs> ServiceCreated;

		public GeoServiceAdd ()
		{
			this.ShowBasicSettings ();
		}

		void ShowBasicSettings(IGeoService gs = null)
		{
			this.Clear ();

			ComboBox typeCombo = new ComboBox ();
			typeCombo.Items.Add (ServiceType.Auto, "Auto");
			typeCombo.Items.Add (ServiceType.Wms, "WMS");
			typeCombo.Items.Add (ServiceType.SlippyMap, "SlippyMap");
			typeCombo.Items.Add (ServiceType.GeoServicesRestApi, "GeoServicesRestApi");
			if (gs != null && typeCombo.Items.Contains (gs.ServiceType)) {
				typeCombo.SelectedItem = gs.ServiceType;
			} else {
				typeCombo.SelectedItem = ServiceType.Auto;
			}

			TextEntry urlEntry = new TextEntry () {
				Text = gs == null ? "http://vmap0.tiles.osgeo.org/wms/vmap0" : gs.Url,
				MinWidth = 300
			};

			Button cancel = new Button ("Cancel");
			cancel.Clicked += (object sender, EventArgs e) => {
				if (FinishRequested != null) {
					FinishRequested(this, new EventArgs() { });
				}
			};
			Button connect = new Button ("Connect") { 
				ExpandHorizontal = false
			};
			connect.Clicked += (object sender, EventArgs e) => {
				ServiceType st = (ServiceType)typeCombo.SelectedItem;
				Connect(urlEntry.Text, st);
			};

			Table t = new Table ();
			t.Add (new Label ("Add GeoService"), 0, 0, 
			       colspan:2, hexpand:true, vexpand:true, hpos: WidgetPlacement.Center
			       );
			//t.Add (new Label ("One:"), 0, 0);
			//t.Add (new TextEntry (), 1, 0);
			t.Add (new Label ("URL"), 0, 1);
			t.Add (urlEntry, 1, 1);
			t.Add (new Label ("Type"), 0, 2);
			t.Add (typeCombo, 1, 2);
			t.Add (cancel, 0, 3);
			t.Add (connect, 1, 3);

			this.PackStart(t);
		}

		void Connect(string url, ServiceType type)
		{
			/*
			HPaned hpane = new HPaned ();
			hpane.Panel1.Content = new Spinner () { 
				Animate = true
			};
			hpane.Panel2.Content = new Label ("Connecting to service");
			Dialog load = new Dialog ();
			load.Content = hpane;
			load.Run (this.ParentWindow);
			*/
			IGeoService gs = ServiceFactory.ServiceFromUrl (url, type);
			ShowSummary (gs);
		}

		void ShowSummary(IGeoService gs)
		{
			this.Clear ();

			TextEntry nameentry = new TextEntry ();
			nameentry.Text = gs.Title;

			Table t = new Table ();
			t.Add (new Label ("GeoService Summary"), 0, 0, 
			       colspan:2, hexpand:true, vexpand:true, hpos: WidgetPlacement.Center
			       );
			t.Add (new Label ("URL"), 0, 1, hpos: WidgetPlacement.End );
			t.Add (new Label (gs.Url), 1, 1);
			t.Add (new Label ("Type"), 0, 2, hpos: WidgetPlacement.End);
			t.Add (new Label (gs.ServiceType.ToString()), 1, 2);
			t.Add (new Label ("Name"), 0, 3, hpos: WidgetPlacement.End);
			t.Add (nameentry, 1, 3);
			//t.Add (cancel, 0, 3);
			//t.Add (connect, 1, 3);

			GeoServiceDetailsView gsdetails = new GeoServiceDetailsView (gs);

			VPaned pane1 = new VPaned ();
			pane1.Panel1.Content = t;
			pane1.Panel2.Content = gsdetails;

			Button back = new Button ("Back");
			back.Clicked += (object sender, EventArgs e) => {
				this.ShowBasicSettings (gs);
			};

			Button addservice = new Button ("Add Service");
			addservice.Clicked += (object sender, EventArgs e) => {
				// TODO save service name (from text entry)
				if (this.ServiceCreated != null) {
					this.ServiceCreated(this, new GeoServiceServiceEventArgs(gs));
				}
			};

			HPaned hpane = new HPaned ();
			hpane.Panel1.Content = back;
			hpane.Panel2.Content = addservice;

			VPaned pane2 = new VPaned ();
			pane2.Panel1.Content = pane1;
			pane2.Panel2.Content = hpane;

			this.PackStart (pane2, true);
		}



	}
}