namespace ProjectManagementSystem.WebApi.Models
{
    public abstract class ItemModelBase
    {
        protected ItemModelBase()
        {

        }
        protected ItemModelBase(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}