using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lab2.DataAccess
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Surname { get; set; }

        [Required]
        [StringLength(10)]
        public string Code { get; set; }

        public List<Grade> Grades { get; } = new();
    }
}
