using System;
using System.Collections.Generic;

namespace IntexQueensSlay.Models;

public partial class Order
{
    public int TransactionId { get; set; }

    public int CustomerId { get; set; }

    public DateOnly Date { get; set; }

    public string WeekDay { get; set; } = null!;

    public byte Time { get; set; }

    public string EntryMode { get; set; } = null!;

    public double? Subtotal { get; set; }

    public string TransactionType { get; set; } = null!;

    public string TransCountry { get; set; } = null!;

    public string? ShippingAddress { get; set; }

    public string Bank { get; set; } = null!;

    public string CardType { get; set; } = null!;

    public int Fraud { get; set; }
}
