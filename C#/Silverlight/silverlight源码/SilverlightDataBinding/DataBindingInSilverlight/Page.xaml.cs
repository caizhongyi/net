using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace DataBindingInSilverlight
{
	public partial class Page : UserControl
	{
		private Library myLibrary;

		public Page()
		{
			// Required to initialize variables
			InitializeComponent();
			GetDataFromWebService();
		}

		public void GetDataFromWebService()
		{
			myLibrary = Resources["LibraryDS"] as Library;
			myLibrary.Name = "Liberty Books";

			ObservableCollection<Author> Authors = new ObservableCollection<Author>();
			Author Author1 = new Author();
			Author Author2 = new Author();
			Author Author3 = new Author();

			Author1.Name = "Jesse Liberty";
			Author2.Name = "Tim Heurer";
			Authors.Add(Author1);
			Authors.Add(Author2);

			Book newBook = new Book("Programming Silverlight", Authors, 49.99, "TBD", "O'Reilly Media", 1, 1, "2009");
			myLibrary.Books.Add(newBook);

			Authors.Remove(Author2);
			Author2 = new Author();
			Author2.Name = "Alex Horovitz";
			Authors.Add(Author2);
			newBook = new Book("51aspx", Authors, 49.99, "0-596-51039-X", "O'Reilly Media", 1, 2, "2008");
			myLibrary.Books.Add(newBook);

			Authors.Remove(Author2);
			Author2 = new Author();
			Author2.Name = "Dan Hurwitz";
			Author3.Name = "Brian Macdonald";
			Authors.Add(Author2);
			Authors.Add(Author3);

			newBook = new Book("Learning ASP.NET 3.5", Authors, 44.99, "0-596-51845-5", "O'Reilly Media", 2, 1, "2008");
			myLibrary.Books.Add(newBook);


			Authors.Remove(Author2);
			Authors.Remove(Author3);
			Author2 = new Author();
			Author2.Name = "Donald Xie";
			Authors.Add(Author2);
			myLibrary.Books.Add(new Book("Programming C# 3.0", Authors, 44.99, "0-596-51845-5", "O'Reilly Media", 5, 2, "2008"));


			Authors.Remove(Author2);
			myLibrary.Books.Add(new Book("Programming VB.NET", Authors, 39.95, "0596004389 ", "O'Reilly Media", 2, 1, "2003"));
			myLibrary.Books.Add(new Book("Visual C# 2005 Dev Notebook", Authors, 29.95, "059600799X", "O'Reilly Media", 1, 1, "2005"));
			myLibrary.Books.Add(new Book("Clouds To Code", Authors, 1.95, "1861000952", "Wrox", 1, 1, "1997"));




		}

	}

} //end of root namespace