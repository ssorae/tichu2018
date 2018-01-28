namespace EXBoardGame.ActionChainModel
{
	public abstract class GameAction
	{
	}

	public sealed class ActionInfo
	{
		public ActionInfo(GameAction action, uint timeStamp)
		{
			this.action = action;
			this.timeStamp = timeStamp;
		}
		private ActionInfo() { }

		public GameAction action { get; private set; }
		public uint timeStamp { get; private set; }
	}
}