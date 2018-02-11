using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tichu2018
{
	public class CardView : MonoBehaviour, IDisposable
	{
		public bool isDisposed { get; private set; } = false;
		public void Dispose()
		{
			if (this.isDisposed)
				return;
		}

		public static class Factory
		{
			public static CardView Create()
			{
				return null;
			}
		}
	}
}