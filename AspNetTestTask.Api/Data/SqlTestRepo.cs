using AspNetTestTask.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetTestTask.Api.Data
{
    public class SqlTestRepo : ITestRepo
    {
        private AppDbContext _context;
        public SqlTestRepo(AppDbContext context)
        {
            _context = context;
        }

        public Answer CheckAnswer(Answer answer)
        {
            return _context.Answers.FirstOrDefault(p => p.Id == answer.Id);
        }

        public IEnumerable<Test> GetAllTests()
        {
            return _context.Tests.ToList();
        }

        public IEnumerable<Answer> GetAnswersByQuestId(int id)
        {
            return _context.Answers.Where(p => p.ParentId == id).ToList();
        }

        public IEnumerable<Question> GetQuestionsByTestId(int id)
        {
            return _context.Questions.Where(p => p.ParentId == id).ToList();
        }
    }
}
