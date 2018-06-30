
namespace FightData.Domain.Entities
{
    public class FighterAltName
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Fighter Fighter { get; set; }
    }
}
