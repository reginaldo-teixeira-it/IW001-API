using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace CCA.API.Model
{
    public class CurrentAccountStatement
    {
        [Key]
        public long Id { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public float Value { get; set; }

        public string? Loose { get; set; }
        public bool State { get; set; }

    }
}
