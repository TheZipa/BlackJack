using BlackJack.Code.Data.Enums;

namespace BlackJack.Code.Core.Players
{
    public struct HandResult
    {
        public GameResultType Result { get; set; }
        public int WinSum { get; set; }

        public void DefineResult(GameResultType result, int winSum = 0)
        {
            Result = result;
            WinSum = winSum;
        }

        public bool IsDefined() => Result != GameResultType.undefine;
    }
}