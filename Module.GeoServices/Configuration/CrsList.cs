using System;
using System.Linq;
using GeoAPI.CoordinateSystems;
using System.Collections.Generic;
using System.Collections;

namespace Modules.GeoServices
{
	public class CrsList : IEnumerable, IEnumerable<ICoordinateSystem>
	{
		List<ICoordinateSystem> crslist = new List<ICoordinateSystem>();

		public CrsList ()
		{
		}

		public void Add(params ICoordinateSystem[] crslist)
		{
			this.Add (crslist as IEnumerable<ICoordinateSystem>);
		}

		public void Add(IEnumerable<ICoordinateSystem> crslist)
		{
			foreach (ICoordinateSystem crs in crslist) {
				ICoordinateSystem c = this.Get (crs.Authority, crs.AuthorityCode);
				if (c != null) {
					throw new InvalidOperationException ("An Coordinatesystem with the same Authority and Code already exists.");
				}
				this.crslist.Add (crs);
			}
		}

		public ICoordinateSystem Get(string authorityname, long authoritycode)
		{
			return this.crslist.Where (e => e.Authority == authorityname).Where (e => e.AuthorityCode == authoritycode).FirstOrDefault ();
		}

		public IEnumerator GetEnumerator ()
		{
			return this.crslist.GetEnumerator ();
		}

		IEnumerator<ICoordinateSystem> IEnumerable<ICoordinateSystem>.GetEnumerator ()
		{
			return this.crslist.GetEnumerator ();
		}
	}
}