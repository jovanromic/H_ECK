using H_ECK.BoardElements;

namespace H_ECK.GameUI
{
    public interface IGameDisplay
    {
        void DisplayBoard(Board board);
        void DisplayMessage(string message);
    }
}
