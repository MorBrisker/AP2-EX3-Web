using System;
using System.Collections.Generic;
using System.Text;

namespace SearchAlgorithmsLib
{
	public interface ISearcher<T>
	{
		Solution<T> Search(ISearchable<T> searchable);
		int GetNumOfNodesEvaluated();
	}
}
