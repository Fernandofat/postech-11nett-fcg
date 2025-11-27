namespace FCG.API.Models.Entities
{
    public class Games
    {
        public int Id { get; set; }
        public DateOnly InsertDate { get; set; }
        public DateOnly UpdateDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CurrencyId { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }
        public DateOnly ReleaseDate { get; set; }
    }
}
