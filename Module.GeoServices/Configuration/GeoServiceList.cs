using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace Modules.GeoServices
{
	public class GeoServiceList : IEnumerable, IEnumerable<IGeoService>
	{
		Dictionary<string,IGeoService> services = new Dictionary<string,IGeoService>();

		public GeoServiceList ()
		{
		}

		public void Add(IGeoService service)
		{
			if (this.services.ContainsKey(service.Name)) {
				throw new InvalidOperationException ("A service with the same name already in list.");
			}
			this.services.Add (service.Name, service);
		}

		public IGeoService this[string name]
		{
			get {
				return this.services[name];
			}
		}

		public IEnumerator GetEnumerator ()
		{
			return this.services.Values.GetEnumerator ();
		}

		IEnumerator<IGeoService> IEnumerable<IGeoService>.GetEnumerator ()
		{
			return this.services.Values.GetEnumerator ();
		}
	}
}