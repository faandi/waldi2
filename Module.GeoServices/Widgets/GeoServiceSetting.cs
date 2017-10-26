using System;
using Xwt;
using Xwt.Drawing;
using System.Collections.Generic;

namespace Modules.GeoServices
{
	public class GeoServiceSetting : VBox
	{
		IGeoService service;
		public IGeoService Service {
			get {
				return this.service;
			}
			set {
				if (value == null) {
					throw new ArgumentNullException ();
				}
				this.service = value;
				this.AddChilds ();
			}
		}

		public GeoServiceSetting () 
		{
		}

		public GeoServiceSetting (IGeoService service) 
		{
			this.Service = service;
		}

		public void AddChilds()
		{
			var title = new Label (this.service.Title);
			this.PackStart (title);
		}
	}
}