using DL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class Context : DbContext
    {
        public Context()
            : base("name=JLearnDBEntities")
        {

        }
        public DbSet<Course> courses { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<ForumThread> forumThreads { get; set; }
        public DbSet<ForumPost> forumPosts { get; set; }
        public DbSet<Schedule> schedule { get; set; }
        public DbSet<Quiz> quizes { get; set; }
        public DbSet<QuizQuestion> quizQuestions { get; set; }
    }
}
