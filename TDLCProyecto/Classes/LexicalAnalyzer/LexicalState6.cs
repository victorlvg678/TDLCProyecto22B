﻿namespace TDLCProyecto.Classes.LexicalAnalyzer
{
    public class LexicalState6 : LexicalBaseState
    {
        #region constructors
        public LexicalState6(LexicalAnalyzer lexicalAnalyzer) : base(lexicalAnalyzer)
        {

        }
        #endregion

        #region public methods
        public override void getNextState(char symbol)
        {
            LexicalBaseState nextState = symbol switch
            {
                (>= '0') and (<= '9') => new LexicalState3(_lexicalAnalyzer),
                _ => new LexicalStateInvalid(_lexicalAnalyzer)
            };

            _lexicalAnalyzer.changeState(nextState);
        }
        #endregion
    }
}
