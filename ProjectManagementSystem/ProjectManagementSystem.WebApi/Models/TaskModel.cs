using System;
using System.Collections.Generic;

namespace ProjectManagementSystem.WebApi.Models
{
    public class TaskModel:ItemModelBase
    {
        public TaskModel() : base()
        {

        }
        public TaskModel(int id, string name, string description, DateTime startDate, DateTime finishDate, State state, List<TaskModel> subTasks) : base(id)
        {
            Name = name;
            Description = description;
            StartDate = startDate;
            FinishDate = finishDate;
            State = state;
            SubTasks = subTasks;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public State State { get; set; }
        public List<TaskModel> SubTasks { get; set; }
    }
}