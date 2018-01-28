using System;

namespace EXBoardGame.ActionChainModel
{
	public abstract class GameCore
	{
		protected ActionChain _chain { get; private set; }
		protected abstract void _consumeActionInfo(ActionInfo givenInfo);

		public bool isInitialized { get; private set; }
		public void Initialize(ActionChain chainToUse)
		{
			this._chain = chainToUse;
			_initialize();
			this.isInitialized = true;
		}
		protected abstract void _initialize();

		public event Action<GameEvent> onGameEvent = delegate{};
	}
}