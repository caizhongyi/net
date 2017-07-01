using System;
using HFSoft.Data;
using HFSoft.Data.Mapping;
namespace NetFiles.DataAccess.Entities
{
	public partial class DBMapping
	{
		private DBMapping()
		{
			
		}
		
		private static Mappers.BootInfoMapper mBootInfo  =new Mappers.BootInfoMapper();
		public static Mappers.BootInfoMapper BootInfo 
		{
			get
			{
				return mBootInfo;
			}
		}
	
	}
	
	#region ===================BootInfo Mapper==============================
		namespace Mappers
		{
		public partial class BootInfoMapper:HFSoft.Data.Mapping.Table
		{
			public BootInfoMapper():base("BootInfo")
			{
				mALL = new HFSoft.Data.Mapping.ObjectField("*",this);
				mBootDirectory = new HFSoft.Data.Mapping.StringField("BootDirectory",this);
				mRemark = new HFSoft.Data.Mapping.StringField("Remark",this);
				mState = new HFSoft.Data.Mapping.NumberField("State",this);
			}
			private  HFSoft.Data.Mapping.StringField mBootDirectory ;
			public HFSoft.Data.Mapping.StringField BootDirectory
			{
				get
				{
					return mBootDirectory;
				}
			}
			private  HFSoft.Data.Mapping.StringField mRemark ;
			public HFSoft.Data.Mapping.StringField Remark
			{
				get
				{
					return mRemark;
				}
			}
			private  HFSoft.Data.Mapping.NumberField mState ;
			public HFSoft.Data.Mapping.NumberField State
			{
				get
				{
					return mState;
				}
			}
			private HFSoft.Data.Mapping.ObjectField mALL;
			public HFSoft.Data.Mapping.ObjectField ALL
			{
				get
				{
					return mALL;
				}
			}
			
		}
		}
		#endregion
		
	
}
