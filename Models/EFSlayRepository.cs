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
        public IQueryable<AspNetUsers> AspNetUserss => _context.AspNetUserss;

        public Product ? GetProductById(int id)
        {
            return _context.Products.Find(id);
        }

        public Order ? GetOrderById(int id)
        {
            return _context.Orders.Find(id);
        }
        public void Update(Product product)
        {
            _context.Update(product);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void RemoveCustomer(Customer customer)
        {
            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }


        public void RemoveProduct(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public void AddCustomer(Customer task)
        {
            _context.Add(task);
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

