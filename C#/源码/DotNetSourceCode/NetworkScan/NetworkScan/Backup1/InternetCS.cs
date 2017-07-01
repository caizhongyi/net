using System;
using System.Runtime ;
using System.Runtime.InteropServices ; 


namespace NetworkScan
{
	/// <summary>
	/// 
	/// </summary>
	public class InternetCS
	{
		//Creating the extern function 
		[DllImport("wininet.dll")] 
		private extern static bool InternetGetConnectedState(int Description,int ReservedValue); 
 
		//Creating a function that uses the API function 
		public static bool IsConnectedToInternet() 
		{ 
			int Desc=0; 
			return InternetGetConnectedState(Desc,0); 
		}
	}
}
