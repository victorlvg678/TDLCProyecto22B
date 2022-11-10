namespace TDLCProyecto.Classes.LexicalAnalyzer
{
    public class LexicalStateInvalid : LexicalBaseState
    {
        #region constructors
        public LexicalStateInvalid(LexicalAnalyzer lexicalAnalyzer) : base(lexicalAnalyzer)
        {

        }

        #endregion

        #region public methods
        public override void getNextState(char symbol)
        {
            // Cannot move to another state because it is already invalid
        } 
        #endregion

    }
}
