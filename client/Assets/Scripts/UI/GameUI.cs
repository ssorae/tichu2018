using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tichu2018
{
	public interface IGameUI
	{
		NameTagUI[] NameTagUIs { get; }
	}

	public class GameUI : MonoBehaviour, IGameUI
	{
		[SerializeField]
		private NameTagUI[] _nameTagUIs = null;
		public NameTagUI[] NameTagUIs
		{
			get { return _nameTagUIs; }
		}


	}
}