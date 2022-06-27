using ElearningDemoRepositories.Entities;
using System;
using System.Linq;

namespace ElearningDemoRepositories.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ElearningDemoContext context)
        {
            context.Database.EnsureCreated();

            var manager = new Manager
            {
                Name = "Developer",
                Username = "admin",
                Password = "0000",
                CreatedAt = DateTime.Now,
                IsValid = true,
                IsDeleted = false
            };
            context.Managers.Add(manager);
            context.SaveChanges();

            var members = new Member[]
            {
                new Member{ Username="T001", Password="T001", CreatedAt=DateTime.Now, IsValid=true, IsDeleted=false },
                new Member{ Username="S001", Password="S001", CreatedAt=DateTime.Now, IsValid=true, IsDeleted=false },
                new Member{ Username="S002", Password="S002", CreatedAt=DateTime.Now, IsValid=true, IsDeleted=false },
            };
            context.Members.AddRange(members);
            context.SaveChanges();

            var teacher = new Teacher
            {
                MemberId = 1,
                Name = "Peter",
                CreatedAt = DateTime.Now,
                IsValid = true,
                IsDeleted = false,
            };
            context.Teachers.Add(teacher);
            context.SaveChanges();

            var students = new Student[]
            {
                new Student{ Name="James", MemberId=2, CreatedAt=DateTime.Now, IsValid=true, IsDeleted=false},
                new Student{ Name="Kevin", MemberId=3, CreatedAt=DateTime.Now, IsValid=true, IsDeleted=false},
            };
            context.Students.AddRange(students);
            context.SaveChanges();

            var classroom = new Classroom
            {
                TeacherId = 1,
                Name = "101",
                CreatedAt = DateTime.Now,
                IsValid = true,
                IsDeleted = false,
            };
            context.Classrooms.Add(classroom);
            context.SaveChanges();
        }
    }
}
