using System;
using System.Collections.Generic;

namespace IntexQueensSlay.Models;

public partial class LineItem
{
    public int TransactionId { get; set; }

    public byte ProductId { get; set; }

    public byte Quantity { get; set; }

    public byte? Rating { get; set; }
}
