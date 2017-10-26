using System;
using System.Linq;
using System.Collections.Generic;

namespace Modules.GeoServices
{
	public static class ServiceFactory
	{
		public static IGeoService ServiceFromUrl(string url, ServiceType type = ServiceType.Auto)
		{
			if (string.IsNullOrEmpty (url)) {
				throw new ArgumentNullException ("url");
			}
			Dictionary<ServiceType,int> scores = new Dictionary<ServiceType,int> (){
				{ ServiceType.GeoServicesRestApi, 0 },
				{ ServiceType.SlippyMap, 0 },
				{ ServiceType.Wms, 0 }
			};
			// TODO more guess logic here
			string lowerurl = url.ToLowerInvariant ();
			// wms
			if (lowerurl.Contains ("service=wms")) {
				scores [ServiceType.Wms] += 70;
			}
			if (lowerurl.Contains ("wms?")) {
				scores [ServiceType.Wms] += 20;
			}
			if (lowerurl.Contains ("wms")) {
				scores [ServiceType.Wms] += 10;
			}
			// SlippyMap
			if (lowerurl.Contains ("{x}")) {
				scores [ServiceType.SlippyMap] += 15;
			}
			if (lowerurl.Contains ("{y}")) {
				scores [ServiceType.SlippyMap] += 15;
			}
			if (lowerurl.Contains ("{z}")) {
				scores [ServiceType.SlippyMap] += 15;
			}
			// GeoServicesRestApi
			if (lowerurl.Contains ("/rest/services/")) {
				scores [ServiceType.GeoServicesRestApi] += 50;
			}
			// GeoServicesRestApi
			if (lowerurl.Contains ("mapserver")) {
				scores [ServiceType.GeoServicesRestApi] += 15;
			}

			ServiceType stype = scores.OrderByDescending (x => x.Value).First().Key;
			if (scores [stype] < 1) {
				throw new Exception ("Could not detect service type from url.");
			}
			switch (stype) {
				case ServiceType.Wms:
					return new WmsGeoService (url);
				case ServiceType.SlippyMap:
					return new DummyGeoService("SlippyMap"){
						Title = "SlippyMap"
					};
				case ServiceType.GeoServicesRestApi:
					return new DummyGeoService("GeoServicesRestApi"){
						Title = "GeoServicesRestApi"
					};
			}

			throw new Exception ("Service Type not supported.");
		}
	}
}