using System;
using System.Collections;

using FluorineFx.Messaging;

namespace ServiceLibrary
{
	/// <summary>
	/// Summary description for SpringFactory.
	/// </summary>
	public class SpringFactory : IFlexFactory
	{
		public SpringFactory()
		{
		}

		#region IFlexFactory Members

		public FactoryInstance CreateFactoryInstance(string id, Hashtable properties)
		{
			SpringFactoryInstance instance = new SpringFactoryInstance(this, id, properties);
			instance.Source = properties["source"] as string;
			return instance;
		}

		public object Lookup(FactoryInstance factoryInstance)
		{
			SpringFactoryInstance springFactoryInstance = factoryInstance as SpringFactoryInstance;
			return springFactoryInstance.Lookup();
		}

		#endregion
	}

	sealed class SpringFactoryInstance : FactoryInstance
	{
		public SpringFactoryInstance(SpringFactory factory, string id, Hashtable properties):base(factory, id, properties)
		{
		}


		public override object Lookup()
		{
			Spring.Context.IApplicationContext applicationContext = Spring.Context.Support.WebApplicationContext.GetRootContext();
			object instance = applicationContext.GetObject(this.Source);
			return instance;
		}
	}
}
