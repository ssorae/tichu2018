using EXBoardGame.ActionChainModel;

namespace tichu2018
{
	public class TichuController : GameConroller
	{
		public void Join(string name, int myClientIndex)
		{
			var joinAction = new ActPlayerJoined
			{
				playerIndex = myClientIndex,
				playerName = name
			};
			this._publishAction(joinAction);
		}

		public void SetReady(bool isReady)
		{

		}
	}
}