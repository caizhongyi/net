using System;
using FluorineFx;

namespace ServiceLibrary
{
	/// <summary>
	/// FluorineFx sample service.
	/// </summary>
    [RemotingService("FluorineFx BookService")]
	public class BookService
	{
        public BookService()
		{
		}

        public object GetBooks()
        {
            BookVO[] result = new BookVO[3];
		    BookVO book = new BookVO();
		    book.name = "Adobe Flex 2: Training from the Source";
		    book.bookid = "032142316X";
		    book.publishdate = new DateTime(2006, 8, 31);
		    result[0] = book;
		    book = new BookVO();
		    book.name = "ActionScript 3.0 Cookbook : Solutions and Examples for Flash Developers";
		    book.bookid = "0596526954";
		    book.publishdate = new DateTime(2006, 9, 1);
		    result[1] = book;
		    book = new BookVO();
		    book.name = "Programming Flex 2 : The Comprehensive Guide to Creating Rich Media Applications with Adobe Flex";
		    book.bookid = "059652689X";
		    book.publishdate = new DateTime(2006, 11, 1);
		    result[2] = book;						
		    return result;
        }
	}
}
