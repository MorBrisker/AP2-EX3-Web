using System;
using System.Collections.Generic;
using System.Text;
using Priority_Queue;

namespace SearchAlgorithmsLib
{
	public abstract class Searcher<T> : ISearcher<T>
	{
		protected int evaluatedNodes;

		public Searcher()
		{
			//this.evaluatedNodes = 0;
		}

		public int GetNumOfNodesEvaluated()
		{
			return evaluatedNodes;
		}

		public Solution<T> BackTrace(State<T> n)
		{
			Solution<T> backTrace = new Solution<T>();
			while (n != null)
			{
				backTrace.AddNode(n);
				n = n.GetCameFrom();
			}
			return backTrace;
		}

		public abstract Solution<T> Search(ISearchable<T> searchable);
	}
}