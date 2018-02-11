namespace EXBoardGame.ActionChainModel
{
	public abstract class GameViewer
	{
		public abstract void ApplyGameEvent(IGameEvent @event);
	}
}
