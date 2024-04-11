using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace IntexQueensSlay.Models
{
    public class Cart
    {
        public List<CartLine> Lines { get; set; } = new List<CartLine>();

        public virtual void AddItem(Products prod, int quantity)
        {
            CartLine? line = Lines
                .Where(x => x.Product.ProductId == prod.ProductId)
                .FirstOrDefault();

            if (line == null)
            {
                Lines.Add(new CartLine
                {
                    Product = prod,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(Products prod) => Lines.RemoveAll(x => x.Product.ProductId == prod.ProductId);

        public virtual void Clear() => Lines.Clear();

        public decimal CalculateTotal() => (decimal)Lines.Sum(x => x.Product.Price * x.Quantity);

        public class CartLine
        {
            public int CartLineId { get; set; }
            public Products Product { get; set; } = new();
            public int Quantity { get; set; }

        }
    }
}
