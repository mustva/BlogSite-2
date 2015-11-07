using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;

namespace BlogSite.Models
{
    public class Post
    {
        public ObjectId Id { get;  set; }  // added for Mongo

        public string Title { get; set; }

        public string Body { get; set; }

        public int CharCount { get; set; }

        public IList<Comment> Comments { get; set; }
    }
}