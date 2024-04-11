using IntexQueensSlay.Data;
using IntexQueensSlay.Models;

namespace IntexQueensSlay.Models
{
    public interface ISlayRepository
    {
        public IQueryable<Customers> Customers { get; }
        public IQueryable<Products> Products { get; }
        public IQueryable<LineItems> LineItems { get; }
        public IQueryable<Orders> Orders { get; }

        Products GetProductById(int id);

        void Update(Products product);
        void SaveChanges();
    }
}


