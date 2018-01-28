using System;
using EXBoardGame.ActionChainModel;
using UnityEngine;
using System.Collections.Generic;

namespace tichu2018
{
	public enum TichuPhase
	{
		kWaitForStart = 0,
		kFirstHandout = 1,
		kSecondHandout = 2,

	}

	public class TichuCore : GameCore
	{
		protected override void _initialize()
		{
			// TODO(sorae): impl..
		}

		private int _lastTimeStamp = 0;
		
		protected override void _applyActionInfo(ActionInfo givenInfo)
		{
			if (givenInfo == null)
			{
				Debug.LogWarning("Given info is null");
				return;
			}

			switch(_currentPhase)
			{
				case TichuPhase.kWaitForStart:
					_applyAction_WaitForStart(givenInfo);
					break;
				case TichuPhase.kFirstHandout:
					break;
				case TichuPhase.kSecondHandout:
					break;
				default:
					break;
			}
		}

		private TichuPhase _currentPhase = TichuPhase.kWaitForStart;

		private List<TichuPlayer> _players = new List<TichuPlayer>
		{
			// NOTE(sorae): 일단 플레이어 없음을 null로 해보자.. 문제 없겠지
			null, null, null, null
		};

		private void _applyAction_WaitForStart(ActionInfo givenInfo)
		{
			_lastTimeStamp = givenInfo.timeStamp;

			// NOTE(sorae): WaitForStart에서 유효한 Action들
			//				1. ActPlayerJoined
			//				2. ActPlayerReady
			#region task definitions..
			Action<ActPlayerJoined> handleJoin = joinedAction =>
			{
				// TODO(sorae): impl..
			};

			Action<ActPlayerReady> handleReady = readyAction =>
			{
				var player = _players[readyAction.playerIndex];
				Debug.Assert(player != null);
				player.isReady = readyAction.isReady;
				_invokeGameEvent(readyAction.ToGameEvent());

				if (_players.TrueForAll(
					eachPlayer => eachPlayer != null && eachPlayer.isReady))
				{
					_currentPhase = TichuPhase.kFirstHandout;
					_invokeGameEvent(new EvtPhaseChanged(_currentPhase));
				}
			};
			#endregion

			if (givenInfo.action is ActPlayerJoined)
				handleJoin(givenInfo.action as ActPlayerJoined);
			else if (givenInfo.action is ActPlayerReady)
				handleReady(givenInfo.action as ActPlayerReady);
		}
	}
}