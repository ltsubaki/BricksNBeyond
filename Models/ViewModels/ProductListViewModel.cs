namespace IntexQueensSlay.Models.ViewModels
{
    public class ProductListViewModel
    {
        public IQueryable<Products> Products { get; set; }
        public PaginationInfo PaginationInfo { get; set; } = new PaginationInfo();
        public string? CurrentAllCat { get; set; }
        public string? CurrentAllColor { get; set; }

        public string? AllCatFilterTitle {  get; set; }
        public string? AllColorFilterTitle { get; set; }
    }
}