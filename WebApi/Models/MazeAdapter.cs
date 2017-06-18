
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
using MazeLib;

namespace WebApi.Models
{
    /// <summary>
    /// Class MazeAdapter.
    /// </summary>
    /// <seealso cref="SearchAlgorithmsLib.ISearchable{MazeLib.Position}" />
    public class MazeAdapter : ISearchable<Position>
	{
        /// <summary>
        /// The maze
        /// </summary>
        private Maze maze;

        /// <summary>
        /// Initializes a new instance of the <see cref="MazeAdapter"/> class.
        /// </summary>
        /// <param name="maze">The maze.</param>
        public MazeAdapter(Maze maze)
		{
			this.maze = maze;
		}
        /// <summary>
        /// Gets the initial state.
        /// </summary>
        /// <returns>State&lt;Position&gt;.</returns>
        public State<Position> GetInitialState()
		{
			State<Position> s = State<Position>.StatePool.GetObject(maze.InitialPos);
			return s;
		}
        /// <summary>
        /// Gets the state of the goal.
        /// </summary>
        /// <returns>State&lt;Position&gt;.</returns>
        public State<Position> GetGoalState()
		{
			State<Position> s = State<Position>.StatePool.GetObject(maze.GoalPos);
			return s;
		}
        /// <summary>
        /// Gets all possible states.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>List&lt;State&lt;Position&gt;&gt;.</returns>
        public List<State<Position>> GetAllPossibleStates(State<Position> s)
		{
			List<State<Position>> list = new List<State<Position>>();
			Position p = s.GetStateType();
			if (p.Row + 1 < maze.Rows)
			{
				if (maze[p.Row + 1, p.Col] == CellType.Free)
				{
					list.Add(State<Position>.StatePool.GetObject((new Position(p.Row + 1, p.Col))));
				}
			}
			if (p.Col + 1 < maze.Cols)
			{
				if (maze[p.Row, p.Col + 1] == CellType.Free)
				{
					list.Add(State<Position>.StatePool.GetObject((new Position(p.Row, p.Col + 1))));
				}
			}
			if (p.Row != 0)
			{
				if (maze[p.Row - 1, p.Col] == CellType.Free)
				{
					list.Add(State<Position>.StatePool.GetObject((new Position(p.Row - 1, p.Col))));
				}
			}
			if (p.Col != 0)
			{
				if (maze[p.Row, p.Col - 1] == CellType.Free)
				{
					list.Add(State<Position>.StatePool.GetObject((new Position(p.Row, p.Col - 1))));
				}
			}
			return list;
		}

        /// <summary>
        /// Gets the maze.
        /// </summary>
        /// <returns>Maze.</returns>
        public Maze GetMaze()
		{
			return this.maze;
		}
	}

}
