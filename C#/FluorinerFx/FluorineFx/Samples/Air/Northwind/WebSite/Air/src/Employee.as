package
{
	[Bindable]
	[RemoteClass(alias="ServiceLibrary.EmployeeVO")]
	public class Employee
	{
	
		public var id : int;
		/** The first name of the employee. **/
		public var firstName : String;
		/** The last name of the employee. **/
		public var lastName : String;
		/** The title of the employee. **/
		public var title : String;
		
		public var reportsTo : Number;
		
		public var phone : String;
		public var extension : String;
		public var address : String;
		public var city : String
		public var state : String;
		public var postalCode : String;
		public var country : String;
		public var photo : String;
		public var notes : String;
	}
}