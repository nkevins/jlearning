using DAL.Repository;
using DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Facade
{
    public class ForumPostFacade
    {
        private IUnitOfWork _uow;

        public ForumPostFacade()
        {
            _uow = new UnitOfWork();
        }

        public ForumPostFacade(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void Add(ForumPost post)
        {
            _uow.ForumPostRepository.Insert(post);
            _uow.Save();
        }
    }
}
