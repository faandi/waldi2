using System;
using System.Collections.Generic;
using Geometries = GeoAPI.Geometries;
using CoordinateSystems = GeoAPI.CoordinateSystems;
using ProjNet.CoordinateSystems;
using System.Reflection;
using System.IO;
using SharpMap.Layers;
using SharpMap.Web.Wms;
using WmsClient = SharpMap.Web.Wms.Client;

namespace Modules.GeoServices
{
	public class WmsGeoService : GeoService
	{
		//string capabilitiesurl;
		WmsLayer smlayer;

		// TODO: SRS ohne BoundingBox -> Transformation von WGS (latlong)
		// TODO: wktsrsstore aus csv file !!

		// min/maxscale mit <ScaleHint>
		// siehe http://wiki.deegree.org/deegreeWiki/HowToUseScaleHintAndScaleDenominator#What_is_the_ScaleHint.3F

		//public WmsGeoService (byte[] capabilitiesdoc) : base("unnamed", ServiceType.Wms)
		//{
		//
		//}

		public WmsGeoService (string capabilitiesurl) : base("unnamed", ServiceType.Wms)
		{
			this.smlayer = new WmsLayer ("wms", capabilitiesurl);

			this.Name = this.smlayer.RootLayer.Name;
			this.Title = this.smlayer.ServiceDescription.Title ?? this.smlayer.RootLayer.Title;
			this.Description = this.BuildDescription(this.smlayer);
			this.SupportedCrs.Add(this.ReadCrs (this.smlayer.RootLayer.CRS));
			this.DataExtents.Add(this.ReadExtents (this.smlayer.RootLayer.SRIDBoundingBoxes));
			// no initialize extens in WMS capabilities
			this.InitializeExtents.Add (this.DataExtents);
			List<Layer> rootlayers = this.ReadLayers (this.smlayer.RootLayer.ChildLayers);
			this.RootLayers.Add (rootlayers);
		}

		private string BuildDescription(WmsLayer wmslayer)
		{
			var description = wmslayer.ServiceDescription;
			string d = string.Empty;
			d += "### Abstract\n\n";
			d += description.Abstract ?? this.smlayer.RootLayer.Abstract ?? "no information";
			d += "\n\n### Contact Information\n\n";
			d += this.BuildContactInformation(description.ContactInformation);
			d += "\n\n### Access Constraints\n\n";
			d += description.AccessConstraints ?? "no information";
			d += "\n\n### Fees\n\n";
			d += description.Fees ?? "no information";
			d += "\n\n### Keywords\n\n";
			d +=  description.Keywords.Length > 0 ? string.Join (",", description.Keywords) : "no information";
			d += "\n\n### Layer Limit\n\n";
			d += description.LayerLimit != 0 ? description.LayerLimit.ToString() : "no information";
			d += "\n\n### Max image dimensions\n\n";
			d += description.MaxWidth > 0 && description.MaxHeight > 0 ? description.MaxWidth + "x" + description.MaxHeight : "no information";
			return d;
		}

		private string BuildContactInformation(Capabilities.WmsContactInformation wmsinfo)
		{
			string d = string.Empty;
			d += wmsinfo.PersonPrimary.Person ?? "no information";
			return d;
		}

		private List<Layer> ReadLayers(WmsClient.WmsServerLayer[] wmslayers)
		{
			List<Layer> layers = new List<Layer> ();
			foreach (WmsClient.WmsServerLayer wmsl in wmslayers) {
				Layer l = new Layer(wmsl.Name, wmsl.Title){
					Description = wmsl.Abstract,
					MaxScale = 0,
					MinScale = 0,
					Visible = true
				};
				//l.AddExtents (this.ReadExtents(wmsl.SRIDBoundingBoxes));
				l.Childs.Add (this.ReadLayers(wmsl.ChildLayers));
				layers.Add (l);
			}
			return layers;
		}

		private ExtentList ReadExtents(IEnumerable<SpatialReferencedBoundingBox> boxes)
		{
			ExtentList list = new ExtentList ();
			foreach (SpatialReferencedBoundingBox b in boxes) {
				CoordinateSystems.ICoordinateSystem crs = CrsStore.Find ("epsg", b.SRID);
				if (crs != null) {
					list.Add (new Extent (b, crs));
				}
			}
			return list;
		}

		private List<CoordinateSystems.ICoordinateSystem> ReadCrs(string[] crslist)
		{
			List<CoordinateSystems.ICoordinateSystem> list = new List<CoordinateSystems.ICoordinateSystem> ();
			foreach (string crs in crslist) {
				string[] crsparts = crs.Split (':');
				CoordinateSystems.ICoordinateSystem icrs = CrsStore.Find("epsg", int.Parse(crsparts[1]));
				if (icrs != null) {
					list.Add(icrs);
				}
			}
			return list;
		}

		public override void Refresh ()
		{
			throw new NotImplementedException ();
		}

		public override Stream Render(Extent envelope, int width, int height)
		{
			//throw new NotImplementedException ();
			return null;
		}

		public override Stream Render(Extent envelope, int width, int height, params string[] layernames)
		{
			//throw new NotImplementedException ();
			return null;
		}
	}
}