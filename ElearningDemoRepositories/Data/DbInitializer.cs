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

            if (context.Managers.Any())
            {
                return;   // DB has been seeded
            }

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
                new Member{ Username="T002", Password="T002", CreatedAt=DateTime.Now, IsValid=true, IsDeleted=false },
                new Member{ Username="T003", Password="T003", CreatedAt=DateTime.Now, IsValid=true, IsDeleted=false },
                new Member{ Username="S001", Password="S001", CreatedAt=DateTime.Now, IsValid=true, IsDeleted=false },
                new Member{ Username="S002", Password="S002", CreatedAt=DateTime.Now, IsValid=true, IsDeleted=false },
                new Member{ Username="S003", Password="S003", CreatedAt=DateTime.Now, IsValid=true, IsDeleted=false },
            };
            context.Members.AddRange(members);
            context.SaveChanges();

            var teachers = new Teacher[]
            {
                new Teacher
                {
                    MemberId = 1,
                    Name = "Steve",
                    CreatedAt = DateTime.Now,
                    IsValid = true,
                    IsDeleted = false,
                },
                new Teacher
                {
                    MemberId = 2,
                    Name = "Kenny",
                    CreatedAt = DateTime.Now,
                    IsValid = true,
                    IsDeleted = false,
                },
                new Teacher
                {
                    MemberId = 3,
                    Name = "Mike",
                    CreatedAt = DateTime.Now,
                    IsValid = true,
                    IsDeleted = false,
                },
            };
            context.Teachers.AddRange(teachers);
            context.SaveChanges();

            var students = new Student[]
            {
                new Student{ Name="Steph", MemberId=4, CreatedAt=DateTime.Now, IsValid=true, IsDeleted=false},
                new Student{ Name="Klay", MemberId=5, CreatedAt=DateTime.Now, IsValid=true, IsDeleted=false},
                new Student{ Name="Draymond", MemberId=6, CreatedAt=DateTime.Now, IsValid=true, IsDeleted=false},
            };
            context.Students.AddRange(students);
            context.SaveChanges();

            var classrooms = new Classroom[]
            {
                new Classroom
                {
                    TeacherId = 1,
                    Name = "101",
                    CreatedAt = DateTime.Now,
                    IsValid = true,
                    IsDeleted = false,     
                },
                new Classroom
                {
                    TeacherId = 2,
                    Name = "102",
                    CreatedAt = DateTime.Now,
                    IsValid = true,
                    IsDeleted = false,
                },
                new Classroom
                {
                    TeacherId = 3,
                    Name = "103",
                    CreatedAt = DateTime.Now,
                    IsValid = true,
                    IsDeleted = false,
                },
            };
            context.Classrooms.AddRange(classrooms);
            context.SaveChanges();
        }
    }
}
