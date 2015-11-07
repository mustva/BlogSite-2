using System;
using System.Web.Mvc;
using BlogSite.Models;
using BlogSite.Repository;
using BlogSite.viewModels;

namespace BlogSite.Controllers
{
    public class BlogController : Controller
    {
        private readonly IRepository _dataRepository = new DictionaryRepository();
        // GET: Blog
        public ActionResult Index()
        {
            var list = _dataRepository.GetListOfAllPosts();
            return View(list);
        }

        // GET: Blog/Details/5
        public ActionResult Details(string id)
        {
            var aPost = _dataRepository.GetAPostWithId(id);
            return View(aPost);
        }

        // GET: Blog/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Blog/Create
        [HttpPost]
        public ActionResult Create(Post aPost)
        {
            try
            {
                _dataRepository.Create(aPost);


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Blog/Edit/5
        public ActionResult Edit(string id)
        {
            var aPost = _dataRepository.GetAPostWithId(id);
            return View("EditPost", aPost);
        }

        // POST: Blog/Edit/5
        [HttpPost]
        public ActionResult Edit(String id, Post aPost)
        {
            try
            {
                // TODO: Add update logic here
                _dataRepository.Edit(aPost, id);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // GET: Blog/Delete/5
        public ActionResult Delete(string id)
        {
            var aPost = _dataRepository.GetAPostWithId(id);
            return View(aPost);
        }

        // POST: Blog/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, Post aPost)
        {
            try
            {
                _dataRepository.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult CreateComment(string id)
        {
            var aPost = _dataRepository.GetAPostWithId(id);
            var vm = new CommentViewModel();
            vm.ThePost = aPost;
            vm.ThePostId = id;
            return View("CreatePostComment", vm);
        }

        [HttpPost]
        public ActionResult CreateComment(CommentViewModel vm)
        {
            try
            {
                vm.TimePosted = DateTime.Now;
                _dataRepository.AddComment(vm);
                //Post aPost =_dataRepository.GetAPostWithId(postId);
                return RedirectToAction("Details", new {id = vm.ThePostId});
            }
            catch
            {
                return View("Index");
            }
        }
    }
}