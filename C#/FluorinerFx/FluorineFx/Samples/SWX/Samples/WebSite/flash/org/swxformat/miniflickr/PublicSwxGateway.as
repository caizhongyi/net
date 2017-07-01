import org.swxformat.SWX;

class org.swxformat.miniflickr.PublicSwxGateway
{
	static var inst:PublicSwxGateway;
	
	var _swx:SWX;
	
	private function PublicSwxGateway()
	{
		// This is a singleton; please use the getInstance() method.
		
		// Create a new SWX instance and set it up to use the 
		// public SWX gateway on swxformat.org.
		_swx = new SWX();
		_swx.gateway = "http://localhost:2896/SwxGateway.aspx";
		//_swx.gateway = "http://localhost:8888/php/swx.php";
		_swx.encoding = "GET";
		_swx.debug = true;
	}
	
	public function get swx():SWX
	{
		return _swx;
	}
	
	public static function getInstance()
	{
		if (!inst)
		{
			inst = new PublicSwxGateway();
		}
		return inst;
	}
}