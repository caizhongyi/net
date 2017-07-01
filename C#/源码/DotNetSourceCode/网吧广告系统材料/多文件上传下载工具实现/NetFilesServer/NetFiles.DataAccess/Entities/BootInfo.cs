using System;
using HFSoft.Data;
using HFSoft.Data.Mapping;
namespace NetFiles.DataAccess.Entities
{
	/// <summary>
	/// BootInfo
	/// </summary>
	[Serializable]
	[HFSoft.Data.Mapping.TableMap("BootInfo")]
	public partial class BootInfo
	{
		public BootInfo()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		private string mBootDirectory;
		/// <summary>
		/// [varchar]
		/// </summary>
		[HFSoft.Data.Mapping.Primary("BootDirectory")]
		public string BootDirectory
		{
			get
			{
				return mBootDirectory;
			}
			set
			{
				mBootDirectory = value;
			}
		}
		private string mRemark;
		/// <summary>
		/// [varchar]
		/// </summary>
		[HFSoft.Data.Mapping.Column("Remark")]
		public string Remark
		{
			get
			{
				return mRemark;
			}
			set
			{
				mRemark = value;
			}
		}
		private int mState;
		/// <summary>
		/// [int]
		/// </summary>
		[HFSoft.Data.Mapping.Column("State")]
		public int State
		{
			get
			{
				return mState;
			}
			set
			{
				mState = value;
			}
		}
		
		
		
	}
}
