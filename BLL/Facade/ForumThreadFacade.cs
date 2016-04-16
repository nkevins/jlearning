using DAL.Repository;
using DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Facade
{
    public class ForumThreadFacade
    {
        private IUnitOfWork _uow;

        public ForumThreadFacade()
        {
            _uow = new UnitOfWork();
        }

        public ForumThreadFacade(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public ForumThread GetById(int forumThreadId)
        {
            return _uow.ForumThreadRepository.GetById(forumThreadId);
        }

        public List<ForumThread> GetBySchedule(int scheduleId)
        {
            return _uow.ForumThreadRepository.GetAll().Where(x => x.ScheduleID == scheduleId && x.ObsInd == "N").OrderByDescending(x => x.CreatedDate).ToList();
        }

        public void Add(ForumThread forum)
        {
            _uow.ForumThreadRepository.Insert(forum);
            _uow.Save();
        }
    }
}
