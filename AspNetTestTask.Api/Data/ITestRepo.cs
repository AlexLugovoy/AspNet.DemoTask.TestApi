using AspNetTestTask.Api.Models;
using System.Collections.Generic;

namespace AspNetTestTask.Api.Data
{
    public interface ITestRepo
    {
        IEnumerable<Test> GetAllTests();
        IEnumerable<Question> GetQuestionsByTestId(int id);
        IEnumerable<Answer> GetAnswersByQuestId(int id);
        Answer CheckAnswer(Answer answer);
    }
}
