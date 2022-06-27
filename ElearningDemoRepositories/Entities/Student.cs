using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElearningDemoRepositories.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsValid { get; set; }
        public bool IsDeleted { get; set; }
        public int MemberId { get; set; }

        public Member Member { get; set; }
        public ICollection<Classroom> Classrooms { get; set; }
        public ICollection<Quiz> Quizzes { get; set; }
    }
}
