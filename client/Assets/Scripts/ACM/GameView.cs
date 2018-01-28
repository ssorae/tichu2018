namespace EXBoardGame.ActionChainModel
{
	public abstract class GameView
	{
		public abstract void ApplyGameEvent(GameEvent @event);
	}
}
