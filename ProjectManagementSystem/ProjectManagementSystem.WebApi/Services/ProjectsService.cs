using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ProjectManagementSystem.Domain;
using ProjectManagementSystem.Domain.Entities;
using ProjectManagementSystem.Domain.Repositories;
using ProjectManagementSystem.WebApi.Models;

namespace ProjectManagementSystem.WebApi.Services
{
    public class ProjectsService: ItemServiceBase<ProjectModel, Project>
    {
        private readonly IItemRepository<Task> _tasksRepository;
        public ProjectsService(IItemRepository<Project> projectRepository, IItemRepository<Task> tasksRepository, IMapper autoMapper) : base(projectRepository, autoMapper)
        {
            _tasksRepository = tasksRepository;
        }

        public override IEnumerable<ProjectModel> GetAllItems()
        {
            var projects = Repository.GetAll().Where(IsRoot).ToList();
            foreach (var projectEntity in projects)
            {
                yield return GetProjectWithTasksAndSubTasks(projectEntity);
            }
        }

        public override ProjectModel GetItem(int id)
        {
            var projectEntity = Repository.Get(id);
            return projectEntity == null ? null : GetProjectWithTasksAndSubTasks(projectEntity);
        }

        public List<ProjectModel> GetProjectsInProgress(DateTime date)
        {
            // TODO: What can be optimized
            var projects = Repository.GetAll().Where(project => true/* TODO: Add condition according to date */).ToList().Select(GetProjectWithTasksAndSubTasks);

            return projects.Where(project => project.State == State.InProgress).ToList();
        }

        // TODO: Get all necessary data for ProjectModel with correct Project State based on it's tasks and sub projects with tasks
        private ProjectModel GetProjectWithTasksAndSubTasks(Project projectEntity)
        {
            var projectState = CalculateProjectState(new List<TaskModel>(), new List<ProjectModel>());

            return null;
        }

        // TODO: Implement method to return if the project is root
        private static bool IsRoot(Project project)
        {
            return true;
        }

        // TODO: Calculate State according to requirements
        private static State CalculateProjectState(List<TaskModel> projectTasks, List<ProjectModel> subProjects)
        {
            return State.Completed;
        }
    }
}