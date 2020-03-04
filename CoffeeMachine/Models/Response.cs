using System.Text.Json.Serialization;

namespace CoffeeMachine.Models
{
    public class Response<T>
    {
        [JsonPropertyName("data")]
        public T Data { get; set; }
    }
}