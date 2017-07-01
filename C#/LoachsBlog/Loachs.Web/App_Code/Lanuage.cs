using System;
using System.Collections.Generic;
using System.Web;

namespace Lanuage
{
    /// <summary>
    ///Lanuage 的摘要说明
    /// </summary>
    public class Page
    {
        public static string Home = "Home";
    }

    public class Comments
    {
        public static string VerificationError = "Verification code input error!";
        public static string EmailError = "E-mail format error!";
        public static string EmptyError(int count)
        {
            return "Comments can not be empty, and limit the " + count + " words!";
        }
    }

    public class WebSite
    {
        public static string Close = "Website is down!";
        public static string Contact = "Website is down, please contact the administrator!";
    }

    public class Article
    {
        public static string ArticleNotFound = "Article not found!";
        public static string ThisArticleIsNotFound = "This article is not found!";
        public static string ArticlesUnpublished = "Articles unpublished!";
        public static string ThisArticleIsNotPublished = "This article is not published!";
    }
}