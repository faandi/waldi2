using System;
using Xwt;
using Xwt.Drawing;
using System.Collections.Generic;

namespace Modules.GeoServices
{
	public class GeoServiceListView : VBox
	{
		ListStore store;
		ListView list;
		HPaned box;

		DataField<string> nameCol = new DataField<string> ();
		DataField<Image> iconCol = new DataField<Image> ();
		DataField<IGeoService> serviceCol = new DataField<IGeoService> ();
		Image mapServiceIcon;
		Menu menu;

		private GeoServiceList services;
		public GeoServiceList Services {
			get {
				return this.services;
			}
			set {
				if (value == null) {
					throw new ArgumentNullException ();
				}
				this.services = value;
			}
		}

		public bool ReadOnly { get; set;}

		// events
		public event EventHandler<GeoServiceServiceEventArgs> ServiceSelected;

		public GeoServiceListView ()
		{
		}
		protected override void OnReallocate ()
		{
			this.ShowList ();
			base.OnReallocate ();
		}
		public GeoServiceListView (GeoServiceList services) 
		{
			this.Services = services;
		}

		void ShowList() 
		{
			mapServiceIcon = Image.FromResource("Modules.GeoServices.Icons.mapservice.png").WithSize(16);

			store = new ListStore (iconCol, nameCol, serviceCol);

			list = new ListView ();
			TextCellView nameTextCell = new TextCellView { Editable = false, TextField = nameCol };
			//nameTextCell.TextChanged += HandleTextChanged;
			ImageCellView iconCell = new ImageCellView (iconCol);
			list.Columns.Add ("Name", iconCell, nameTextCell);
			list.SelectionChanged += HandleSelectionChanged;
			list.DataSource = store;

			foreach (IGeoService gs in this.services) {
				this.AddServiceToList (gs);
			}

			box = new HPaned ();
			box.Panel1.Content = list;
			PackStart (box, true);

			if (!this.ReadOnly) {
				// contextmenu
				list.ButtonPressed += HandleButtonPressed;
				menu = new Menu ();
				MenuItem itemDelete = new MenuItem ("Delete");
				itemDelete.Clicked += HandleItemDeleteClicked;
				menu.Items.Add (itemDelete);
				// add button
				Button addButton = new Button ("Add Service");
				addButton.Clicked += HandleAddService;
				PackStart (addButton);
			}
		}

		void AddServiceToList (IGeoService gs)
		{
			int rid = store.AddRow ();
			store.SetValue(rid, iconCol, mapServiceIcon);
			store.SetValue (rid, nameCol, gs.Title);
			store.SetValue (rid, serviceCol, gs);
		}

		void HandleItemDeleteClicked (object sender, EventArgs e)
		{
			if (list.SelectedRow < 0) {
				return;
			}
			store.RemoveRow (list.SelectedRow);
		}

		void HandleButtonPressed (object sender, ButtonEventArgs e)
		{
			if (e.Button == PointerButton.Right) {
				menu.Popup ();
			}
		}

		void HandleSelectionChanged (object sender, EventArgs e)
		{
			if (list.SelectedRow < 0) {
				return;
			}
			IGeoService gs = store.GetValue (list.SelectedRow, serviceCol);
			if (ServiceSelected != null) {
				ServiceSelected (this, new GeoServiceServiceEventArgs (gs) { });
			}			
		}

		void HandleAddService(object sender, EventArgs e)
		{
			Dialog d = new Dialog ();
			GeoServiceAdd add = new GeoServiceAdd ();
			add.ServiceCreated += (object sendercreated, GeoServiceServiceEventArgs ecreated) => {
				this.services.Add(ecreated.Service);
				this.AddServiceToList(ecreated.Service);
				d.Hide();
				d.Dispose();
			};
			add.FinishRequested += (object senderfinish, EventArgs efinish) => {
				d.Hide();
				d.Dispose();
			};
			d.Content = add;
			d.CloseRequested += (object senderclose, CloseRequestedEventArgs args) => {	
				d.Hide();
				d.Dispose();
			};
			d.Run (this.ParentWindow);
		}
	}
}