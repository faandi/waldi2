using System;
using Xwt;
using GeoAPI.CoordinateSystems;

namespace Modules.GeoServices
{
	public class LayerCrsList : VBox
	{
		DataField<string> crsNameCol = new DataField<string> ();
		DataField<string> dataExtentCol = new DataField<string> ();

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


		public LayerCrsList ()
		{
		}

		public LayerCrsList (ILayer layer)
		{
			this.Layer = layer;
		}

		void AddChilds()
		{
			ListStore store = new ListStore (crsNameCol, dataExtentCol);
			ListView list = new ListView ();
			list.DataSource = store;
			list.Columns.Add ("CRS Name", crsNameCol);
			list.Columns.Add ("Data Extent (minx,miny,maxx,maxy)", dataExtentCol);
			foreach (ICoordinateSystem c in this.layer.SupportedCrs) {
				Extent extent = this.layer.DataExtents.Get (c);
				int rowid = store.AddRow ();
				store.SetValue (rowid, crsNameCol, string.Format("{0}:{1}", c.Authority, c.AuthorityCode));
				store.SetValue (rowid, dataExtentCol, string.Format("{0},{1},{2},{3}", extent.MinX, extent.MinY, extent.MaxX, extent.MaxY));
			}
			this.PackStart(list, true);
		}
	}
}