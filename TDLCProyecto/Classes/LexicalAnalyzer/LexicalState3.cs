namespace TDLCProyecto.Classes.LexicalAnalyzer
{
    public class LexicalState3 : LexicalBaseState
    {
        #region constructors
        public LexicalState3(LexicalAnalyzer lexicalAnalyzer) : base(lexicalAnalyzer)
        {

        }
        #endregion

        #region public methods
        public override void getNextState(char symbol)
        {
            LexicalBaseState nextState = symbol switch
            {
                (>= '0') and (<= '9') => new LexicalState3(_lexicalAnalyzer),
                '.' => new LexicalState4(_lexicalAnalyzer),
                'E' => new LexicalState6(_lexicalAnalyzer),
                '+' => new LexicalState9(_lexicalAnalyzer),
                '-' => new LexicalState9(_lexicalAnalyzer),
                '*' => new LexicalState9(_lexicalAnalyzer),
                '/' => new LexicalState9(_lexicalAnalyzer),
                _ => new LexicalStateInvalid(_lexicalAnalyzer)
            };

            _lexicalAnalyzer.changeState(nextState);
        }
        #endregion
    }
}
