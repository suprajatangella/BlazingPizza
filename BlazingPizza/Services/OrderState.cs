using BlazingPizza.Data;
using BlazingPizza.Models;

namespace BlazingPizza.Services;

public class OrderState
{
    public bool ShowingConfigureDialog { get; private set; }
    public Pizza ConfiguringPizza { get; private set; }
    public Order Order { get; private set; } = new Order();

    public void ShowConfigurePizzaDialog(PizzaSpecial special)
    {
        ConfiguringPizza = new Pizza()
        {
            Special = special,
            SpecialId = special.id,
            Size = Pizza.DefaultSize,
            Toppings = new List<PizzaTopping>(),
        };

        ShowingConfigureDialog = true;
    }

    public void CancelConfigurePizzaDialog()
    {
        ConfiguringPizza = null;

        ShowingConfigureDialog = false;
    }

    public void ConfirmConfigurePizzaDialog()
    {
        ConfiguringPizza.PizzaId = 0;
        ConfiguringPizza.PizzaName = "Mashroom Lovers";
        ConfiguringPizza.PizzaDescription = "Description";
        ConfiguringPizza.Size = 12;
        ConfiguringPizza.Vegetarian = true;
        ConfiguringPizza.Vegan = true;
        ConfiguringPizza.Price = "12";
        Order.DeliveryLocation = new LatLong();
            
        Order.UserId = "1";
        Order.Pizzas.Add(ConfiguringPizza);
        ConfiguringPizza = null;

        ShowingConfigureDialog = false;
    }
    public void ResetOrder()
    { 
        Order.Pizzas.Clear();
        Order = new Order();
    }

}
