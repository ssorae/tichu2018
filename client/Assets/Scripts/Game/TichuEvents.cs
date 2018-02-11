using EXBoardGame.ActionChainModel;

namespace tichu2018
{
	public struct EvtPlayerJoined : IGameEvent
	{
		public int playerIndex;
		public string nickName;
	}

	public struct EvtPlayerReady : IGameEvent
	{
		public int playerIndex;
		public bool isReady;
	}

	public struct EvtPhaseChanged : IGameEvent
	{
		public TichuPhase targetPhase;

		public EvtPhaseChanged(TichuPhase phase)
		{
			targetPhase = phase;
		}
	}


	public static class TichuEventExtensions
	{
		public static EvtPlayerJoined ToGameEvent(this ActPlayerJoined me)
		{
			return new EvtPlayerJoined
			{
				playerIndex = me.playerIndex,
				nickName = me.playerName,
			};
		}

		public static EvtPlayerReady ToGameEvent(this ActPlayerReady me)
		{
			return new EvtPlayerReady
			{
				isReady = me.isReady,
				playerIndex = me.playerIndex,
			};
		}
	}
}
