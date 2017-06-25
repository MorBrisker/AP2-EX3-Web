using MazeGeneratorLib;
using MazeLib;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace WebApi.Models
{
    public class MultiMazeModel
    {
        private static List<Game> games = new List<Game>();

        public Maze StartGame(string name, int rows, int cols, string client)
        {
            IMazeGenerator mg = new DFSMazeGenerator();
            Maze m = mg.Generate(rows, cols);
            m.Name = name;
            Game g = new Game(name, m);
            g.SetHome(client);
            games.Add(g);
            while (g.IsAvailable)
            {
                Thread.Sleep(100);
            }
            return m;
        }

        public Maze JoinGame(string name, string client)
        {
            Game game = null;
            foreach (Game g in games)
            {
                if (g.Name == name)
                {
                    game = g;
                }
            }
            game.IsAvailable = false;
            game.SetAway(client);
            return game.Maze;
        }

        public JArray ListGames()
        {
            JArray gameList = new JArray();
            foreach (Game g in games)
            {
                if (g.IsAvailable)
                {
                    gameList.Add(g.Name);
                }
            }

            return gameList;
        }

        public string GetGameOpp(string client)
        {
            foreach (Game g in games)
            {
                if (g.GetHome() == client)
                {
                    return g.GetAway();
                } else if (g.GetAway() == client)
                {
                    return g.GetHome();
                }
            }
            return null;
        }
    }
}