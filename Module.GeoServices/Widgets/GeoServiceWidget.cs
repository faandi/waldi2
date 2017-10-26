using System;
using Xwt;
using Xwt.Drawing;
//using System.Xml;
using Modules.GeoServices;

namespace Modules.GeoServices
{
	public class GeoServiceWidget : HPaned
	{
		public GeoServiceWidget ()
		{
			/*
			DummyGeoService s1 = (DummyGeoService)ServiceFactory.ServiceFromUrl ("dummy");
			s1.Name = "dummy1";
			s1.Title = "dummy1";
			DummyGeoService s2 = (DummyGeoService)ServiceFactory.ServiceFromUrl ("dummy");
			s2.Name = "dummy2";
			s2.Title = "dummy2";
			DummyGeoService s3 = (DummyGeoService)ServiceFactory.ServiceFromUrl ("dummy");
			s3.Name = "dummy3";
			s3.Title = "dummy3";

			GeoServiceList services = new GeoServiceList() {
				s1,
				s2,
				s3
			};
			*/

			GeoServiceList services = new GeoServiceList();

			// left
			GeoServiceListView list = new GeoServiceListView (services);
			list.ReadOnly = false;
			list.ServiceSelected += HandleServiceSelected;
			this.Panel1.Content = list;
			// right
			Label label = new Label ("No service selected.");
			this.Panel2.Content = label;
			// general
			this.Position = 160;
		}

		void HandleServiceSelected(object sender, GeoServiceServiceEventArgs e)
		{
			this.Panel2.Content.Dispose ();
			//GeoServiceSetting lset = new GeoServiceSetting ();
			//lset.SetLayer (e.Service);
			//this.Panel2.Content = lset;
			this.Panel2.Content = new GeoServiceDetailsView (e.Service);
		}

		protected override void Dispose (bool disposing)
		{
			base.Dispose (disposing);
		}
	}
}