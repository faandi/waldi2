using System;
using System.Collections.Generic;
using Geometries = GeoAPI.Geometries;
using CoordinateSystems = GeoAPI.CoordinateSystems;

namespace Modules.GeoServices
{
	public interface ILayer
	{
		string Name { get; }
		string Title { get; }
		string Description { get; }

		CrsList SupportedCrs { get; }
		ExtentList DataExtents { get; }

		int MinScale { get; }
		int MaxScale { get; }
		bool Visible { get; set; }

		ILayer Parent { get; set; }
		LayerList Childs { get; }

		//string[] ChildNames { get; }
		//string ParentName { get; }

		// Preview (Render)
		// Layers
		// User
		// Password
		// ImageFormats
	}
}