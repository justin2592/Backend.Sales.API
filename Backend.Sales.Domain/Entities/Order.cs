using System;
using System.Collections.Generic;

namespace Backend.Sales.Domain.Entities;

public partial class Order
{
    public int OrderId { get; set; }

    public DateOnly Date { get; set; }

    public TimeOnly Time { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime LastUpdatedDate { get; set; }

    public string LastUpdatedBy { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
