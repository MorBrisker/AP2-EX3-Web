using System;
using System.Collections.Generic;
using System.Text;

namespace SearchAlgorithmsLib
{
	public class Solution<T>
	{
		protected Stack<State<T>> stack;

		public Solution()
		{
			stack = new Stack<State<T>>();
		}

		public Stack<State<T>> GetStack() {
			return this.stack;
		}
		public void AddNode(State<T> state)
		{
			stack.Push(state);
		}
	}
}
