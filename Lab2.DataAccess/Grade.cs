using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lab2.DataAccess
{
    public class Grade
    {
        [Key]
        public int Id { get; set; }

            
        public int? Value { get; set; }

        [StringLength(200)]
        public string? Comment { get; set; }

        //public Student Student { get; set; }

        [Required]
        public Subject Subject { get; set; }
    }
}
