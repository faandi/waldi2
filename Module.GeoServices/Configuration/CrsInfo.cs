using System;
using GeoAPI.CoordinateSystems;

namespace Modules.GeoServices
{
	public class CrsInfo : IInfo
	{
		public CrsInfo ()
		{
		}

		#region IInfo implementation

		public bool EqualParams (object obj)
		{
			throw new NotImplementedException ();
		}

		public string Name {
			get {
				throw new NotImplementedException ();
			}
		}

		public string Authority {
			get {
				throw new NotImplementedException ();
			}
		}

		public long AuthorityCode {
			get {
				throw new NotImplementedException ();
			}
		}

		public string Alias {
			get {
				throw new NotImplementedException ();
			}
		}

		public string Abbreviation {
			get {
				throw new NotImplementedException ();
			}
		}

		public string Remarks {
			get {
				throw new NotImplementedException ();
			}
		}

		public string WKT {
			get {
				throw new NotImplementedException ();
			}
		}

		public string XML {
			get {
				throw new NotImplementedException ();
			}
		}

		#endregion
	}
}

