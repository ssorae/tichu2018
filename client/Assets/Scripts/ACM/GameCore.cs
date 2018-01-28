using System;
using System.Collections;

namespace EXBoardGame.ActionChainModel
{
	public abstract class GameCore
	{
		protected ActionChain _chain { get; private set; }
		protected abstract void _applyActionInfo(ActionInfo givenInfo);

		public bool isInitialized { get; private set; }
		public void Initialize(ActionChain chainToUse)
		{
			this._chain = chainToUse;
			_initialize();
			this.isInitialized = true;
		}
		protected abstract void _initialize();

		public event Action<GameEvent> onGameEvent = delegate{};

		protected void _invokeGameEvent(GameEvent @event)
			=> onGameEvent.Invoke(@event);

		public IEnumerator GameRoutine()
		{
			if(false == this.isInitialized)
			{
				// TODO(sorae): log error
				yield break;
			}
			foreach(var newAction in _chain.AsEnumerable())
			{
				if(newAction == null)
				{
					yield return null;
					continue;
				}

				this._applyActionInfo(newAction);
			}
		}
	}
}