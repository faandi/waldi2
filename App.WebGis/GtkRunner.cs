using System;
using System.Linq;
using Xwt;
using System.Collections.Generic;
using System.Threading;

namespace Apps.WebGis
{
	class GtkRunner
	{
		//[STAThread]
		public static void Main (string[] args)
		{
			Application.Initialize (ToolkitType.Gtk);
			MainWindow mainWindow = new MainWindow (){
				Title = "WebGis Creator/Designer/Generator/Architect/Author/Maker/Producer",
				Width = 700,
				Height = 500
			};
			mainWindow.Show ();
			Application.Run ();
			mainWindow.Dispose ();
		}
	}
}
