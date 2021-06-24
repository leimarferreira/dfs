namespace ProjetoDFS.Resources
{
    public class ProductResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Value { get; set; }
        public string Note { get; set; }
        public string ImageDataURL { get; set; }
        public CompanyResource Company { get; set; }
    }
}
