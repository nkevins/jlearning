using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using DL.ModelView;
using Newtonsoft.Json;
using BLL.Facade;
using DL;

namespace JLearnWeb
{
    public class ForumHub : Hub
    {
        private ForumPostFacade _postFac;

        public ForumHub()
        {
            _postFac = new ForumPostFacade();
        }

        public void SendMessage(string forumId, string message, string userId)
        {
            ForumPostModelView post = new ForumPostModelView();
            post.Message = message;
            post.Name = userId;
            post.CreatedDate = DateTime.Now;

            // Saving into database
            ForumPost p = new ForumPost();
            p.ThreadID = int.Parse(forumId);
            p.UserID = int.Parse(userId);
            p.Description = message;
            p.CreatedDate = post.CreatedDate;
            p.ObsInd = "N";

            _postFac.Add(p);

            Clients.All.broadcast(forumId, JsonConvert.SerializeObject(post));
        }
    }
}