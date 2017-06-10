using System;
using System.Collections.Generic;
using System.Text;

namespace SearchAlgorithmsLib
{
	public abstract class StackSearcher<T> : Searcher<T>
	{
		protected Stack<State<T>> s;

		public StackSearcher()
		{
			this.evaluatedNodes = 0;
			s = new Stack<State<T>>();
		}

		protected State<T> PopOpenList()
		{
			evaluatedNodes++;
			return s.Pop();
		}

		public int OpenListSize
		{
			get { return s.Count; }
		}

		public void AddToOpenList(State<T> state)
		{
			s.Push(state);
			/*if (state.GetCameFrom() != null)
            {
                s.Enqueue(state, state.GetCameFrom().GetCost() + 1);
            }
            else
            {
                openList.Enqueue(state, 0);
            }*/
		}
	}
}
