package flex.samples.crm.company
{
	import mx.collections.ArrayCollection;

	[Managed]
	[RemoteClass(alias="samples.crm.Company")]
	public class Company
	{
		public var companyId:int;

		public var name:String = "";

		public var address:String = "";

		public var city:String = "";

		public var state:String = "";

		public var zip:String = "";

        public var industry:String = "";
		
	}
}
