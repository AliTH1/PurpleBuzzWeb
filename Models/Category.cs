namespace PurpleBuzzWeb.Models
{
    public class Category
    {
        public Category()
        {
            Services = new List<Service>();
        }

        public int Id { get; set; }
        public int Name { get; set; }
        public bool IsDeleted { get; set; }
        public List<Service> Services { get; set; }
    }
}
