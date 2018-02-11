using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tichu2018
{
	public class DataContainer
	{
		public static DataContainer instance { get; set; } = new DataContainer();

		public void Clear()
		{
			myNickname = null;
		}

		public string myNickname { get; set; }
	}
}