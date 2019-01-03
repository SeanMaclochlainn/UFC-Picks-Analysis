namespace FightData.Domain.Entities
{
    public class Odd : Entity
    {
        public Odd(FightPicksContext context) : base(context) { }

        public int Id { get; set; }
        public decimal Value { get; set; }
        public Fighter Fighter { get; set; }
        public Fight Fight { get; set; }
    }
}
