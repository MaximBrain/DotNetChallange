using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using OfficeOpenXml;
using ProjectManagementSystem.WebApi.Models;

namespace ProjectManagementSystem.WebApi.Services
{
    public class ReportService
    {
        public MemoryStream Generate(IEnumerable<ProjectModel> projects, IEnumerable<TaskModel> tasks)
        {
            var columns = new List<string> {"Id"};
            var projectPropertyNames = GetModelPropertyNames(typeof(ProjectModel));
            var taskPropertyNames = GetModelPropertyNames(typeof(TaskModel));

            using (var excelPackage = new ExcelPackage())
            {
                var projectsWorksheet = excelPackage.Workbook.Worksheets.Add("ProjectsInProgress");
                var projectsColumns = columns.Union(projectPropertyNames.Keys).ToList();

                var tasksWorksheet = excelPackage.Workbook.Worksheets.Add("TasksInProgress");
                var tasksColumns = columns.Union(taskPropertyNames.Keys).ToList();

                FillWorksheet(projects, projectPropertyNames, projectsColumns, projectsWorksheet);
                FillWorksheet(tasks, taskPropertyNames, tasksColumns, tasksWorksheet);

                excelPackage.Save();
                return new MemoryStream(excelPackage.GetAsByteArray());
            }
        }

        // TODO: Implement the method with
        private static void FillWorksheet<TModel>(
            IEnumerable<TModel> models,
            Dictionary<string, PropertyInfo> propertyNames,
            List<string> columns,
            ExcelWorksheet projectsWorksheet) where TModel: ItemModelBase
        {
        }


        private static Dictionary<string, PropertyInfo> GetModelPropertyNames(Type modelType)
        {
            return modelType.GetProperties().Where(property => property.PropertyType.IsValueType || property.PropertyType == typeof(string)).ToDictionary(property => property.Name);
        }
    }
}