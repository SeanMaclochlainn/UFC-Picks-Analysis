
namespace FightDataProcessor
{
    public class InputParser
    {
        public string InputText { get; protected set; }

        public virtual string InvalidInputMessage
        {
            get
            {
                return "Invalid input. Please try again:";
            }
        }

        public virtual void ParseInput(string inputText)
        {
            InputText = inputText;
        }

        public virtual bool IsValid()
        {
            if (string.IsNullOrEmpty(InputText))
                return false;
            else
                return true;
        }

    }
}
