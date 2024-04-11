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

        public IQueryable<Customers> Customers => _context.Customers;
        public IQueryable<Products> Products => _context.Products;
        public IQueryable<LineItems> LineItems => _context.LineItems;
        public IQueryable<Orders> Orders => _context.Orders;

        public Products GetProductById(int id)
        {
            return _context.Products.Find(id);
        }    
            public void Update(Products product)
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

