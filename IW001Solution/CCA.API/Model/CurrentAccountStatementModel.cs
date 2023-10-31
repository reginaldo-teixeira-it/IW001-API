using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace CCA.API.Model
{
    public class CurrentAccountStatementModel
    {
        [Key]
        [JsonProperty( "id" )]
        public long Id { get; set; }
        [JsonProperty( "description" )]
        public string? Description { get; set; }
        [JsonProperty( "startDate" )]
        public DateTime StartDate { get; set; }
        [JsonProperty( "value" )]
        public float Value { get; set; }
        [JsonProperty( "loose" )]
        public bool Loose { get; set; }
        [JsonProperty( "state" )]
        public bool State { get; set; }

    }
}
