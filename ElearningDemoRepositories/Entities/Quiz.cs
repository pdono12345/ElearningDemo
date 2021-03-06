using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElearningDemoRepositories.Entities
{
    public class Quiz
    {
        public int Id { get; set; }
        public string? Question { get; set; }
        public string? OptionA { get; set; }
        public string? OptionB { get; set; }
        public string? OptionC { get; set; }
        public string? OptionD { get; set; }
        public CorrectOption CorrectOption { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsValid { get; set; }
        public bool IsDeleted { get; set; }
        public int ClassroomId { get; set; }

        public Classroom? Classroom { get; set; }
        public ICollection<Student>? Students { get; set; }
    }
    public enum CorrectOption
    {
        A, B, C, D
    }
}
