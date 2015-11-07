using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogSite.Repository
{
    public class RepositoryProxy:IRepository
    {
        private bool allowDelete;
        public RepositoryProxy(bool useMongo, bool canDelete)
        {
            allowDelete = canDelete;
            if(useMongo)
                realRepository = new MongoRepository();
            else 
                realRepository = new DictionaryRepository();
        }
        private IRepository realRepository;
        public List<Models.Post> GetListOfAllPosts()
        {
           return realRepository.GetListOfAllPosts();
        }

        public Models.Post GetAPostWithId(string idValue)
        {
           return realRepository.GetAPostWithId(idValue);
        }

        public void Create(Models.Post aPost)
        {
           realRepository.Create(aPost);
        }

        public void Edit(Models.Post aPost, string idValue)
        {
           realRepository.Edit(aPost,idValue);
        }

        public void Remove(string idValue)
        {  if(allowDelete)
            realRepository.Remove(idValue);
        }

        public void AddComment(viewModels.CommentViewModel aCommentvm)
        {
           realRepository.AddComment(aCommentvm);
        }
    }
}