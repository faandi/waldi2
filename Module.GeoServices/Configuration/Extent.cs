using System;
using GeoAPI.Geometries;
using GeoAPI.CoordinateSystems;

namespace Modules.GeoServices
{
	public class Extent : Envelope
	{
		public Extent (double x1, double x2, double y1, double y2, ICoordinateSystem crs) : base (x1,x2,y1,y2)
		{
			this.Crs = crs;
		}

		public Extent (Envelope envelope, ICoordinateSystem crs) : base (envelope)
		{
			this.Crs = crs;
		}

		public ICoordinateSystem Crs { get; private set; }
	}
}