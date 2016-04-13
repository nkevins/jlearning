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

        public List<ForumThread> GetBySchedule(int scheduleId)
        {
            return _uow.ForumThreadRepository.GetAll().Where(x => x.ScheduleID == scheduleId).ToList();
        }
    }
}
