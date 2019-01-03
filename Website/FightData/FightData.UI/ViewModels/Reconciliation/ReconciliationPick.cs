namespace FightData.UI.ViewModels.Reconciliation
{
    public class ReconciliationPick
    {
        public ReconciliationPick() { }

        public string AnalystName { get; set; }
        public string PickText { get; set; }
        public int CorrectFighterId { get; set; }
        public int CorrectAnalystId { get; set; }
    }
}
