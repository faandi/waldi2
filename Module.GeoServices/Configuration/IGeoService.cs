using System;
using System.Collections.Generic;
using System.IO;

namespace Modules.GeoServices
{
	public interface IGeoService
	{
		string Name { get; }
		string Title { get; }
		string Description { get; }
		string Url { get; }
		LayerList RootLayers { get; }
		LayerList Layers { get; }
		ServiceType ServiceType { get; }

		CrsList SupportedCrs { get; }
		ExtentList DataExtents { get; }
		ExtentList InitializeExtents { get; }

		//void Refresh();
		// Refresh, Reload, ReloadCapabilities ??
		Stream Render(Extent envelope, int width, int height);
		Stream Render(Extent envelope, int width, int height, params string[] layernames);
		// Preview (Render)
		// Layers
		// User
		// Password
		// ImageFormats
	}
}