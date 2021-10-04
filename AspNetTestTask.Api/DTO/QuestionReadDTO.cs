using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetTestTask.Api.DTO
{
    public class QuestionReadDTO
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public int ParentId { get; set; }
    }
}
