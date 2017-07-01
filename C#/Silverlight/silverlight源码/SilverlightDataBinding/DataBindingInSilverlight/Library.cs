using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Browser;

namespace DataBindingInSilverlight
{
	public class Library : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public Library()
		{
			PrivateName = string.Empty;
			PrivateQuery = string.Empty;
			PrivateBooks = new ObservableCollection<Book>();
		}


		private string PrivateQuery;
		public string Query
		{
			get
			{
				return PrivateQuery;
			}
			set
			{
				PrivateQuery = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Query"));
			}
		}


		private string PrivateName;
		public string Name
		{
			get
			{
				return PrivateName;
			}
			set
			{
				PrivateName = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Name"));
			}
		}

		private ObservableCollection<Book> PrivateBooks;
		public ObservableCollection<Book> Books
		{
			get
			{
				return PrivateBooks;
			}
			set
			{
				PrivateBooks = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Books"));
			}
		}
	}

} //end of root namespace