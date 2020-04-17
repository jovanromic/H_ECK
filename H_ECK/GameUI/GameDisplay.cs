using H_ECK.GameElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H_ECK.GameUI
{
    public interface IGameDisplay
    {
        void DisplayBoard(Game game);
        void DisplayMessage(string message);
    }
}
