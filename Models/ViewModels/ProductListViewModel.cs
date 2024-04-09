namespace IntexQueensSlay.Models.ViewModels
{
    public class ProductListViewModel
    {
        public IQueryable<Product> Products { get; set; }
        public PaginationInfo PaginationInfo { get; set; } = new PaginationInfo();
        public string? CurrentProductCat { get; set; }
    }
}