using System.Text.Json.Serialization;

namespace CoffeeMachine.Models
{
    public class Menu
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("image")]
        public string Image { get; set; }
        [JsonPropertyName("coffeeNeeded")]
        public int CoffeeNeeded { get; set; }
        [JsonPropertyName("waterNeeded")]
        public int WaterNeeded { get; set; }
        [JsonPropertyName("milkNeeded")]
        public int MilkNeeded { get; set; }
        [JsonPropertyName("price")]
        public int Price { get; set; }
    }
}