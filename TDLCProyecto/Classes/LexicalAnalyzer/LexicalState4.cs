namespace TDLCProyecto.Classes.LexicalAnalyzer
{
    public class LexicalState4 : LexicalBaseState
    {
        #region constructors
        public LexicalState4(LexicalAnalyzer lexicalAnalyzer) : base(lexicalAnalyzer)
        {

        }

        #endregion

        #region public methods
        public override void getNextState(char symbol)
        {
            LexicalBaseState nextState = symbol switch
            {
                (>= '0') and (<= '9') => new LexicalState5(_lexicalAnalyzer),
                _ => new LexicalStateInvalid(_lexicalAnalyzer)
            };

            _lexicalAnalyzer.changeState(nextState);
        }

        #endregion
    }
}
