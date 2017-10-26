using System;

namespace Modules.GeoServices
{
	public class GeoServiceServiceEventArgs : EventArgs
	{
		//
		// Properties
		//
		public IGeoService Service
		{
			get;
			private set;
		}
		//
		// Constructors
		//
		public GeoServiceServiceEventArgs (IGeoService service)
		{
			this.Service = service;
		}
	}
}

