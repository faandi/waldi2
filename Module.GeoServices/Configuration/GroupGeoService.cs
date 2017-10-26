using System;
using System.Collections.Generic;
using Geometries = GeoAPI.Geometries;
using CoordinateSystems = GeoAPI.CoordinateSystems;
using ProjNet.CoordinateSystems;
using System.IO;

namespace Modules.GeoServices
{
	public class GroupGeoService : IGeoService
	{
		ExtentList initializeExtents;
		ExtentList fullExtents;
		CoordinateSystems.ICoordinateSystem[] supportedCrs;
		CoordinateSystemFactory factory = new CoordinateSystemFactory ();
		string url;
		LayerList layers = new LayerList ();
		List<IGeoService> services = new List<IGeoService> ();

		public GroupGeoService ()
		{
			throw new NotImplementedException ();
		}

		public List<IGeoService> Services {
			get {
				return this.services;
			}
		}

		public ServiceType ServiceType
		{
			get {
				return ServiceType.Group;
			}
		}

		public string Name {
			get;
			set;
		}

		public string Title {
			get;
			set;
		}

		public string Description {
			get {
				return "This Service is a dummy Service, usefull for testing.";
			}
		}

		public string Url {
			get {
				return this.url;
			}
		}

		public LayerList Layers { 
			get {
				return this.layers;
			}
		}

		public CoordinateSystems.ICoordinateSystem[] SupportedCrs {
			get {
				return this.supportedCrs;
			}
		}

		public ExtentList DataExtents {
			get {
				return this.fullExtents;
			}
		}

		public ExtentList InitializeExtents {
			get {
				return this.initializeExtents;
			}
		}

		public void Refresh ()
		{
			throw new NotImplementedException ();
		}

		public Stream Render(Extent envelope, int width, int height)
		{
			throw new NotImplementedException ();
		}

		public Stream Render(Extent envelope, int width, int height, params string[] layernames)
		{
			throw new NotImplementedException ();
		}
	}
}