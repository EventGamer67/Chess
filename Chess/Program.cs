using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Chess.Models.Core;
using Chess.Models.Figures;

namespace Chess
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.startGame();
        }
    }
}