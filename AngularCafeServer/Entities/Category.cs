namespace AngularCafeServer.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Menu> Menus { get; set; }
    }
}
