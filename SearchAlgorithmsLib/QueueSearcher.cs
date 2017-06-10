using System;
using System.Collections.Generic;
using System.Text;
using Priority_Queue;

namespace SearchAlgorithmsLib
{
	public abstract class QueueSearcher<T> : Searcher<T>
	{
		protected SimplePriorityQueue<State<T>> openList;

		public QueueSearcher()
		{
			this.evaluatedNodes = 0;
			openList = new SimplePriorityQueue<State<T>>();
		}

		protected State<T> PopOpenList()
		{
			evaluatedNodes++;
			return openList.Dequeue();
		}

		public int OpenListSize
		{
			get { return openList.Count; }
		}

		public void AddToOpenList(State<T> state)
		{
			if (state.GetCameFrom() != null)
			{
				openList.Enqueue(state, state.GetCameFrom().GetCost() + 1);
			}
			else
			{
				openList.Enqueue(state, 0);
			}
		}

	}
}