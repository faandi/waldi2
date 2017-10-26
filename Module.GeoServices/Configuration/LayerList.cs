using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace Modules.GeoServices
{
	public class LayerList : IEnumerable, IEnumerable<ILayer>
	{
		Dictionary<string,ILayer> layers = new Dictionary<string,ILayer>();

		public event EventHandler<LayerEventArgs> LayerAdded;

		public LayerList ()
		{
		}

		public void Add(params ILayer[] layers)
		{
			this.Add (layers as IEnumerable<ILayer>);
		}

		public void Add(IEnumerable<ILayer> layers)
		{
			foreach (ILayer l in layers) {
				if (this.layers.ContainsKey (l.Name)) {
					throw new InvalidOperationException ("A layer with the same name already in list.");
				}
				this.layers.Add (l.Name, l);
				if (this.LayerAdded != null) {
					this.LayerAdded (this, new LayerEventArgs (l));
				}
			}
		}

		public ILayer this[string name]
		{
			get {
				return this.layers [name];
			}
		}

		public IEnumerator GetEnumerator ()
		{
			return this.layers.Values.GetEnumerator ();
		}

		IEnumerator<ILayer> IEnumerable<ILayer>.GetEnumerator ()
		{
			return this.layers.Values.GetEnumerator ();
		}
	}
}