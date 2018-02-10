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

		public override void TryAppendFromClient(GameAction actionToAppend)
			=> _onAppendSucceed(new ActionInfo(actionToAppend, Mathf.FloorToInt(Time.time)));
	}
}