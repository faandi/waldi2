using System;
using Xwt;
using Xwt.Drawing;
using System.Xml;
using Modules.GeoServices;

namespace Apps.WebGis
{
	public class MainWindow: Window
	{
		public MainWindow ()
		{
			//this.Icon = Image.FromResource("Apps.WebGis.Icons.waldi-bw-32.png");

			Menu menu = new Menu ();

			var file = new MenuItem ("_File");
			file.SubMenu = new Menu ();
			file.SubMenu.Items.Add (new MenuItem ("_Open"));
			file.SubMenu.Items.Add (new MenuItem ("_New"));
			MenuItem mi = new MenuItem ("_Close");
			mi.Clicked += delegate {
				Application.Exit();
			};
			file.SubMenu.Items.Add (mi);
			menu.Items.Add (file);

			var edit = new MenuItem ("_Edit");
			edit.SubMenu = new Menu ();
			edit.SubMenu.Items.Add (new MenuItem ("_Copy"));
			edit.SubMenu.Items.Add (new MenuItem ("Cu_t"));
			edit.SubMenu.Items.Add (new MenuItem ("_Paste"));
			menu.Items.Add (edit);

			MainMenu = menu;

			Notebook noteb = new Notebook ();
			noteb.Add (new GeoServiceWidget (), "Services");
			noteb.Add (new PlaceholderWidget ("Left: TOC\nRight: Services (drag'n'drop)"), "Toc");
			noteb.Add (new PlaceholderWidget ("Show Excel Export [*]\nShow Links, Editor, etc."), "Selectset");
			noteb.Add (new PlaceholderWidget ("???"), "Tools");

			Content = noteb;

			CloseRequested += HandleCloseRequested;
		}

		void HandleCloseRequested (object sender, CloseRequestedEventArgs args)
		{
			//args.Handled = !MessageDialog.Confirm ("Samples will be closed", Command.Ok);
			Application.Exit ();
		}

		protected override void Dispose (bool disposing)
		{
			base.Dispose (disposing);
		}
	}

	public class PlaceholderWidget : HBox
	{
		public PlaceholderWidget (string text)
		{
			var title = new Label (text);
			this.PackStart (title);
		}
	}
}