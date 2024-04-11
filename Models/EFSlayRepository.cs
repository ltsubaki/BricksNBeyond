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

    }
}

