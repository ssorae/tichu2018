using System;
using System.Collections.Generic;
using UniRx;

namespace EXBoardGame.ActionChainModel
{
	public abstract class ActionChain : IDisposable
	{
		public Random random { get; private set; } = null;

		public IEnumerable<ActionInfo> AsEnumerable()
		{
			while (false == this.isDisposed && _chain.First == null)
				yield return null;

			if (this.isDisposed)
				yield break;

			yield return _chain.First.Value;

			var lastReturnedNode = _chain.First;

			while (false == this.isDisposed)
			{
				if (lastReturnedNode.Next == null)
					yield return null;
				else
				{
					yield return lastReturnedNode.Next.Value;
					lastReturnedNode = lastReturnedNode.Next;
				}
			}
			yield break;
		}

		public bool isDisposed => _disposables.IsDisposed;
		protected CompositeDisposable _disposables = new CompositeDisposable();
		protected LinkedList<ActionInfo> _chain = new LinkedList<ActionInfo>();

		public abstract void TryAppend(GameAction actionToAppend);

		public void Dispose() => _disposables.Dispose();
	}
}