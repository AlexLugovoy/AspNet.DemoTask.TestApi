using AspNetTestTask.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetTestTask.Api.DTO
{
    public class ResultDTO
    {
        public AnswerReadDTO[] Answers { get; set; }
    }

    public class ResultResponseDTO 
    {
        public int CountOfAnswer { get; set; }
        public int CountOfRightAnswers { get; set; }
    }
}
