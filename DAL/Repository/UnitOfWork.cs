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
        IRepository<ForumThread> ForumThreadRepository { get; }
        IRepository<ForumPost> ForumPostRepository { get; }
        void Save();
    }

    public partial class UnitOfWork : IUnitOfWork
    {
        private IRepository<Course> _courseRepository;
        private IRepository<User> _userRepository;
        private IRepository<Role> _userRoleRepository;
        private IRepository<ForumThread> _forumThreadRepository;
        private IRepository<ForumPost> _forumPostRepository;
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

        public IRepository<Role> UserRoleRepository
        {
            get
            {

                if (_userRoleRepository == null)
                    _userRoleRepository = new Repository<Role>(_context);

                return _userRoleRepository;
            }
        }

        public IRepository<ForumThread> ForumThreadRepository
        {
            get
            {
                if (_forumThreadRepository == null)
                    _forumThreadRepository = new Repository<ForumThread>(_context);

                return _forumThreadRepository;
            }
        }

        public IRepository<ForumPost> ForumPostRepository
        {
            get
            {
                if (_forumPostRepository == null)
                    _forumPostRepository = new Repository<ForumPost>(_context);

                return _forumPostRepository;
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
