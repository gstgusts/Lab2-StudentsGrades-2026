using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lab2.DataAccess
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(2000)]
        public string? Description { get; set; }
    }
}
