using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace tichu2018
{
	public class LoginUI : MonoBehaviour
	{
		[SerializeField]
		private InputField _nicknameInputField = null;
		[SerializeField]
		private Button _loginButton = null;

		public event Action OnLoginButtonClicked = delegate { };

		public string nicknameInput
		{
			get { return _nicknameInputField.text; }
		}

		public void Awake()
		{
			_loginButton.onClick.AddListener(() => OnLoginButtonClicked.Invoke());
		}
	}
}