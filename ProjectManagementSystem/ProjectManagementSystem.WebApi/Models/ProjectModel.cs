using System;
using System.Collections.Generic;
using ProjectManagementSystem.Domain.Entities;

namespace ProjectManagementSystem.WebApi.Models
{
    public class ProjectModel: ItemModelBase
    {
        public ProjectModel():base()
        {

        }
        public ProjectModel(int id, int? parentId, string code, string name, DateTime startDate, DateTime finishDate, State state, List<TaskModel> tasks, List<ProjectModel> subProjects) : base(id)
        {
            ParentId = parentId;
            Code = code;
            Name = name;
            StartDate = startDate;
            FinishDate = finishDate;
            State = state;
            Tasks = tasks;
            SubProjects = subProjects;
        }

        public int? ParentId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public State State { get; set; }
        public List<TaskModel> Tasks { get; set; }
        public List<ProjectModel> SubProjects { get; set; }
    }
}