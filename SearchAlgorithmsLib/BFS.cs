using System;
using System.Collections.Generic;
using System.Text;

namespace SearchAlgorithmsLib
{
	public class BFS<T> : QueueSearcher<T>
	{
		public override Solution<T> Search(ISearchable<T> searchable)
		{
			State<T> n = null;
			AddToOpenList(searchable.GetInitialState());
			HashSet<State<T>> closed = new HashSet<State<T>>();
			while (OpenListSize > 0)
			{
				n = PopOpenList();
				closed.Add(n);
				if (n.Equals(searchable.GetGoalState()))
				{
					return BackTrace(n);
				}
				List<State<T>> succesors = searchable.GetAllPossibleStates(n);
				foreach (State<T> s in succesors)
				{
					if (!closed.Contains(s) && !openList.Contains(s))
					{
						//check if we need this line!!!
						s.SetCost(n.GetCost() + 1);
						s.SetCameFrom(n);
						AddToOpenList(s);
					}
					else if (openList.Contains(s))
					{

						foreach (State<T> state in openList)
						{
							if (state.Equals(s))
							{
								float cost = n.GetCost() + 1;
								if (s.GetCost() > cost)
								{
									s.SetCost(cost);
									s.SetCameFrom(n);
									openList.UpdatePriority(s, cost);
								}
							}
						}
					}

					//s is in closed 
					///vcx
					/*else
					{
                        if ()
						AddToOpenList(s);
					}*/
				}
			}
			//change next line
			return BackTrace(n);
		}
	}
}