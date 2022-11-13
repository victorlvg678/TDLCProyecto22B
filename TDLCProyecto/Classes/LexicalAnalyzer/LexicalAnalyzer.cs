namespace TDLCProyecto.Classes.LexicalAnalyzer
{
    public class LexicalAnalyzer
    {
        #region private members
        private LexicalBaseState _state;
        private string _input;
        private int _currentChar;
        #endregion

        #region constructors
        public LexicalAnalyzer()
        {
            _state = new LexicalState0(this);
            _input = string.Empty;
            _currentChar = 0;
        }

        public LexicalAnalyzer(string input)
        {
            _state = new LexicalState0(this);
            _input = input;
            _currentChar = 0;
        }

        public LexicalAnalyzer(string input, int state, int currentChar)
        {
            _input = input;
            _currentChar = currentChar;
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
                _ => new LexicalStateInvalid(this)
            };
        }
        #endregion

        #region public methods
        public void getNextState()
        {
            if (string.IsNullOrWhiteSpace(_input))
                return;

            if (_currentChar < 0 || _currentChar > (_input.Length - 1))
                return;

            _state.getNextState(_input[_currentChar]);
        }

        public void changeState(LexicalBaseState state)
        {
            _state = state;
            _currentChar++;
        }

        public LexicalBaseState getCurrentState() => _state;

        public int currentChar
        {
            set => _currentChar = (value >= 0) ? value : 0;

            get => _currentChar;
        }

        public int state
        {
            set
            {
                _state = value switch
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
                    _ => new LexicalStateInvalid(this)
                };
            }

            get
            {
                return _state switch
                {
                    LexicalState0 => 0,
                    LexicalState1 => 1,
                    LexicalState2 => 2,
                    LexicalState3 => 3,
                    LexicalState4 => 4,
                    LexicalState5 => 5,
                    LexicalState6 => 6,
                    LexicalState7 => 7,
                    LexicalState8 => 8,
                    LexicalState9 => 9,
                    _ => 10
                };
            }
        }

        public string input
        { 
            set => _input = !string.IsNullOrWhiteSpace(value) ? value : string.Empty;

            get => _input;
        }
        #endregion
    }
}
