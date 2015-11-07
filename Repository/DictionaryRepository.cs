using System;
using System.Collections.Generic;
using System.Linq;
using BlogSite.Models;
using BlogSite.viewModels;
using MongoDB.Bson;

namespace BlogSite.Repository
{
    public class DictionaryRepository : IRepository
    {
        private static readonly Dictionary<ObjectId, Post> DataDictionary = new Dictionary<ObjectId, Post>();

        public List<Post> GetListOfAllPosts()
        {
            var result = DataDictionary.Values.ToList();
            return result;
        }

        public Post GetAPostWithId(string idValue)
        {
            var id = new ObjectId(idValue);
            return DataDictionary[id];
        }

        public void Create(Post aPost)
        {
            var myId = ObjectId.GenerateNewId();
            aPost.Comments = new List<Comment>();
            aPost.Id = myId;
            DataDictionary.Add(myId, aPost);
        }

        public void Edit(Post aPost, string idValue)
        {
            var updatedPost = GetAPostWithId(idValue);
            updatedPost.Title = aPost.Title;
            updatedPost.Body = aPost.Body;
            updatedPost.CharCount = aPost.CharCount;
            DataDictionary[updatedPost.Id] = updatedPost;
        }

        public void Remove(string idValue)
        {
            var id = new ObjectId(idValue);
            DataDictionary.Remove(id);
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
        }
    }
}