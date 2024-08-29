using System;
using System.Collections.Generic;

namespace Backend.Sales.Domain.Entities;

public partial class Pizza
{
    public string PizzaId { get; set; } = null!;

    public string PizzaTypeId { get; set; } = null!;

    public string Size { get; set; } = null!;

    public decimal Price { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual PizzaType PizzaType { get; set; } = null!;
}
