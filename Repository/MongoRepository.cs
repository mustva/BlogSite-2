using System;
using System.Collections.Generic;
using System.Linq;
using BlogSite.Models;
using BlogSite.viewModels;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace BlogSite.Repository
{
    

    public class MongoRepository : IRepository
    {
        private readonly MongoCollection<Post> _collection;

        public MongoRepository()
        {   // set up mongo repository
            const string connectionString = "add your mongodb key here";
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            var db = server.GetDatabase("bacs487test");
            _collection = db.GetCollection<Post>("Post"); // a collection is like a table
        }

        public List<Post> GetListOfAllPosts()
        {
            return _collection.FindAll().ToList();
        }

        public Post GetAPostWithId(string idValue)
        {
            var Oid = new ObjectId(idValue);
            var results = from p in _collection.AsQueryable()
                where p.Id == Oid
                select p;

            return results.FirstOrDefault();
        }

        public void Create(Post aPost)
        {
             aPost.Comments = new List<Comment>();
            _collection.Save(aPost);
        }

        public void Edit(Post aPost, string idValue)
        {
            var updatedPost = GetAPostWithId(idValue);
            updatedPost.Title = aPost.Title;
            updatedPost.Body = aPost.Body;
            updatedPost.CharCount = aPost.CharCount;
            _collection.Save(updatedPost);
        }

        public void Remove(string idValue)
        {
            var query = Query<Post>.EQ(e => e.Id, new ObjectId(idValue));
            _collection.Remove(query);
        }

        public void AddComment(CommentViewModel aCommentvm)
        {
            var thePost = GetAPostWithId(aCommentvm.ThePostId);
            var aNewComment = new Comment
            {
                Body = aCommentvm.Body,
                Email = aCommentvm.Email,
                TimePosted = DateTime.Now
            };
            thePost.Comments.Add(aNewComment);
            _collection.Save(thePost);
        }
    }
}