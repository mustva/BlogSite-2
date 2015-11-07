using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlogSite.Models;
using BlogSite.viewModels;

namespace BlogSite.Repository
{
    public interface IRepository
    {
        List<Post> GetListOfAllPosts();
        Post GetAPostWithId(string idValue);
        void Create(Post aPost);
        void Edit(Post aPost, string idValue);
        void Remove(string idValue);
        void AddComment(CommentViewModel aCommentvm);
    }
}