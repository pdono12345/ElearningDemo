using ElearningDemo.ViewModels;
using ElearningDemoServices.DTOs;

namespace ElearningDemo;

public class AutoMapping : ElearningDemoServices.AutoMapping
{
    public AutoMapping()
    {
        CreateMap<TeacherDTO, HomeTeacherViewModel>();
    }
}
