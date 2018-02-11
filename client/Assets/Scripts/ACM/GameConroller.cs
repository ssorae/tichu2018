using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXBoardGame.ActionChainModel
{
	public abstract class GameConroller
	{
		public event Action<GameAction> onPlayerAction = delegate { };

		protected void _publishAction(GameAction action)
			=> onPlayerAction.Invoke(action);
	}
}
