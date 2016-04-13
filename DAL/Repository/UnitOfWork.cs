using DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Course> CourseRepository { get; }
        void Save();
    }

    public partial class UnitOfWork : IUnitOfWork
    {
        private IRepository<Course> _courseRepository;
        private IRepository<User> _userRepository;
        private Context _context;

        //Add any new repository here 

        public IRepository<Course> CourseRepository
        {
            get
            {

                if (_courseRepository == null)
                    _courseRepository = new Repository<Course>(_context);

                return _courseRepository;
            }
        }

        public IRepository<User> UserRepository
        {
            get
            {

                if (_userRepository == null)
                    _userRepository = new Repository<User>(_context);

                return _userRepository;
            }
        }

        public UnitOfWork()
        {
            _context = new Context();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
