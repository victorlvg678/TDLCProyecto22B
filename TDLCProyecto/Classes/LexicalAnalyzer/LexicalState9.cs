namespace TDLCProyecto.Classes.LexicalAnalyzer
{
    public class LexicalState9 : LexicalBaseState
    {
        #region constructors
        public LexicalState9(LexicalAnalyzer lexicalAnalyzer) : base(lexicalAnalyzer)
        {

        }
        #endregion

        #region public methods
        public override void getNextState(char symbol)
        {
            LexicalBaseState nextState = symbol switch
            {
                (>= '1') and (<= '9') => new LexicalState3(_lexicalAnalyzer),
                _ => new LexicalStateInvalid(_lexicalAnalyzer)
            };

            _lexicalAnalyzer.changeState(nextState);
        }
        #endregion
    }
}
