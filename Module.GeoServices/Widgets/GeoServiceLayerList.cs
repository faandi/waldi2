using System;
using Xwt;
using Xwt.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace Modules.GeoServices
{
	public class GeoServiceLayerList : VBox
	{
		private static class WaldiIcons
		{
			public static Image FolderOpen = Image.FromResource("Modules.GeoServices.Icons.folder-open.png").WithSize(16);
			public static Image FolderClosed = Image.FromResource("Modules.GeoServices.Icons.folder-closed.png").WithSize(16);
			public static Image Layer = Image.FromResource("Modules.GeoServices.Icons.layer.png").WithSize(16);
		}

		TreeView tree;
		TreeStore store;

		DataField<string> nameCol = new DataField<string> ();
		DataField<Image> iconCol = new DataField<Image> ();
		DataField<ILayer> layerCol = new DataField<ILayer> ();

		LayerList layers;
		public LayerList Layers {
			get {
				return this.layers;
			}
			set {
				if (value == null) {
					throw new ArgumentNullException ();
				}
				this.layers = value;
				this.AddChilds ();
			}
		}

		// events
		public event EventHandler<LayerEventArgs> LayerSelected;


		public GeoServiceLayerList ()
		{

		}

		public GeoServiceLayerList (LayerList layers) 
		{
			this.Layers = layers;
		}

		void AddChilds()
		{
			store = new TreeStore (nameCol, iconCol, layerCol);
			tree = new TreeView ();
			tree.Columns.Add ("Name", iconCol, nameCol);
			tree.DataSource = store;
			tree.SelectionChanged += HandleTreeSelectionChanged;
			AddLayersToTree(layers);
			PackStart (tree, true);
		}

		void AddLayersToTree (LayerList layers, TreePosition pos = null)
		{
			if (layers == null) {
				return;
			}
			// serviceStore.AddNode ().SetValue (nameCol, layer.Title).SetValue (iconCol, icon).SetValue(layerCol, layer);
			foreach (ILayer l in layers) {

				bool isgroup = l.Childs.Count () > 0;

				TreeNavigator curnav;

				if (pos == null) {
					curnav = store.AddNode ();
				} else {
					curnav = store.AddNode (pos);
				}

				Image icon = isgroup ? WaldiIcons.FolderClosed : WaldiIcons.Layer;

				curnav.SetValue (nameCol, l.Title).SetValue (iconCol, icon).SetValue(layerCol, l);
				if (isgroup) {
					this.AddLayersToTree (l.Childs, curnav.CurrentPosition);
				}
			}
		}

		void HandleTreeSelectionChanged (object sender, EventArgs e)
		{
			if (this.LayerSelected != null && tree.SelectedRow != null) {
				ILayer l = store.GetNavigatorAt (tree.SelectedRow).GetValue (layerCol);
				this.LayerSelected (this, new LayerEventArgs (l));
			}
		}
	}
}