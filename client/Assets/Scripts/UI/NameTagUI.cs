using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace tichu2018
{
	public interface INameTagUI
	{
		void SetReady(bool isReady);
		string nameText { get; set; }
	}

	public class NameTagUI : MonoBehaviour, INameTagUI
	{
		[SerializeField]
		private GameObject[] _readyObjects = null;
		[SerializeField]
		private GameObject[] _notReadyObjects = null;

		public void SetReady(bool isReady)
		{
			foreach (var eachObj in _readyObjects)
				eachObj.SetActive(isReady);
			foreach (var eachObj in _notReadyObjects)
				eachObj.SetActive(!isReady);
		}

		[SerializeField]
		private Text _nameText = null;
		public string nameText
		{
			get { return _nameText.text; }
			set { _nameText.text = value; }
		}

	}
}