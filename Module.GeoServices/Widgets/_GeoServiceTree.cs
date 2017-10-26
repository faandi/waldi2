using System;
using Xwt;
using Xwt.Drawing;
using System.Collections.Generic;

namespace Modules.GeoServices
{
	public class GeoServiceTree : VBox
	{
		private TreeView serviceTree;
		TreeStore serviceStore;
		HPaned box;

		DataField<string> nameCol = new DataField<string> ();
		DataField<Image> iconCol = new DataField<Image> ();
		DataField<ServiceInfo> layerCol = new DataField<ServiceInfo> ();
		Image iconOpen;
		Image iconClosed;

		// events
		public event EventHandler<GeoServiceServiceEventArgs> LayerSelected;
		public event EventHandler AddNewService;

		public GeoServiceTree () 
		{
			iconOpen = Image.FromResource("Modules.GeoServices.Icons.folder-open.png").WithSize(16);
			iconClosed = Image.FromResource("Modules.GeoServices.Icons.folder-closed.png").WithSize(16);
			//icon = icon.Scale (0.3);
			//icon = Xwt.StockIcons.Error;

			serviceStore = new TreeStore (nameCol, iconCol, layerCol);

			serviceTree = new TreeView ();
			serviceTree.Columns.Add ("Name", iconCol, nameCol);

			serviceTree.DataSource = serviceStore;

			box = new HPaned ();
			box.Panel1.Content = serviceTree;

			PackStart (box, true);

			ServiceInfo[] layers = new ServiceInfo[] {
				new ServiceInfo{ Title = "layer1", Name = "0",
					Layers =  new ServiceInfo[]{
						new ServiceInfo{ Title = "layer1.1", Name = "3"},
						new ServiceInfo{ Title = "layer1.2", Name = "4"}
					}
				},
				new ServiceInfo{ Title = "layer2", Name = "1",
					Layers =  new ServiceInfo[]{
						new ServiceInfo{ Title = "layer1.1", Name = "3"},
						new ServiceInfo{ Title = "layer1.2", Name = "4"}
					}
				},
				new ServiceInfo{ Title = "layer3", Name = "2",
					Layers =  new ServiceInfo[]{
						new ServiceInfo{ Title = "layer1.1", Name = "3"},
						new ServiceInfo{ Title = "layer1.2", Name = "4"}
					}
				}
			};

			AddLayers(layers);

			serviceTree.SelectionChanged += HandleServiceTreeSelectionChanged;
			serviceTree.RowExpanded += HandleServiceTreeRowExpanded;
			serviceTree.RowActivated += HandleRowActivated;
			serviceTree.KeyPressed += HandleKeyPressed;

			Button addGroupButton = new Button ("Add Group");
			addGroupButton.Clicked += HandleAddNewGroup;
			PackStart (addGroupButton);
			Button addLayerButton = new Button ("Add Layer");
			addLayerButton.Clicked += HandleAddNewLayer;
			PackStart (addLayerButton);
			Button removeButton = new Button ("Remove Selection");
			removeButton.Clicked += delegate(object sender, EventArgs e) {
				foreach (TreePosition row in serviceTree.SelectedRows) {
					serviceStore.GetNavigatorAt (row).Remove ();
				}
			};
			PackStart (removeButton);
		}

		void HandleKeyPressed (object sender, KeyEventArgs e)
		{
			if (e.Key == Key.F2) {
				OpenRenameDialog (serviceTree.SelectedRow);
				e.Handled = true;
			}
		}

		void HandleRowActivated (object sender, TreeViewRowEventArgs e)
		{
			OpenRenameDialog (e.Position);
		}

		void OpenRenameDialog(TreePosition pos)
		{
			Dialog d = new Dialog ();
			d.Title = "Rename Item";
			Table t = new Table ();
			t.Add (new Label ("Name"), 0, 0);
			string txt = serviceStore.GetNavigatorAt (pos).GetValue (nameCol);
			TextEntry text = new TextEntry ();
			text.Text = txt;
			text.KeyPressed += (object sender, KeyEventArgs e) => {
				if (e.Key == Key.Return) {
					RenameItem(pos, ((TextEntry)sender).Text);
					d.Dispose();
				}
			};
			t.Add (text, 1, 0);
			d.Content = t;
			d.Buttons.Add (new DialogButton (Command.Cancel));
			d.Buttons.Add (new DialogButton (Command.Ok));
			d.Run (this.ParentWindow);
			if (!string.IsNullOrEmpty (text.Text)) {
				RenameItem (pos, text.Text);
			}
			d.Dispose ();
		}

		void RenameItem(TreePosition pos, string name)
		{
			TreeNavigator nav = serviceStore.GetNavigatorAt (pos);
			ServiceInfo li = nav.GetValue (layerCol);
			nav.SetValue (nameCol, name);
			li.Title = name;
		}

		private void AddLayers (IEnumerable<ServiceInfo> layers, TreeNavigator nav = null)
		{
			if (layers == null) {
				return;
			}
			// serviceStore.AddNode ().SetValue (nameCol, layer.Title).SetValue (iconCol, icon).SetValue(layerCol, layer);
			foreach (ServiceInfo l in layers)
			{
				TreeNavigator curnav = nav;
				TreeNavigator subnav;
				if (curnav == null) {
					curnav = serviceStore.AddNode ();
				} else {
					curnav = nav.AddChild ();
				}
				subnav = curnav.SetValue (nameCol, l.Title).SetValue (iconCol, iconClosed).SetValue(layerCol, l);
				this.AddLayers (l.Layers, subnav);
			}
		}

		private void HandleAddNewLayer(object sender, EventArgs e)
		{
			if (AddNewService != null) {
				AddNewService (this, new EventArgs () { });
			}
		}

		private void HandleAddNewGroup(object sender, EventArgs e)
		{
			TreeNavigator nav;
			if (serviceTree.SelectedRow == null) {
				nav = serviceStore.AddNode ();
			} else {
				nav = serviceStore.GetNavigatorAt(serviceTree.SelectedRow).AddChild();
			}
			ServiceInfo l = new ServiceInfo(){
				Name = Guid.NewGuid().ToString(),
				Title =  "New group"
			};
			nav.SetValue (nameCol, "New group").SetValue (iconCol, iconClosed).SetValue(layerCol, l);
			nav.MoveToParent ();
			serviceTree.ExpandRow (nav.CurrentPosition, false);
		}

		private void HandleLayerItemClicked(object sender, EventArgs e)
		{
			MessageDialog.ShowMessage ("S");
		}

		private void HandleServiceTreeRowExpanded (object sender, TreeViewRowEventArgs e)
		{
			serviceStore.GetNavigatorAt (e.Position).SetValue (iconCol, iconOpen);
		}

		private void HandleServiceTreeSelectionChanged (object sender, EventArgs e)
		{
			if (serviceTree.SelectedRow != null) {
				ServiceInfo l = serviceStore.GetNavigatorAt (serviceTree.SelectedRow).GetValue (layerCol);
				if (LayerSelected != null) {
					LayerSelected (this, new GeoServiceServiceEventArgs (l) { });
					//MessageDialog.ShowMessage ("Selected Layer:\n" + l.Title);
				}
			}
		}
	}
}