using EXBoardGame.ActionChainModel;

namespace tichu2018
{
	public class ActPlayerJoined : GameAction
	{
		public string playerName;
	}

	public class ActPlayerReady : GameAction
	{
		public bool isReady;
	}
}
