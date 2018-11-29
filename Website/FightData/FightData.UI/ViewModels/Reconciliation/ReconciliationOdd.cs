namespace FightData.UI.ViewModels.Reconciliation
{
    public class ReconciliationOdd
    {
        public ReconciliationOdd() { }

        public string FighterName { get; set; }
        public string FighterOdds { get; set; }
        public int CorrectFighterId { get; set; }
        public bool Cancelled { get; set; }
    }
}
