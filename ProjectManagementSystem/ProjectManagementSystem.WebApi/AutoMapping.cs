using AutoMapper;
using ProjectManagementSystem.Domain.Entities;
using ProjectManagementSystem.WebApi.Models;

namespace ProjectManagementSystem.WebApi
{
    public class AutoMapping: Profile
    {
        public AutoMapping()
        {
            CreateMap<TaskModel, Task>();
            CreateMap<ProjectModel, Project>();

            CreateMap<Task, TaskModel>();
            CreateMap<Project, ProjectModel>();
        }
    }
}