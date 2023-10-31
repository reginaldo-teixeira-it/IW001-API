namespace CCA.API.DTO
{
    public class CurrentAccountStatementDTO
    {
        public string Id { get; set; }
        public string? Description { get; set; }
        public string StartDate { get; set; }
        public string Value { get; set; }
        public string Loose { get; set; }
        public string State { get; set; }
    }
}
