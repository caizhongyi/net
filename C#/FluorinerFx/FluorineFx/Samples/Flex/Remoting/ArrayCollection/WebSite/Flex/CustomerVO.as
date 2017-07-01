package
{
	import mx.collections.ArrayCollection;
	
	[RemoteClass(alias="ServiceLibrary.CustomerVO")]
	public class CustomerVO
	{
    	public var Firstname:String;
    	public var Lastname:String;
    	public var PhoneNumbers:ArrayCollection;
    	
	}
}