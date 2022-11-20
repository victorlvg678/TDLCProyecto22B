namespace TDLCProyecto.Classes.LexicalAnalyzer
{
    public class LexicalState0 : LexicalBaseState
    {
        #region constructors
        public LexicalState0(LexicalAnalyzer lexicalAnalyzer) : base(lexicalAnalyzer)
        {
        
        }
        #endregion

        #region public methods
        public override void getNextState(char symbol)
        {
            LexicalBaseState nextState = symbol switch
            {
                (>= 'a') and (<= 'z') => new LexicalState1(_lexicalAnalyzer),
                (>= 'A') and (<= 'Z') => new LexicalState1(_lexicalAnalyzer),
                '_' => new LexicalState1(_lexicalAnalyzer),
                '+' => new LexicalState2(_lexicalAnalyzer),
                '-' => new LexicalState2(_lexicalAnalyzer),
                (>= '1') and (<= '9') => new LexicalState3(_lexicalAnalyzer),
                _ => new LexicalStateInvalid(_lexicalAnalyzer)
            };

            _lexicalAnalyzer.changeState(nextState);
        }
        #endregion
    }
}
