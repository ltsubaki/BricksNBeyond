﻿using IntexQueensSlay.Data;
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

        public void AddCustomer(Customers task)
        {
            _context.Add(task);
            _context.SaveChanges();
        }

        public void DeleteCustomer(Customers task)
        {
            _context.Remove(task);
            _context.SaveChanges();
        }

        public void EditCustomer(Customers task)
        public void EditCustomer(Customer task)
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

    }
}

