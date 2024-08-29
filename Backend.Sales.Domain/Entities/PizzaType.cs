using System;
using System.Collections.Generic;

namespace Backend.Sales.Domain.Entities;

public partial class PizzaType
{
    public string PizzaTypeId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Category { get; set; } = null!;

    public string Ingredients { get; set; } = null!;

    public virtual ICollection<Pizza> Pizzas { get; set; } = new List<Pizza>();
}
