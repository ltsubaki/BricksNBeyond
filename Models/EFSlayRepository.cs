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

        public IQueryable<AspNetUsers> AspNetUserss => throw new NotImplementedException();

        public Products GetProductById(int id)
        {
            return _context.Products.Find(id);
        }

        public Orders ? GetOrderById(int id)
        {
            return _context.Orders.Find(id);
        }
        public void Update(Products product)
        {
            _context.Update(product);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void AddProduct(Products product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void AddOrder(Orders order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void 
            Customer(Customers customer)
        {
            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }


        public void RemoveProduct(Products product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public void AddCustomer(Customers task)
        {
            _context.Customers.Add(task);
            _context.SaveChanges();
        }

        public void DeleteCustomer(Customers task)
        {
            _context.Remove(task);
            _context.SaveChanges();
        }

        public void EditCustomer(Customers task)
        {
            _context.Update(task);
            _context.SaveChanges();
        }

        public Customers GetCustomerById(int id)
        {
            return _context.Customers.Find(id);
        }
        public void UpdateCustomer(Customers customer)
        {
            _context.Update(customer);
        }

        public void RemoveCustomer(Customers customer)
        {
            throw new NotImplementedException();
        }

        //public void ClearCart()
        //{
        //    var userCartItems = _context.LineItems;
        //    _context.LineItems.RemoveRange(userCartItems);
        //    _context.SaveChanges();
        //}

        //public Products GetProductById(int id)
        //{
        //    return _context.Products.Find(id);
        //}

    }
}

