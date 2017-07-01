using System;
using FluorineFx.AMF3;

namespace ServiceLibrary
{
	/// <summary>
	/// Summary description for AssetVO.
	/// </summary>
    [FluorineFx.TransferObject]
    public class AssetVO : IExternalizable
	{
		private int _id;
        private string _name;

		public AssetVO()
		{
		}

        public AssetVO(int id, string name)
        {
            _id = id;
            _name = name;
        }

        #region IExternalizable Members

        public void WriteExternal(IDataOutput output)
        {
            output.WriteInt(_id);
            output.WriteObject(_name);
        }

        public void ReadExternal(IDataInput input)
        {
            _id = input.ReadInt();
            _name = input.ReadObject() as string;
        }

        #endregion
    }
}
