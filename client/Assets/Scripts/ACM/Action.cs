namespace EXBoardGame.ActionChainModel
{
	public abstract class Action
	{
	}

	public sealed class ActionInfo
	{
		public ActionInfo(Action action, uint timeStamp)
		{
			this.action = action;
			this.timeStamp = timeStamp;
		}
		private ActionInfo() { }

		public Action action { get; private set; }
		public uint timeStamp { get; private set; }
	}
}