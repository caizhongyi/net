package
{
	import mx.messaging.Consumer;
	
    [Managed]
	public class AlertItem
	{
		public var symbol:String;
		public var trigger:String;
		public var consumer:Consumer;
		public var alertTime:Date;
	}
	
}
	