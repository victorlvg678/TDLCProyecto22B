namespace TDLCProyecto.Classes.LexicalAnalyzer
{
    public class LexicalState5 : LexicalBaseState
    {
        #region constructors
        public LexicalState5(LexicalAnalyzer lexicalAnalyzer) : base(lexicalAnalyzer)
        {

        }
        #endregion

        #region public methods
        public override void getNextState(char symbol)
        {
            LexicalBaseState nextState = symbol switch
            {
                (>= '0') and (<= '9') => new LexicalState5(_lexicalAnalyzer),
                '+' => new LexicalState9(_lexicalAnalyzer),
                '-' => new LexicalState9(_lexicalAnalyzer),
                '*' => new LexicalState9(_lexicalAnalyzer),
                '/' => new LexicalState9(_lexicalAnalyzer),
                'E' => new LexicalState6(_lexicalAnalyzer),
                _ => new LexicalStateInvalid(_lexicalAnalyzer)
            };

            _lexicalAnalyzer.changeState(nextState);
        }
        #endregion
    }
}
