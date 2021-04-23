using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagementSystem.Domain.Entities
{
    public class ItemBase
    {
        [Key]
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
    }
}