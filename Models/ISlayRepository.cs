namespace IntexQueensSlay.Models
{
    public interface ISlayRepository
    {
        public IQueryable<Customer> Customers { get; }
        public IQueryable<Product> Products { get; }
        public IQueryable<LineItem> LineItems { get; }
        public IQueryable<Order> Orders { get; }

        Product GetProductById(int id);
    }
}
