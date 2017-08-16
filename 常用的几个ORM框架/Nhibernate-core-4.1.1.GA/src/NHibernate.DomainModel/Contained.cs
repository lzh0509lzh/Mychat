using System;
using System.Collections.Generic;

namespace NHibernate.DomainModel
{
	/// <summary>
	/// Summary description for Contained.
	/// </summary>
	public class Contained
	{
		private Container _container;
		private long _id;
		private IList<Container> _bag = new List<Container>();
		private IList<Container> _lazyBag = new List<Container>();

		# region object overrides

		public override bool Equals(object obj)
		{
			if (obj == null) return false;

			return _id == ((Contained) obj).Id;
		}

		public override int GetHashCode()
		{
			return _id.GetHashCode();
		}

		#endregion

		public virtual Container Container
		{
			get { return _container; }
			set { _container = value; }
		}

		public virtual long Id
		{
			get { return _id; }
			set { _id = value; }
		}

		public virtual IList<Container> Bag
		{
			get { return _bag; }
			set { _bag = value; }
		}

		public virtual IList<Container> LazyBag
		{
			get { return _lazyBag; }
			set { _lazyBag = value; }
		}
	}
}