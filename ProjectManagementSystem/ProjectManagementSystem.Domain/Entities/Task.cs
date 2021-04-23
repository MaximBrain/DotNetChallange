namespace ProjectManagementSystem.Domain.Entities
{
    public class Task: ItemBase
    {
        public int ProjectId { get; set; }
        public string Description { get; set; }
        public int State { get; set; }
    }
}