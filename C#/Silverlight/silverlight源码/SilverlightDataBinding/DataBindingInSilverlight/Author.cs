namespace DataBindingInSilverlight
{
	public class Author
	{
		private string privateName;
		public string Name
		{
			get
			{
				return privateName;
			}
			set
			{
				privateName = value;
			}
		}
	}

} //end of root namespace