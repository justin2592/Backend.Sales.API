using System;
using System.Collections.Generic;

namespace Backend.Sales.Domain.Entities;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public string PizzaId { get; set; } = null!;

    public int Quantity { get; set; }

    public int OrderId { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime LastUpdatedDate { get; set; }

    public string LastUpdatedBy { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;

    public virtual Pizza Pizza { get; set; } = null!;
}
