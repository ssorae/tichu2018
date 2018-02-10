using System;
using System.Collections;

namespace EXBoardGame.ActionChainModel
{
	// TODO(sorae): 추후 Update()가 필요해지면 추가
	public abstract class GameCore : IDisposable
	{
		protected ActionChain _chain { get; private set; }
		protected abstract void _applyActionInfo(ActionInfo givenInfo);

		public bool isInitialized { get; private set; }
		public void Initialize(ActionChain chainToUse)
		{
			_chain = chainToUse;
			_chain.OnNewActionAppended += this._applyActionInfo;
			_initialize();
			this.isInitialized = true;
		}
		protected abstract void _initialize();

		public virtual void Dispose()
		{
			_chain.OnNewActionAppended -= this._applyActionInfo;
		}

		public event Action<GameEvent> onGameEvent = delegate{};

		protected void _publishGameEvent(GameEvent @event)
			=> onGameEvent.Invoke(@event);
	}
}