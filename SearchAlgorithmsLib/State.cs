using System;
using System.Collections.Generic;
using System.Text;

namespace SearchAlgorithmsLib
{
	public class State<T>
	{

		public static class StatePool
		{
			private static HashSet<State<T>> pool = new HashSet<State<T>>();
			public static int GetPoolSize()
			{
				return pool.Count;
			}
			public static State<T> GetObject(T s)
			{
				State<T> m;
				foreach (State<T> t in pool)
				{

					if (t.GetStateType().Equals(s))
					{
						return t;
					}
				}
				m = new State<T>(s);
				pool.Add(m);
				return m;
			}

            public static void ClearPool()
            {
                pool.Clear();
            }
		}

		private T state;
		private float cost;
		private State<T> cameFrom;
		private bool isVisited;
		private string direction;

		public bool IsVisited
		{
			get
			{
				return isVisited;
			}
			set
			{
				isVisited = value;
			}
		}

		private State(T state)
		{
			this.state = state;
			this.cost = float.PositiveInfinity;
			this.cameFrom = null;
			this.IsVisited = false;
			this.direction = "-1";
		}

		public void SetDirection(string dir) {
			this.direction = dir;
		}

		public bool Equals(State<T> s)
		{
			return state.Equals(s.state);
		}

		public float GetCost()
		{
			return this.cost;
		}
		public State<T> GetCameFrom()
		{
			return this.cameFrom;
		}
		public T GetStateType()
		{
			return this.state;
		}
		public void SetCameFrom(State<T> s)
		{
			this.cameFrom = s;
		}
		public void SetCost(float c)
		{
			this.cost = c;
		}
		public override int GetHashCode()
		{
			return state.GetHashCode();
		}
	}
}