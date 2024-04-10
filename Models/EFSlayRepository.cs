using IntexQueensSlay.Data;
using IntexQueensSlay.Models;

namespace IntexQueensSlay.Models
{
    public class EFSlayRepository : ISlayRepository
    {
        private LegoContext _context;

        public EFSlayRepository(LegoContext temp)
        {
            _context = temp;
        }

        public IQueryable<Customer> Customers => _context.Customers;
        public IQueryable<Product> Products => _context.Products;
        public IQueryable<LineItem> LineItems => _context.LineItems;
        public IQueryable<Order> Orders => _context.Orders;

        public Product GetProductById(int id)
        {
            return _context.Products.Find(id);
        }    
            public void Update(Product product)
        {
            _context.Update(product);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

    }
}

