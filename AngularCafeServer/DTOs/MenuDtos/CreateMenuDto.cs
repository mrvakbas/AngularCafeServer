namespace AngularCafeServer.DTOs.MenuDtos
{
    public class CreateMenuDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
    }
}