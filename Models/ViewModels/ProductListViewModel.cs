namespace IntexQueensSlay.Models.ViewModels
{
    public class ProductListViewModel
    {
        public IQueryable<Product> Products { get; set; }
        public PaginationInfo PaginationInfo { get; set; } = new PaginationInfo();
        public string? CurrentProductCat { get; set; }
        public string? CurrentPrimaryColor { get; set; }
        public string? CurrentAllColor { get; set; }

        public string? CategoryFilterTitle {  get; set; }
        public string? PrimaryColorFilterTitle {  get; set; }
        public string? AllColorFilterTitle { get; set; }
    }
}