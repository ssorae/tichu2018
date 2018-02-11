using System;
using EXBoardGame.ActionChainModel;

namespace tichu2018
{
	public class TichuViewer : GameViewer
	{
		private IGameUI _gameUI = null;

		public override void ApplyGameEvent(IGameEvent @event)
		{
			if (@event == null)
				return;
			else if (@event is EvtPlayerJoined)
				_applyGameEvent((EvtPlayerJoined)@event);
			else if (@event is EvtPlayerReady)
				_applyGameEvent((EvtPlayerReady)@event);
		}

		private void _applyGameEvent(EvtPlayerJoined @event)
		{
			_gameUI.NameTagUIs[@event.playerIndex].nameText = @event.nickName;
		}

		private void _applyGameEvent(EvtPlayerReady @event)
		{
			_gameUI.NameTagUIs[@event.playerIndex].SetReady(@event.isReady);
		}

		public TichuViewer(IGameUI gameUI)
		{
			_gameUI = gameUI;

		}
		private TichuViewer() { }
	}
}
