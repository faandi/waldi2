using System;

namespace Modules.GeoServices
{
	public class LayerEventArgs : EventArgs
	{
		//
		// Properties
		//
		public ILayer Layer
		{
			get;
			private set;
		}
		//
		// Constructors
		//
		public LayerEventArgs (ILayer layer)
		{
			this.Layer = layer;
		}
	}
}

