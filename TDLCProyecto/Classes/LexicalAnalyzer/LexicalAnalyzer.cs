namespace TDLCProyecto.Classes.LexicalAnalyzer
{
    public class LexicalAnalyzer
    {
        #region private members
        private LexicalBaseState _state;
        private readonly string _input;
        private int _currentChar;
        #endregion

        #region constructors
        public LexicalAnalyzer(string input)
        {
            _state = new LexicalState0(this);
            _input = input;
            _currentChar = 0;
        }

        public LexicalAnalyzer(string input, int state, int charPosition)
        {
            _input = input;
            _currentChar = charPosition;
            _state = state switch
            {
                0 => new LexicalState0(this),
                1 => new LexicalState1(this),
                2 => new LexicalState2(this),
                3 => new LexicalState3(this),
                4 => new LexicalState4(this),
                5 => new LexicalState5(this),
                6 => new LexicalState6(this),
                7 => new LexicalState7(this),
                8 => new LexicalState8(this),
                9 => new LexicalState9(this),
                10 => new LexicalStateInvalid(this)
            };
        }
        #endregion

        #region public methods
        public void changeState(LexicalBaseState state) => _state = state;

        public LexicalBaseState getCurrentState() => _state;

        #endregion
    }
}
