using System.ComponentModel.DataAnnotations;


namespace AspNetTestTask.Api.Models
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }
        public string Body { get; set; }
        public int ParentId { get; set; }
        public bool Status { get; set; }
    }
}
