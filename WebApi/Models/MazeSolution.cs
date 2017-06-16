
using System;
using System.Collections.Generic;
using System.Text;
using SearchAlgorithmsLib;
using MazeLib;
using Newtonsoft.Json.Linq;

namespace WebApi.Models
{
    /// <summary>
    /// Class MazeSolution.
    /// </summary>
    public class MazeSolution
	{
        /// <summary>
        /// The sol
        /// </summary>
        private String sol;
        /// <summary>
        /// The path
        /// </summary>
        private Solution<Position> path;
        /// <summary>
        /// The number of nodes evaluated
        /// </summary>
        private int numOfNodesEvaluated;
        /// <summary>
        /// The maze name
        /// </summary>
        private String mazeName;

        /// <summary>
        /// Initializes a new instance of the <see cref="MazeSolution"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="mazeName">Name of the maze.</param>
        /// <param name="numOfNodesEvaluated">The number of nodes evaluated.</param>
        public MazeSolution(Solution<Position> path, string mazeName, int numOfNodesEvaluated)
		{
			this.path = path;
			this.numOfNodesEvaluated = numOfNodesEvaluated;
			this.mazeName = mazeName;
		}
        /// <summary>
        /// Solutions the path.
        /// </summary>
        public void SolutionPath() {
			StringBuilder sb = new StringBuilder();
			Stack<State<Position>> stack = this.path.GetStack();
			Position before = stack.Pop().GetStateType();
			//Position after = this.stack.Pop().GetStateType();

			while (stack.Count != 0) {
				Position after = stack.Pop().GetStateType();
				if (before.Row + 1 == after.Row) {
					sb.Append("3");
				} else if (before.Row - 1 == after.Row) {
					sb.Append("2");
				} else if (before.Col + 1 == after.Col) {
					sb.Append("1");
				} else {
					sb.Append("0");
				}

				before = after;
			}

			this.sol = sb.ToString();
		}

        /// <summary>
        /// Gets the path.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetPath() {
			return this.sol;
		}

        /// <summary>
        /// To the json.
        /// </summary>
        /// <returns>System.String.</returns>
        public string ToJSON()
		{
			JObject mazeSolObj = new JObject();
			mazeSolObj["Name"] = this.mazeName;
			mazeSolObj["Solution"] = this.sol;
			mazeSolObj["NodesEvaluated"] = this.numOfNodesEvaluated;


			return mazeSolObj.ToString();
		}

        /// <summary>
        /// Sols to string.
        /// </summary>
        /// <returns>System.String.</returns>
        public string SolToString()
        {
            return this.sol;
        }
    }
}
