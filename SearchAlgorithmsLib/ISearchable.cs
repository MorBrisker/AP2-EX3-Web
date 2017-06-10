using System;
using System.Collections.Generic;
using System.Text;

namespace SearchAlgorithmsLib
{
	public interface ISearchable<T>
	{
		State<T> GetInitialState();
		State<T> GetGoalState();
		List<State<T>> GetAllPossibleStates(State<T> s);
	}
}
