package flex.samples.crm.employee
{
	import flex.samples.crm.company.Company;

	[Managed]
	[RemoteClass(alias="samples.crm.Employee")]
	public class Employee
	{
		public function Employee() {}
		
		public var employeeId:int;

		public var firstName:String = "";

		public var lastName:String = "";

		public var title:String = "";

		public var phone:String = "";

		public var email:String = "";

		public var company:Company;
	}
}
