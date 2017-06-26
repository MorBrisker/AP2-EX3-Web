using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using WebApi.Models;
using Newtonsoft.Json.Linq;
using MazeLib;

namespace WebApi.Controllers
{
    public class MultiMazeHub : Hub
    {
        public static MultiMazeModel m = new MultiMazeModel();
        public static Dictionary<string, string> users = new Dictionary<string, string>();

        public void StartGame(string name, int rows, int cols, string client)
        {
            users[client] = Context.ConnectionId;
            Maze maze = m.StartGame(name, rows, cols, client);
            JObject obj = JObject.Parse(maze.ToJSON());
            Clients.Client(users[client]).drawMaze(obj);

        }

        public void JoinGame(string name, string client)
        {
            users[client] = Context.ConnectionId;
            Maze maze = m.JoinGame(name, client);
            JObject obj = JObject.Parse(maze.ToJSON());
            Clients.Client(users[client]).drawMaze(obj);

        }
        public void GetList()
        {
            string id = Context.ConnectionId;
            JArray list = m.ListGames();
            Clients.Client(id).getListOfGames(list);
        }

        public void PlayMove(string dir, string client)
        {
            string opp = m.GetGameOpp(client);
            string id = users[opp];
            Clients.Client(id).moveOpp(dir);
        }
    }
}