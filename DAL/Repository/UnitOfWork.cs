﻿using DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> UserRepository { get; }
        IRepository<Role> UserRoleRepository { get; }
        IRepository<Course> CourseRepository { get; }
        IRepository<ForumThread> ForumThreadRepository { get; }
        IRepository<ForumPost> ForumPostRepository { get; }
        IRepository<Quiz> QuizRepository { get; }
        IRepository<QuizQuestion> QuizQuestionRepository { get; }
        IRepository<QuizChoice> QuizChoiceRepository { get; }
        IRepository<QuizAnswer> QuizAnswerRepository { get; }
        IRepository<Module> ModuleRepository { get; }
        IRepository<Document> DocumentRepository { get; }
        void Save();
    }

    public partial class UnitOfWork : IUnitOfWork
    {
        private IRepository<Course> _courseRepository;
        private IRepository<User> _userRepository;
        private IRepository<Role> _userRoleRepository;
        private IRepository<ForumThread> _forumThreadRepository;
        private IRepository<ForumPost> _forumPostRepository;
        private IRepository<Schedule> schRepo;
        private IRepository<UserSchedule> usrSchRepo;
        private IRepository<Quiz> _quizRepository;
        private IRepository<QuizQuestion> _quizQuestionRepository;
        private IRepository<QuizChoice> _quizChoiceRepository;
        private IRepository<QuizAnswer> _quizAnswerRepository;
        private IRepository<Module> _moduleRepository;
        private IRepository<Document> _documentRepository;
        private Context _context;

        //Add any new repository here 

        public IRepository<UserSchedule> UsrSchRepo
        {
            get
            {

                if (usrSchRepo == null)
                    usrSchRepo = new Repository<UserSchedule>(_context);

                return usrSchRepo;
            }
        }

        public IRepository<Schedule> SchRepo
        {
            get
            {

                if (schRepo == null)
                    schRepo = new Repository<Schedule>(_context);

                return schRepo;
            }
        }

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

        public IRepository<Quiz> QuizRepository
        {
            get
            {
                if (_quizRepository == null)
                    _quizRepository = new Repository<Quiz>(_context);

                return _quizRepository;
            }
        }

        public IRepository<QuizQuestion> QuizQuestionRepository
        {
            get
            {
                if (_quizQuestionRepository == null)
                    _quizQuestionRepository = new Repository<QuizQuestion>(_context);

                return _quizQuestionRepository;
            }
        }

        public IRepository<QuizChoice> QuizChoiceRepository
        {
            get
            {
                if (_quizChoiceRepository == null)
                    _quizChoiceRepository = new Repository<QuizChoice>(_context);

                return _quizChoiceRepository;
            }
        }

        public IRepository<QuizAnswer> QuizAnswerRepository
        {
            get
            {
                if (_quizAnswerRepository == null)
                    _quizAnswerRepository = new Repository<QuizAnswer>(_context);

                return _quizAnswerRepository;
            }
        }

        public IRepository<Module> ModuleRepository
        {
            get
            {
                if (_moduleRepository == null)
                    _moduleRepository = new Repository<Module>(_context);

                return _moduleRepository;
            }
        }

        public IRepository<Document> DocumentRepository
        {
            get
            {
                if (_documentRepository == null)
                    _documentRepository = new Repository<Document>(_context);

                return _documentRepository;
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
