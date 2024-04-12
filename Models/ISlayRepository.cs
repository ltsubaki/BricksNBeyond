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
        void AddProduct(Products product);

        void RemoveProduct(Products product);

        void RemoveCustomer(Customers customer);
   
        void Update(Products product);
        void SaveChanges();

        public void AddCustomer(Customers task);
        public void EditCustomer(Customers task);

        Customers GetCustomerById(int id);

        Orders GetOrderById(int id);

        void UpdateCustomer(Customers customer);
    }
}


