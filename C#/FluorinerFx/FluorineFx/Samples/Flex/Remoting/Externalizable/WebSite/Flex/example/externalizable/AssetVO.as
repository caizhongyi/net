package example.externalizable
{
	import flash.utils.IDataOutput;
	import flash.utils.IDataInput;
	import flash.utils.IExternalizable;
	
	[RemoteClass(alias="ServiceLibrary.AssetVO")]
	public class AssetVO implements IExternalizable
	{
    	private var _id:int;
    	private var _name:String;

    	public function AssetVO(id:int = 0, name:String = null)
    	{
    		_id = id;
    		_name = name;
    	}
    	
		public function readExternal(input:IDataInput):void
		{
			_id = input.readInt(); 
			_name = input.readObject() as String; 
			
		}
		
		public function writeExternal(output:IDataOutput):void
		{
			output.writeInt(_id);
			output.writeObject(_name);
		}		 	
	}
}