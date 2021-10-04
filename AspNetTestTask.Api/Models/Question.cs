using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetTestTask.Api.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        public string Body { get; set; }
        public int ParentId { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
