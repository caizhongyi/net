using System;
using System.Collections;
using System.Collections.Generic;

using FluorineFx.AMF3;

namespace ServiceLibrary
{
	/// <summary>
	/// Summary description for AgreementVO.
	/// </summary>
    [FluorineFx.TransferObject]
	public class AgreementVO : IExternalizable
	{
		private int _id;
		private DateTime _dateCreated;
		private ContactVO _seller;
		private IList<AssetVO> _assets;

        public AgreementVO()
        {
            _id = 0;
            _dateCreated = DateTime.MinValue;
            _assets = new List<AssetVO>();
        }

		public AgreementVO(int id, DateTime dateCreated)
		{
            _id = id;
            _dateCreated = dateCreated;
			_assets = new List<AssetVO>();
		}

		public ContactVO Contact
		{
			get{ return _seller; }
			set{ _seller = value; }
		}

        public IList<AssetVO> Assets
		{
			get{ return _assets; }
			set{ _assets = value; }
		}

		#region IExternalizable Members

		public void WriteExternal(IDataOutput output)
		{
			output.WriteInt(_id);
			output.WriteObject(_dateCreated);
			output.WriteObject(_seller);
            output.WriteObject(_assets);
		}

		public void ReadExternal(IDataInput input)
		{
			_id = input.ReadInt(); 
			_dateCreated = (DateTime)input.ReadObject();
			_seller = new ContactVO();
			_seller = input.ReadObject() as ContactVO;
            IList assets = input.ReadObject() as IList;
            //The IList returned by the ReadObject is an ArrayCollection in this sample
            foreach (AssetVO asset in assets)
                _assets.Add(asset);
        }

		#endregion
	}
}
