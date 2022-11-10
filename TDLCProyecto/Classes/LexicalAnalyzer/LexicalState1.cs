namespace TDLCProyecto.Classes.LexicalAnalyzer
{
    public class LexicalState1 : LexicalBaseState
    {
        #region constructors
        public LexicalState1(LexicalAnalyzer lexicalAnalyzer) : base(lexicalAnalyzer)
        {

        }
        #endregion

        #region public methods
        public override void getNextState(char symbol)
        {
            LexicalBaseState nextState = symbol switch
            {
                (>= 'A') and (<= 'Z') => new LexicalState1(_lexicalAnalyzer),
                (>= 'a') and (<= 'z') => new LexicalState1(_lexicalAnalyzer),
                (>= '0') and (<= '9') => new LexicalState1(_lexicalAnalyzer),
                '_' => new LexicalState1(_lexicalAnalyzer),
                _ => new LexicalStateInvalid(_lexicalAnalyzer)
            };

            _lexicalAnalyzer.changeState(nextState);
        }
        #endregion
    }
}
