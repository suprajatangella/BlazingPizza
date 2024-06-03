using BlazingPizza.Models;

namespace BlazingPizza.Data;

public class Pizza
{
    public const int DefaultSize = 12;
    public const int MinimumSize = 9;
    public const int MaximumSize = 17;
    public int PizzaId { get; set; }
    public string PizzaName { get; set; }
    public string PizzaDescription { get; set; }
    public string Price { get; set; }
    public bool Vegetarian { get; set; }
    public bool Vegan { get; set; }
    public int Size { get; set; }
    public List<PizzaTopping> Toppings { get; set; }
    public int SpecialId { get; set; }
    public PizzaSpecial Special { get; set; }

    public decimal GetBasePrice()
    {
        return ((decimal)Size / (decimal)DefaultSize) * Special.BasePrice;
    }

    public decimal GetTotalPrice()
    {
        return GetBasePrice();
    }

    public string GetFormattedTotalPrice()
    {
        return GetTotalPrice().ToString("0.00");
    }

}
