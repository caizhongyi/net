package example.externalizable
{
	import flash.utils.IDataOutput;
	import flash.utils.IDataInput;
	import flash.utils.IExternalizable;
	import mx.collections.ArrayCollection;

	[RemoteClass(alias="ServiceLibrary.AgreementVO")]
	public class AgreementVO implements IExternalizable
	{
		private var _id:int;
		private var _dateCreated:Date;
		private var _seller:ContactVO;
		private var _assets:ArrayCollection;
		
		public function AgreementVO()
		{
			_id = 0;
			_dateCreated = new Date();
			_assets = new ArrayCollection();
		}
		
		public function get id():int
		{
			return _id;
		}

		public function get dateCreated():Date
		{
			return _dateCreated;
		}

		public function get seller():ContactVO
		{
			return _seller;
		}
		
		public function get assets():ArrayCollection
		{
			return _assets;
		}
		

		public function set seller(contact:ContactVO):void
		{
			_seller = contact;
		}
		
		public function readExternal(input:IDataInput):void
		{
			_id = input.readInt(); 
			_dateCreated = input.readObject() as Date;
			_seller = input.readObject() as ContactVO;
			_assets = input.readObject() as ArrayCollection;
		}
		
		public function writeExternal(output:IDataOutput):void
		{
			output.writeInt(_id);
			output.writeObject(_dateCreated);
			output.writeObject(_seller);
			output.writeObject(_assets);
		}
	}
}