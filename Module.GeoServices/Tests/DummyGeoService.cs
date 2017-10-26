using System;
using System.Collections.Generic;
using Geometries = GeoAPI.Geometries;
using CoordinateSystems = GeoAPI.CoordinateSystems;
using ProjNet.CoordinateSystems;
using System.Reflection;
using System.IO;

namespace Modules.GeoServices
{
	public class DummyGeoService : GeoService
	{
		// "http://dummyservice.org"

		public DummyGeoService (string name) : base (name, ServiceType.Dummy)
		{
			// simulate latency
			//System.Threading.Thread.Sleep (TimeSpan.FromSeconds (5));
			//this.Name = "dummyname";

			this.Title = this.Name;
			this.SupportedCrs.Add (
				// WGS84
				crsfactory.CreateFromWkt ("GEOGCS[\"WGS 84\",DATUM[\"WGS_1984\",SPHEROID[\"WGS 84\",6378137,298.257223563,AUTHORITY[\"EPSG\",\"7030\"]],AUTHORITY[\"EPSG\",\"6326\"]],PRIMEM[\"Greenwich\",0,AUTHORITY[\"EPSG\",\"8901\"]],UNIT[\"degree\",0.01745329251994328,AUTHORITY[\"EPSG\",\"9122\"]],AUTHORITY[\"EPSG\",\"4326\"]]"),
				// Austria Lambert
				crsfactory.CreateFromWkt ("PROJCS[\"MGI / Austria Lambert\",GEOGCS[\"MGI\",DATUM[\"Militar_Geographische_Institute\",SPHEROID[\"Bessel 1841\",6377397.155,299.1528128,AUTHORITY[\"EPSG\",\"7004\"]],TOWGS84[577.326,90.129,463.919,5.137,1.474,5.297,2.4232],AUTHORITY[\"EPSG\",\"6312\"]],PRIMEM[\"Greenwich\",0,AUTHORITY[\"EPSG\",\"8901\"]],UNIT[\"degree\",0.0174532925199433,AUTHORITY[\"EPSG\",\"9108\"]],AUTHORITY[\"EPSG\",\"4312\"]],UNIT[\"metre\",1,AUTHORITY[\"EPSG\",\"9001\"]],PROJECTION[\"Lambert_Conformal_Conic_2SP\"],PARAMETER[\"standard_parallel_1\",49],PARAMETER[\"standard_parallel_2\",46],PARAMETER[\"latitude_of_origin\",47.5],PARAMETER[\"central_meridian\",13.33333333333333],PARAMETER[\"false_easting\",400000],PARAMETER[\"false_northing\",400000],AUTHORITY[\"EPSG\",\"31287\"],AXIS[\"Y\",EAST],AXIS[\"X\",NORTH]]")
			);
			this.DataExtents.Add(
				new Extent(-180,180,-90,90, this.SupportedCrs.Get("epsg", 4326)),
			    new Extent(107778.5323, 694883.9348, 286080.6331, 575953.6150, this.SupportedCrs.Get("epsg", 31287))
			);
			this.InitializeExtents.Add(
				new Extent(9.5300, 17.1700, 46.4100, 49.0200 , this.SupportedCrs.Get("epsg", 4326)),
				new Extent(107778.5323, 694883.9348, 286080.6331, 575953.6150, this.SupportedCrs.Get("epsg", 31287))
			);

			Layer layer1 = new Layer ("layer1", "Layer 1") {
				Visible = true,
				Description = "This Layer is dummy layer 1.\nDummy data is benign information that does not contain any useful data, but serves to reserve space where real data is nominally present. Dummy data can be used as a placeholder for both testing and operational purposes. For testing, dummy data can also be used as stubs or pad to avoid software testing issues by ensuring that all variables and data fields are occupied. In operational use, dummy data may be transmitted for OPSEC purposes. Dummy data must be rigorously evaluated and documented to ensure that it does not cause unintended effects."
			};
			layer1.AddExtents (this.DataExtents);
			Layer layer2 = new Layer ("layer2", "Layer 2") {
				Visible = false,
				Description = "This Layer is dummy layer 2.\nDummy data is benign information that does not contain any useful data, but serves to reserve space where real data is nominally present. Dummy data can be used as a placeholder for both testing and operational purposes. For testing, dummy data can also be used as stubs or pad to avoid software testing issues by ensuring that all variables and data fields are occupied. In operational use, dummy data may be transmitted for OPSEC purposes. Dummy data must be rigorously evaluated and documented to ensure that it does not cause unintended effects."
			};
			layer2.AddExtents (this.DataExtents);
			Layer layer3 = new Layer ("layer3", "Layer 3") {
				Visible = true,
				Description = "This Layer is dummy layer 3.\nDummy data is benign information that does not contain any useful data, but serves to reserve space where real data is nominally present. Dummy data can be used as a placeholder for both testing and operational purposes. For testing, dummy data can also be used as stubs or pad to avoid software testing issues by ensuring that all variables and data fields are occupied. In operational use, dummy data may be transmitted for OPSEC purposes. Dummy data must be rigorously evaluated and documented to ensure that it does not cause unintended effects."
			};
			layer3.AddExtents (this.DataExtents);
			Layer layer31 = new Layer ("layer3.1", "Layer 3.1") {
				Visible = true,
				Description = "This Layer is dummy layer 3.1.\nDummy data is benign information that does not contain any useful data, but serves to reserve space where real data is nominally present. Dummy data can be used as a placeholder for both testing and operational purposes. For testing, dummy data can also be used as stubs or pad to avoid software testing issues by ensuring that all variables and data fields are occupied. In operational use, dummy data may be transmitted for OPSEC purposes. Dummy data must be rigorously evaluated and documented to ensure that it does not cause unintended effects."
			};
			layer31.AddExtents (this.DataExtents);
			Layer layer32 = new Layer ("layer3.2", "Layer 3.2") {
				Visible = true,
				Description = "This Layer is dummy layer 3.2.\nDummy data is benign information that does not contain any useful data, but serves to reserve space where real data is nominally present. Dummy data can be used as a placeholder for both testing and operational purposes. For testing, dummy data can also be used as stubs or pad to avoid software testing issues by ensuring that all variables and data fields are occupied. In operational use, dummy data may be transmitted for OPSEC purposes. Dummy data must be rigorously evaluated and documented to ensure that it does not cause unintended effects."
			};
			layer32.AddExtents (this.DataExtents);
			Layer layer4 = new Layer ("layer4", "Layer 4") {
				Visible = false,
				Description = "This Layer is dummy layer 4.\nDummy data is benign information that does not contain any useful data, but serves to reserve space where real data is nominally present. Dummy data can be used as a placeholder for both testing and operational purposes. For testing, dummy data can also be used as stubs or pad to avoid software testing issues by ensuring that all variables and data fields are occupied. In operational use, dummy data may be transmitted for OPSEC purposes. Dummy data must be rigorously evaluated and documented to ensure that it does not cause unintended effects."
			};
			layer4.AddExtents (this.DataExtents);
			Layer layer41 = new Layer ("layer4.1", "Layer 4.1") {
				Visible = false,
				Description = "This Layer is dummy layer 4.1.\nDummy data is benign information that does not contain any useful data, but serves to reserve space where real data is nominally present. Dummy data can be used as a placeholder for both testing and operational purposes. For testing, dummy data can also be used as stubs or pad to avoid software testing issues by ensuring that all variables and data fields are occupied. In operational use, dummy data may be transmitted for OPSEC purposes. Dummy data must be rigorously evaluated and documented to ensure that it does not cause unintended effects."
			};
			layer41.AddExtents (this.DataExtents);
			Layer layer411 = new Layer ("layer4.1.1", "Layer 4.1.1") {
				Visible = false,
				Description = "This Layer is dummy layer 4.1.1.\nDummy data is benign information that does not contain any useful data, but serves to reserve space where real data is nominally present. Dummy data can be used as a placeholder for both testing and operational purposes. For testing, dummy data can also be used as stubs or pad to avoid software testing issues by ensuring that all variables and data fields are occupied. In operational use, dummy data may be transmitted for OPSEC purposes. Dummy data must be rigorously evaluated and documented to ensure that it does not cause unintended effects."
			};
			layer411.AddExtents (this.DataExtents);
			Layer layer4111 = new Layer ("layer4.1.1.1", "Layer 4.1.1.1") {
				Visible = true,
				Description = "This Layer is dummy layer 4.1.1.1.\nDummy data is benign information that does not contain any useful data, but serves to reserve space where real data is nominally present. Dummy data can be used as a placeholder for both testing and operational purposes. For testing, dummy data can also be used as stubs or pad to avoid software testing issues by ensuring that all variables and data fields are occupied. In operational use, dummy data may be transmitted for OPSEC purposes. Dummy data must be rigorously evaluated and documented to ensure that it does not cause unintended effects."
			};
			layer4111.AddExtents (this.DataExtents);

			layer3.Childs.Add (layer31);
			layer3.Childs.Add (layer32);
			layer4.Childs.Add (layer41);
			layer41.Childs.Add (layer411);
			layer411.Childs.Add (layer4111);

			this.RootLayers.Add (layer1, layer2, layer3);
		}

		public override void Refresh ()
		{
		}

		public override Stream Render(Extent envelope, int width, int height)
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			Stream input = assembly.GetManifestResourceStream ("Modules.GeoServices.Tests.osm-snapshot.png");
			return input;
		}

		public override Stream Render(Extent envelope, int width, int height, params string[] layernames)
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			Stream input = assembly.GetManifestResourceStream ("Modules.GeoServices.Tests.osm-snapshot.png");
			return input;
		}
	}
}