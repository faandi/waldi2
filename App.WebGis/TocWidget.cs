using System;
using Xwt;

namespace Apps.WebGis
{
	public class TocWidget : HBox
	{
		public TocWidget ()
		{
			var title = new Label ("Toc Widget");
			this.PackStart (title);
		}
	}
}