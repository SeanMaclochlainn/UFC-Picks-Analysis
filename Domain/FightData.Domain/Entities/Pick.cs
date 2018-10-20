
namespace FightData.Domain.Entities
{
    public class Pick : Entity
    {
        public Pick(FightPicksContext context) : base(context) { }

        public int Id { get; set; }
        public Analyst Analyst { get; set; }
        public Fight Fight { get; set; }
        public Fighter Fighter { get; set; }

        public bool IsCorrect()
        {
            if (Fighter.Id == Fight.Winner.Id)
                return true;
            else
                return false;
        }
    }
}
