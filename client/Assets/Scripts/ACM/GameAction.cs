using UnityEngine;

namespace EXBoardGame.ActionChainModel
{
	public abstract class GameAction
	{
		public int playerIndex;

		public override string ToString()
		{
			return JsonUtility.ToJson(this);
		}
	}

	public sealed class ActionInfo
	{
		public ActionInfo(GameAction action, int timeStamp)
		{
			this.action = action;
			this.timeStamp = timeStamp;
		}
		private ActionInfo() { }

		public GameAction action { get; private set; }
		public int timeStamp { get; private set; }
	}
}