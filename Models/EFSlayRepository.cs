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

        public void AddCustomer(Customer task)
        {
            _context.Add(task);
            _context.SaveChanges();
        }

        public void DeleteCustomer(Customer task)
        {
            _context.Remove(task);
            _context.SaveChanges();
        }

        public void EditCustomer(Customer task)
        {
            _context.Update(task);
            _context.SaveChanges();
        }

        public Customer GetCustomerById(int id)
        {
            return _context.Customers.Find(id);
        }
        public void UpdateCustomer(Customer customer)
        {
            _context.Update(customer);
        }

    }
}

