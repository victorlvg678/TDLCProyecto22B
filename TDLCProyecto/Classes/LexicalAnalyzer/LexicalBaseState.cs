namespace TDLCProyecto.Classes.LexicalAnalyzer
{
    public abstract class LexicalBaseState
    {
        #region protected members
        protected readonly LexicalAnalyzer _lexicalAnalyzer;
        #endregion

        #region constructors
        public LexicalBaseState(LexicalAnalyzer lexicalAnalyzer)
        {
            _lexicalAnalyzer = lexicalAnalyzer;
        }
        #endregion

        #region public methods
        public abstract void getNextState(char symbol);
        #endregion
    }
}
