using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using OfficeOpenXml;
using ProjectManagementSystem.WebApi.Models;
using ProjectManagementSystem.WebApi.Services;

namespace ProjectManagementSystem.UnitTest.IntegrationTests.Services
{
    [TestFixture]
    public class ReportServiceIntegrationTest
    {
        private const string ReportFileName = "report.xlsx";
        private ReportService _reportService;

        [SetUp]
        public void Init()
        {
            _reportService = new ReportService();
        }

        [Test]
        public void CreateItemsInProgressReportTest()
        {
            var projects = CreateTestProjectModels();
            var tasks = CreateTestTaskModels();

            var workingDirectory = Environment.CurrentDirectory;
            var projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            var expectedFilePath = Path.Combine(projectDirectory, $"Resources\\{ReportFileName}");

            var tempFilePath = Path.Combine(projectDirectory, "tmp");

            if (!Directory.Exists(tempFilePath))
            {
                Directory.CreateDirectory(tempFilePath);
            }

            var actualFilePath = Path.Combine(tempFilePath, ReportFileName);
            var streamMemory = _reportService.Generate(projects, tasks);

            using (var excelPackage = new ExcelPackage())
            {
                var file = File.Create(actualFilePath);

                excelPackage.Load(streamMemory);
                excelPackage.SaveAs(file);
                file.Close();
            }

            var filesAreEqual = FilesAreEqual(new FileInfo(actualFilePath), new FileInfo(expectedFilePath));
            Directory.Delete(tempFilePath, true);

            Assert.IsTrue(filesAreEqual);
        }

        private List<TaskModel> CreateTestTaskModels()
        {
            var date = new DateTime(2020, 1, 1);

            return new List<TaskModel>()
            {
                new TaskModel(
                    1,
                    "Some Name",
                    "Some Description",
                    date,
                    date.AddDays(1),
                    State.InProgress,
                    new List<TaskModel>()),
                new TaskModel(
                    2,
                    "Some Name2",
                    "Some Description2",
                    date,
                    date.AddDays(1),
                    State.InProgress,
                    new List<TaskModel>())
            };
        }

        private List<ProjectModel> CreateTestProjectModels()
        {
            var date = new DateTime(2020, 1, 1);

            return new List<ProjectModel>()
            {
                new ProjectModel(
                    1,
                    null,
                    "Some Code",
                    "Some Name",
                    date,
                    date.AddDays(1),
                    State.InProgress,
                    new List<TaskModel>(),
                    new List<ProjectModel>()),
                new ProjectModel(
                    2,
                    null,
                    "Some Code2",
                    "Some Name2",
                    date,
                    date.AddDays(1),
                    State.InProgress,
                    new List<TaskModel>(),
                    new List<ProjectModel>())
            };
        }

        private static bool FilesAreEqual(FileInfo expectedFile, FileInfo actualFile)
        {
            if (expectedFile.Length != actualFile.Length)
                return false;

            using var expectedStream = expectedFile.OpenRead();
            using var actualStream = actualFile.OpenRead();
            var expectedExcelPackage = new ExcelPackage(expectedStream);
            var actualExcelPackage = new ExcelPackage(actualStream);
            var expectedWorksheetNames = GetWorksheetNames(expectedExcelPackage);
            var actualWorksheetName = GetWorksheetNames(actualExcelPackage);
            CollectionAssert.AreEqual(expectedWorksheetNames, actualWorksheetName);

            var expectedWorkSheets = expectedExcelPackage.Workbook.Worksheets;
            var actualWorkSheets = actualExcelPackage.Workbook.Worksheets;

            // TODO: Implement checking for data equality

            return true;
        }

        private static string GetCellValue(ExcelRange cells, int cellRowIndex, int cellColumnIndex)
        {
            var cellValue = cells[cellRowIndex, cellColumnIndex].Value;
            return cellValue?.ToString();
        }

        private static IEnumerable<string> GetWorksheetNames(ExcelPackage excelPackage)
        {
            return excelPackage.Workbook.Worksheets.Select(sheet => sheet.Name);
        }
    }
}