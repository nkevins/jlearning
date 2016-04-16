using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using DL.ModelView;
using Newtonsoft.Json;

namespace JLearnWeb
{
    public class ForumHub : Hub
    {
        public void SendMessage(string forumId, string message, string userId)
        {
            ForumPostModelView post = new ForumPostModelView();
            post.Message = message;
            post.Name = userId;
            post.CreatedDate = DateTime.Now;

            // TODO: Save into database

            Clients.All.broadcast(forumId, JsonConvert.SerializeObject(post));
        }
    }
}