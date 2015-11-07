using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlogSite.Models;

namespace BlogSite.viewModels
{
    public class CommentViewModel:Comment
    {
        public string ThePostId  { get; set; }

        public Post ThePost { get; set; }

        
    }
}