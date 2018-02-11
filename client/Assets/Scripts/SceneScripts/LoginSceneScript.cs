using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace tichu2018
{
	public class LoginSceneScript : MonoBehaviour
	{
		[SerializeField]
		private LoginUI _loginUI = default(LoginUI);
		
		public void Awake()
		{
			_loginUI.OnLoginButtonClicked += _onLoginButtonClicked;
		}

		public void OnDestroy()
		{
			_loginUI.OnLoginButtonClicked -= _onLoginButtonClicked;
		}

		private void _onLoginButtonClicked()
		{
			var nicknameInput = _loginUI.nicknameInput;
			// TODO(sorae): isValidNickname() 이런걸로 나중에 빼야됨
			if (string.IsNullOrEmpty(nicknameInput))
				return;

			DataContainer.instance.myNickname = nicknameInput;

			SceneManager.LoadScene(GameSceneScript.GAME_SCENE_NAME, LoadSceneMode.Single);
		}
		
	}
}