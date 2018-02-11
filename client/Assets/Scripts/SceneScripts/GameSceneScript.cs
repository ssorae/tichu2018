using EXBoardGame.ActionChainModel;
using EXBoardGame.Utilities;
using System;
using System.Collections;
using UniRx;
using UnityEngine;

namespace tichu2018
{
	public sealed class GameSceneScript : MonoBehaviour
	{
		public const string GAME_SCENE_NAME = "2.GameScene";
		public enum GameType
		{
			kLocal = 0,
			kP2P = 1,
			kClientServer = 2,
		}

		[SerializeField]
		private GameType _gameType;

		[SerializeField]
		private GameUI _gameUI = null;

		private GameCore _gameCore { get; set; }
		private ActionChain _actionChain { get; set; }
		private GameViewer _gameView { get; set; }
		private TichuController _gameController { get; set; }
		

		private CompositeDisposable _disposables = new CompositeDisposable();

		public void Awake()
		{
			Debug.Assert(_gameUI != null);

			// TODO(sorae): GameType에 맞는 Chain 생성. 일단 무조건 local로..
			_actionChain = new LocalActionChain();
			_disposables.Add(_actionChain);
			_gameCore = new TichuCore();
			_disposables.Add(_gameCore);
			_gameView = new TichuViewer(_gameUI);
			_gameController = new TichuController();

			_gameCore.Initialize(_actionChain);
			_gameCore.onGameEvent += _gameView.ApplyGameEvent;
			_gameController.onPlayerAction += _actionChain.TryAppendFromClient;

			var waitForChainReadyRoutine = StartCoroutine(
				_waitForChainReady(() =>
				{
					Debug.Log($"Chain is ready! Client index :" +
						_actionChain.myClientIndex);
					_gameController.Join(
						DataContainer.instance.myNickname,
						_actionChain.myClientIndex.Value);
				}));
			// TODO(sorae): add routine to disposables
		}

		private IEnumerator _waitForChainReady(Action onConnected)
		{
			while (_actionChain.myClientIndex == null)
				yield return null;
			onConnected?.Invoke();
		}


		public void OnDestroy()
		{
			_disposables.Dispose();
		}
	}
}