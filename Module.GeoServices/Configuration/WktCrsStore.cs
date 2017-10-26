using System;
using System.Collections.Generic;
using System.Linq;
using GeoAPI.CoordinateSystems;
using ProjNet.CoordinateSystems;
using System.IO;
using System.Net;

namespace Modules.GeoServices
{
	// CommonCrsStore
	// CsvCrsStore
	// WebCrsStore (http://www.spatialreference.org/ref/epsg/2006/ogcwkt/)
	public class WktCrsStore
	{
		Dictionary<int,ICoordinateSystem> coordinateSystems = new Dictionary<int,ICoordinateSystem> ();
		CoordinateSystemFactory factory = new CoordinateSystemFactory ();

		public WktCrsStore ()
		{

		}

		public void ReadTextFile (string filepath)
		{
			using (StreamReader file = new System.IO.StreamReader(filepath)) {
				string line;
				while ((line = file.ReadLine()) != null) {
					ICoordinateSystem crs = factory.CreateFromWkt (line);
					int srid = coordinateSystems.Count + 1;
					this.coordinateSystems.Add (srid, crs);
				}
			}
		}

		public ICoordinateSystem ReadFromSrOrg(int srid, string authority, int authoritycode)
		{
			authority = authority.ToLowerInvariant();
			string[] supportedAuthority = new string[] { 
				"epsg", "esri", "iau2000", "sr-org"
			};
			if (!supportedAuthority.Contains(authority)) {
				throw new Exception ("Authority " + authority + " not supported.");
			}

			string url = string.Format ("http://www.spatialreference.org/ref/{0}/{1}/ogcwkt/", authority, authoritycode);
			WebRequest request = WebRequest.Create (url);
			request.Proxy = null;

			try {
				using (WebResponse response = request.GetResponse ())
				using (Stream dataStream = response.GetResponseStream ())
				using (StreamReader reader = new StreamReader (dataStream)) {
					string wkt = reader.ReadToEnd ();
					ICoordinateSystem crs = factory.CreateFromWkt (wkt);
					this.coordinateSystems.Add (srid, crs);
					return crs;
				}			
			}
			catch(WebException) {
				return null;
			}
		}

		public ICoordinateSystem Find(int srid)
		{
			if (this.coordinateSystems.ContainsKey (srid)) {
				return this.coordinateSystems [srid];
			}
			return null;
		}

		public ICoordinateSystem Find(string authority, int authoritycode)
		{
			authority = authority.ToLowerInvariant();
			ICoordinateSystem coordsys = this.coordinateSystems.Values.Where (c => c.Authority == "epsg").Where (c => c.AuthorityCode == authoritycode).FirstOrDefault ();
			if (coordsys == null) {
				coordsys = this.ReadFromSrOrg (this.coordinateSystems.Count, authority, authoritycode);
			}
			return coordsys;
			//this.coordinateSystems[0].Authority
			//return this.coordinateSystems.Values.Where (c => c.Authority == "EPSG").FirstOrDefault ();
		}
	}
}