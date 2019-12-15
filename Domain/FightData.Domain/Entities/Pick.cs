
using FightData.Domain.Builders;

namespace FightData.Domain.Entities
{
    public class Pick : Entity
    {
        public Pick(FightPicksContext context) : base(context) { }

        public Pick(PickBuilder pickBuilder) : this(pickBuilder.Context)
        {
            Analyst = pickBuilder.Analyst;
            Fight = pickBuilder.Fight;
            Fighter = pickBuilder.Fighter;
        }

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
