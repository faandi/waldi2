using System;
using System.Linq;
using System.Collections.Generic;
using CoordinateSystems = GeoAPI.CoordinateSystems;
using System.Collections;

namespace Modules.GeoServices
{
	public class ExtentList : IEnumerable, IEnumerable<Extent>
	{
		List<Extent> extents = new List<Extent>();

		public ExtentList ()
		{
		}

		public void Add(params Extent[] extentlist)
		{
			this.Add (extentlist as IEnumerable<Extent>);
		}

		public void Add(IEnumerable<Extent> extentlist)
		{
			foreach (Extent extent in extentlist) {
				if (this.extents.Where (e => e.Crs == extent.Crs).Count () > 0) {
					throw new InvalidOperationException ("An extent with the same CRS already in list.");
				}
				this.extents.Add (extent);
			}
		}

		public Extent Get(CoordinateSystems.ICoordinateSystem crs)
		{
			return this.extents.Where (e => e.Crs.EqualParams(crs)).FirstOrDefault ();
		}

		public Extent Get(string authorityname, int authoritycode)
		{
			return this.extents.Where (e => (e.Crs.Authority == authorityname && e.Crs.AuthorityCode == authoritycode)).FirstOrDefault ();
		}

		public IEnumerator GetEnumerator ()
		{
			return this.extents.GetEnumerator ();
		}

		IEnumerator<Extent> IEnumerable<Extent>.GetEnumerator ()
		{
			return this.extents.GetEnumerator ();
		}
	}
}