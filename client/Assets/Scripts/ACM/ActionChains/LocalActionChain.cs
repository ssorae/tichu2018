using System;
using UnityEngine;
using System.Collections.Generic;

namespace EXBoardGame.ActionChainModel
{
	public class LocalActionChain : ActionChain
	{
		private float _initialTime = Time.time;

		public LocalActionChain()
		{
			this.random = new System.Random();
			this.myClientIndex = 0;
		}

		public override void TryAppendFromClient(GameAction actionToAppend)
			=> _onAppendSucceed(new ActionInfo(
					action: actionToAppend,
					timeStamp: Mathf.FloorToInt(Time.time - _initialTime)));
	}
}