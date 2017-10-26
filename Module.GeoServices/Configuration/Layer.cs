using System;
using System.Collections.Generic;
using CoordinateSystems = GeoAPI.CoordinateSystems;

namespace Modules.GeoServices
{
	public class Layer : ILayer
	{
		public Layer (string name) : this(name,name)
		{
		}

		public Layer (string name, string title)
		{
			this.Name = name;
			this.Title = title;
			this.SupportedCrs = new CrsList ();
			this.DataExtents = new ExtentList ();
			this.Childs = new LayerList ();
			this.Childs.LayerAdded += HandleChildAdded;
		}

		private void HandleChildAdded (object sender, LayerEventArgs e)
		{
			e.Layer.Parent = this;
		}

		public void AddExtents(params Extent[] extent)
		{
			this.AddExtents (extent as IEnumerable<Extent>);
		}

		public void AddExtents(IEnumerable<Extent> extents)
		{
			foreach (Extent e in extents) {
				if (this.SupportedCrs.Get (e.Crs.Authority, e.Crs.AuthorityCode) == null) {
					this.SupportedCrs.Add (e.Crs);
				}
				this.DataExtents.Add (e);
			}
		}


		#region ILayer implementation

		public string Name {
			get;
			private set;
		}

		public string Title {
			get;
			set;
		}

		public string Description {
			get;
			set;
		}

		public CrsList SupportedCrs {
			get;
			private set;
		}

		public ExtentList DataExtents {
			get;
			private set;
		}

		public int MinScale {
			get;
			set;
		}

		public int MaxScale {
			get;
			set;
		}

		public bool Visible {
			get;
			set;
		}

		public LayerList Childs {
			get;
			private set;
		}

		public ILayer Parent {
			get;
			set;
		}

		#endregion
	}
}