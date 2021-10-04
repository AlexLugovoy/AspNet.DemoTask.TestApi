using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AspNetTestTask.Api.Models
{
    public class Test
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
