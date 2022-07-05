using ElearningDemoRepositories.IRepositories;
using ElearningDemoRepositories.Repositories;
using ElearningDemoServices.Services;

namespace ElearningDemo.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IManagerService, ManagerService>();
        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<IMemberService, MemberService>();
        //services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<ITeacherService, TeacherService>();
        //services.AddScoped<IClassroomService, ClassroomService>();

        services.AddScoped<IManagerRepository, ManagerRepository>();
        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<ITeacherRepository, TeacherRepository>();
        services.AddScoped<IClassroomRepository, ClassroomRepository>();
        services.AddScoped<IQuizRepository, QuizRepository>();

        return services;
    }
}
