using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManagementSystem.WebApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            CreateTables(migrationBuilder);

            InsertProjectRow(migrationBuilder, null, "SomeCode1", "Some Name1", DateTime.Today, DateTime.Today.AddDays(1));
            InsertProjectRow(migrationBuilder, 1, "SomeCode1_1", "Some Name1_1", DateTime.Today, DateTime.Today.AddDays(1));
            InsertProjectRow(migrationBuilder, null, "SomeCode2", "Some Name2", DateTime.Today,DateTime.Today.AddDays(1));

            InsertTaskRow(migrationBuilder, 1, null, "Some Task Name 1", "Some Task Description 1", DateTime.Today, DateTime.Today.AddDays(1),0);
            InsertTaskRow(migrationBuilder, 1, 1, "Some Task Name 1_1", "Some Task Description 1_1", DateTime.Today, DateTime.Today.AddDays(1),1);
            InsertTaskRow(migrationBuilder, 1, null, "Some Task Name 2", "Some Task Description 2", DateTime.Today, DateTime.Today.AddDays(1), 1);
            InsertTaskRow(migrationBuilder, 2, null, "Some Task Name 3", "Some Task Description 3", DateTime.Today, DateTime.Today.AddDays(1), 1);
            InsertTaskRow(migrationBuilder, 3, null, "Some Task Name 4", "Some Task Description 4", DateTime.Today, DateTime.Today.AddDays(1), 1);
            InsertTaskRow(migrationBuilder, 3, 6, "Some Task Name 4_1", "Some Task Description 4_1", DateTime.Today, DateTime.Today.AddDays(1), 1);
        }

        

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Tasks");
        }

        private static void CreateTables(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    FinishDate = table.Column<DateTime>(nullable: false),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Projects", x => x.Id); });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    FinishDate = table.Column<DateTime>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    State = table.Column<int>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Tasks", x => x.Id); });
        }

        private static void InsertTaskRow(MigrationBuilder migrationBuilder, int projectId, int? parentId, string taskName, string taskDescription, DateTime startDate, DateTime finishDate, int state)
        {
            migrationBuilder.InsertData(
                "Tasks",
                columns: new[] { "ProjectId", "ParentId", "Name", "Description", "StartDate", "FinishDate", "State" },
                values: new object[]
                {
                    projectId, parentId, taskName,taskDescription, startDate, finishDate, state
                });
        }

        private static void InsertProjectRow(MigrationBuilder migrationBuilder, int? parentId, string code, string name, DateTime startDate, DateTime finishDate)
        {
            migrationBuilder.InsertData(
                "Projects",
                columns: new[] { "ParentId", "Code", "Name", "StartDate", "FinishDate" },
                values: new object[]
                {
                    parentId, code, name, startDate, finishDate
                    
                });
        }
    }
}
