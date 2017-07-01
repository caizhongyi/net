using System;
using FluorineFx.AMF3;

namespace ServiceLibrary
{
	/// <summary>
	/// Summary description for ContactVO.
	/// </summary>
    [FluorineFx.TransferObject]
    public class ContactVO : IExternalizable
	{
        private int _id;
        private string _name;

        public ContactVO()
        {
        }

        public ContactVO(int id, string name)
        {
            this._id = id;
            this._name = name;
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
