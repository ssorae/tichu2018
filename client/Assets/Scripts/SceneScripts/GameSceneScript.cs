using System.Collections;
using System.Collections.Generic;
using EXBoardGame.ActionChainModel;
using UnityEngine;

namespace tichu2018
{
	public sealed class GameSceneScript : MonoBehaviour
	{
		public enum GameType
		{
			kLocal = 0,
			kP2P = 1,
			kClientServer = 2,
		}

		[SerializeField]
		private GameType _gameType;

		private GameCore _gameCore { get; set; }
		private ActionChain _actionChain { get; set; }
		private GameView _gameView { get; set; }
		private GameConroller _gameController { get; set; }

		public void Awake()
		{
			// TODO(sorae): GameType에 맞는 Chain 생성. 일단 무조건 local로..
			_actionChain = new LocalActionChain();
			_gameCore = new TichuCore();
			_gameView = new TichuView();
			_gameController = new TichuController();

			_gameCore.Initialize(_actionChain);
			_gameCore.onGameEvent += _gameView.ApplyGameEvent;
			_gameController.onPlayerAction += _actionChain.TryAppend;
		}
	}
}