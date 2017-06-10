using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
	public class DFS<T> : StackSearcher<T>
	{
		public override Solution<T> Search(ISearchable<T> searchable)
		{
			//Stack<State<T>> s = new Stack<State<T>>();
			State<T> n = searchable.GetInitialState();
			AddToOpenList(n);
			//s.Push(searchable.GetInitialState());
			while (OpenListSize != 0)
			{
				n = PopOpenList();
				//n = s.Pop();

				if (n.Equals(searchable.GetGoalState()))
				{
					return BackTrace(n);
				}

				n.IsVisited = true;
				List<State<T>> succesors = searchable.GetAllPossibleStates(n);
				foreach (State<T> t in succesors)
				{
					if (t.IsVisited == false)
					{
						t.SetCameFrom(n);
						AddToOpenList(t);
					}
				}

			}
			//change next line
			return null;
		}
	}
}