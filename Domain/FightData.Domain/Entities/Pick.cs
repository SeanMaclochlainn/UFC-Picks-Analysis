
namespace FightData.Domain.Entities
{
    public class Pick
    {
        //TD rename pick to Analyst pick
        public int Id { get; set; }
        public Analyst Analyst { get; set; }
        public Fight Fight { get; set; }
        public Fighter FighterPick { get; set; }
    }
}
