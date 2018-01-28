using System;
using UnityEngine;
using System.Collections.Generic;

namespace EXBoardGame.ActionChainModel
{
	public class LocalActionChain : ActionChain
	{
		public LocalActionChain()
		{
			this.random = new System.Random();
		}

		public override void TryAppend(GameAction actionToAppend)
			=> _chain.AddLast(
				new ActionInfo(actionToAppend, Mathf.FloorToInt(Time.time)));
	}
}