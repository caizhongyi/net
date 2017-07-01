using System;
using System.Collections;
using System.Reflection;

using FluorineFx.Messaging;
using Microsoft.Practices.EnterpriseLibrary.PolicyInjection;

namespace ServiceLibrary
{
	/// <summary>
	/// Summary description for SpringFactory.
	/// </summary>
	public class PIABFactory : IFlexFactory
	{
		public PIABFactory()
		{
		}

		#region IFlexFactory Members

		public FactoryInstance CreateFactoryInstance(string id, Hashtable properties)
		{
            PIABFactoryInstance instance = new PIABFactoryInstance(this, id, properties);
			instance.Source = properties["source"] as string;
			return instance;
		}

		public object Lookup(FactoryInstance factoryInstance)
		{
            PIABFactoryInstance piFactoryInstance = factoryInstance as PIABFactoryInstance;
            return piFactoryInstance.Lookup();
		}

		#endregion
	}

	sealed class PIABFactoryInstance : FactoryInstance
	{
        public PIABFactoryInstance(PIABFactory factory, string id, Hashtable properties)
            : base(factory, id, properties)
		{
		}


		public override object Lookup()
		{
            Type type = Type.GetType(this.Source);
            object instance = Activator.CreateInstance(type, BindingFlags.CreateInstance | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static, null, new object[] { }, null);
            MarshalByRefObject mbro = PolicyInjection.Wrap<MarshalByRefObject>(instance);
            return mbro;
		}
	}
}
