using System;
using System.Collections.Generic;
using Geometries = GeoAPI.Geometries;
using CoordinateSystems = GeoAPI.CoordinateSystems;
using ProjNet.CoordinateSystems;
using System.Reflection;
using System.IO;

namespace Modules.GeoServices
{
	public abstract class GeoService : IGeoService
	{
		LayerList layers = new LayerList();
		protected static CoordinateSystemFactory crsfactory = new CoordinateSystemFactory ();

		public GeoService (string name, ServiceType servicetype)
		{
			this.Name = name;
			this.Description = "No descripton set.";
			this.ServiceType = servicetype;

			this.DataExtents = new ExtentList ();
			this.InitializeExtents = new ExtentList ();
			this.SupportedCrs = new CrsList ();
			this.RootLayers = new LayerList ();
			this.RootLayers.LayerAdded += HandleLayerAdded;
			// todo von aussen setzen ??
			// factory ??
			this.CrsStore = new WktCrsStore ();
		}

		private void HandleLayerAdded (object sender, LayerEventArgs e)
		{
			this.layers.Add(e.Layer);
			this.AddLayerListEvents (e.Layer);
		}

		private void AddLayerListEvents(ILayer layer) {
			foreach(ILayer l in layer.Childs) {
				this.layers.Add(l);
				this.AddLayerListEvents (l);
			}
			layer.Childs.LayerAdded += HandleLayerAdded;
		}

		public WktCrsStore CrsStore {
			get;
			protected set;
		}

		public ServiceType ServiceType {
			get;
			private set;
		}

		public string Name {
			get;
			protected set;
		}

		public string Title {
			get;
			set;
		}

		public string Description {
			get;
			protected set;
		}

		public string Url {
			get;
			protected set;
		}

		public LayerList RootLayers { 
			get;
			private set;
		}

		public LayerList Layers { 
			get {
				return this.layers;
			}
		}

		public CrsList SupportedCrs {
			get;
			private set;
		}

		public ExtentList DataExtents {
			get;
			private set;
		}

		public ExtentList InitializeExtents {
			get;
			private set;
		}

		public abstract void Refresh ();
		public abstract Stream Render (Extent envelope, int width, int height);
		public abstract Stream Render (Extent envelope, int width, int height, params string[] layernames);
	}
}