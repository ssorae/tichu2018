using EXBoardGame.ActionChainModel;

namespace tichu2018
{
	public class EvtPlayerReady : GameEvent
	{
		public int playerIndex;
		public bool isReady;
	}

	public class EvtPhaseChanged : GameEvent
	{
		public TichuPhase targetPhase;

		public EvtPhaseChanged(TichuPhase phase)
		{
			targetPhase = phase;
		}
	}


	public static class TichuEventExtensions
	{
		public static EvtPlayerReady ToGameEvent(this ActPlayerReady me)
		{
			return new EvtPlayerReady
			{
				isReady = me.isReady, playerIndex = me.playerIndex
			};
		}
	}
}
