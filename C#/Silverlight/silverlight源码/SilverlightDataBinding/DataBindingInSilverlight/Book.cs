using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
//£µ£±£ás£ð£ø
namespace DataBindingInSilverlight
{
	public class Book : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public Book(string title, ObservableCollection<Author> authorList, double coverPrice, string isbn10, string publisher, int edition, int printing, string pubYear)
		{


			this.Title = title;

			this.Authors = new ObservableCollection<Author>();
			foreach (Author auth in authorList)
			{
				this.Authors.Add(auth);
			}

			this.CoverPrice = coverPrice;
			this.ISBN10 = isbn10;
			this.Publisher = publisher;
			this.Edition = edition;
			this.Printing = printing;
			this.PubYear = pubYear;

		}




		private string privateTitle;
		public string Title
		{
			get
			{
				return privateTitle;
			}
			set
			{
				privateTitle = value;
			}
		}
		public string NumAuthors
		{
			get
			{
				return privateAuthors.Count.ToString();
			}
		}
		private ObservableCollection<Author> privateAuthors;
		public ObservableCollection<Author> Authors
		{
			get
			{
				return privateAuthors;
			}
			internal set
			{
				privateAuthors = value;
			}
		}
		private double privateCoverPrice;
		public double CoverPrice
		{
			get
			{
				return privateCoverPrice;
			}
			set
			{
				privateCoverPrice = value;
			}
		}
		private string privateISBN10;
		public string ISBN10
		{
			get
			{
				return privateISBN10;
			}
			set
			{
				privateISBN10 = value;
			}
		}
		private string privatePublisher;
		public string Publisher
		{
			get
			{
				return privatePublisher;
			}
			set
			{
				privatePublisher = value;
			}
		}
		private int privateEdition;
		public int Edition
		{
			get
			{
				return privateEdition;
			}
			set
			{
				privateEdition = value;
			}
		}
		private int privatePrinting;
		public int Printing
		{
			get
			{
				return privatePrinting;
			}
			set
			{
				privatePrinting = value;
			}
		}
		private string privatePubYear;
		public string PubYear
		{
			get
			{
				return privatePubYear;
			}
			set
			{
				privatePubYear = value;
			}
		}
	}
} //end of root namespace