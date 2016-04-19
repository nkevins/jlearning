using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.ModelView
{
    public class LiveQuizModelView
    {
        public LiveQuizModelView()
        {
            Choice = new List<QuizChoiceModelView>();
        }

        public int QuestionID { get; set; }
        public string Title { get; set; }
        public List<QuizChoiceModelView> Choice { get; set; }
        public int TotalQuestions { get; set; }
        public int CurrentQuestionIndex { get; set; }
    }

    public class QuizChoiceModelView
    {
        public int ChoiceID { get; set; }
        public string Choice { get; set; }
    }
}
