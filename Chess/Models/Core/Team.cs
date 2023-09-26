using Chess.Models.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models.Core
{
    internal class Team
    {
        string Color { get; set; }
        ConsoleColor ConsoleColor { get; set; }

        public Board board;

        public Team(Board board, ConsoleColor consoleColor, string color)
        {
            this.board = board;
            this.ConsoleColor = ConsoleColor;
            this.Color = color;
        }

        


        //public bool IsTeamKingCatched()
        //{
        //    List<Figure> teamKings = board.getTeamFigures(this.Color).Where(figure => figure is King).ToList();

        //    foreach(King king in teamKings)
        //    {
        //        if(king.is)
        //    }

        //}
    }
}
