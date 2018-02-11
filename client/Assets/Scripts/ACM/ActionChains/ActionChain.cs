using System;
using System.Collections.Generic;
using UniRx;

namespace EXBoardGame.ActionChainModel
{
	public abstract class ActionChain : IDisposable
	{
#if UNITY_EDITOR
		public static List<ActionChain> AllChains
		{
			get
			{
				_allChains.RemoveAll(chain => chain.isDisposed);
				return _allChains;
			}
		}
		private static List<ActionChain> _allChains = new List<ActionChain>();
#endif

		public ActionChain()
		{
#if UNITY_EDITOR
			AllChains.Add(this);
#endif
		}

		public Random random { get; protected set; } = null;

		public IEnumerable<ActionInfo> AsEnumerable()
		{
			var nextNodeToReturn = _chain.First;
			while (false == this.isDisposed && nextNodeToReturn != null)
			{
				yield return nextNodeToReturn.Value;
				nextNodeToReturn = nextNodeToReturn.Next;
			}
		}

		public event Action<ActionInfo> OnNewActionAppended = delegate { };

		public bool isDisposed => _disposables.IsDisposed;
		protected CompositeDisposable _disposables = new CompositeDisposable();
		private LinkedList<ActionInfo> _chain = new LinkedList<ActionInfo>();

		/// <summary>
		/// Chain에 새 Action 추가를 시도한다.
		/// 추가가 실패하거나, 바로 추가되지 않을 수도 있다.
		/// </summary>
		/// <param name="actionToAppend">추가하길 원하는 action</param>
		public abstract void TryAppendFromClient(GameAction actionToAppend);

		/// <summary>
		/// 실제로 append 성공했을 때(서버에 허락을 받았을 때 등) 호출하여 로컬 체인에 append
		/// </summary>
		protected void _onAppendSucceed(ActionInfo actionInfoToAppend)
		{
			_chain.AddLast(actionInfoToAppend);
			this.OnNewActionAppended.Invoke(actionInfoToAppend);
		}

		public void Dispose() => _disposables.Dispose();

		public int? myClientIndex { get; protected set; }
	}
}