using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using DL.ModelView;
using Newtonsoft.Json;
using BLL.Facade;
using DL;
using log4net;

namespace JLearnWeb
{
    [Authorize]
    public class ForumHub : Hub
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ForumHub));
        private ForumPostFacade _postFac;
        private UserFacade _userFac;

        public ForumHub()
        {
            _postFac = new ForumPostFacade();
            _userFac = new UserFacade();
        }

        public void SendMessage(int forumId, string message, int userId)
        {
            log.Info("Receive message from client forumId: " + forumId + " message: " + message + " userId: " + userId);

            try
            {
                User usr = _userFac.GetById(userId);
                ForumPostModelView post = new ForumPostModelView();
                post.Message = message;
                post.Name = usr.Name;
                post.CreatedDate = DateTime.Now;

                // Saving into database
                ForumPost p = new ForumPost();
                p.ThreadID = forumId;
                p.UserID = userId;
                p.Description = message;
                p.CreatedDate = post.CreatedDate;
                p.ObsInd = "N";

                _postFac.Add(p);

                log.Info("Save to DB: " + JsonConvert.SerializeObject(p));

                Clients.All.broadcast(forumId, JsonConvert.SerializeObject(post));

                log.Info("Broadcast forumId: " + forumId + " message: " + JsonConvert.SerializeObject(post));
            }
            catch (Exception ex)
            {
                log.Error("Exception ", ex);
            }
        }
    }
}