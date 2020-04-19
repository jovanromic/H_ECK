using H_ECK.GameElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H_ECK.BoardElements;

namespace H_ECK.GameUI
{
    public interface IGameDisplay
    {
        void DisplayBoard(Board board);
        void DisplayMessage(string message);
    }
}
