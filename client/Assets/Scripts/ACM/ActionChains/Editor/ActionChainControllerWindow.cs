using EXBoardGame.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace EXBoardGame.ActionChainModel.Editor
{
	public class ActionChainEditorWindow : EditorWindow
	{
		[MenuItem("EXBoardGame/ActionChainEditor")]
		public static void Init()
		{
			var newWindow = (ActionChainEditorWindow)EditorWindow.GetWindow(typeof(ActionChainEditorWindow));
			newWindow.titleContent = new GUIContent("AC Editor");
			newWindow.Show();
		}
		public void OnGUI()
		{
			var targetChain = ActionChain.AllChains.FirstOrDefault();

			if (targetChain == null)
				_onGUI_NoTarget();
			else
				_onGUI_WithTarget(targetChain);
		}

		private void _onGUI_NoTarget()
		{
			GUILayout.Label("There's no activated ActionChain", EditorStyles.boldLabel);
		}

		private int _selectedTab = 0;

		private GameAction _lastSelectedAction = null;

		private void _onGUI_WithTarget(ActionChain targetChain)
		{
			GUILayout.Label("Target Chain : " + targetChain.GetType().Name, EditorStyles.boldLabel);

			_selectedTab = GUILayout.Toolbar(_selectedTab, new string[] { "Chain View", "Append New GameAction" });
			switch(_selectedTab)
			{
				// chain view
				case 0:
					EditorGUILayout.BeginHorizontal();
					var selectedAction = _printChain(targetChain);
					if (selectedAction != null)
						_lastSelectedAction = selectedAction;
					if (_lastSelectedAction != null)
						_drawActionContents(_lastSelectedAction);
					EditorGUILayout.EndHorizontal();
					break;
				// append new one
				case 1:
					_drawAppender(targetChain);
					break;
			}
		}

		private Vector2 _historyScrollPosition;
		private Vector2 _contentScrollPosition;

		/// <summary>
		/// Print all GameActions
		/// </summary>
		/// <param name="targetChain">Selected GameAction</param>
		/// TODO(sorae): 네이밍 어떻게 하지..
		private GameAction _printChain(ActionChain targetChain)
		{
			if (targetChain == null)
				return null;

			var selectedAction = null as GameAction;
			_historyScrollPosition = EditorGUILayout.BeginScrollView(
				_historyScrollPosition,
				GUILayout.Width(this.position.width * 1 / 2),
				GUILayout.Height(this.position.height * 2 / 3));
			{
				// in scroll view
 				Func<ActionInfo, bool> drawActionButton = actionToDraw =>
 					GUILayout.Button(
 						$"Player:{actionToDraw.action.playerIndex}  " +
 						$"Type:{actionToDraw.action.GetType().Name}  " +
 						$"Time:{actionToDraw.timeStamp}",
 						GUILayout.Width(this.position.width * 1/2 - 10));
 
 				foreach (var eachActionInfo in targetChain.AsEnumerable())
 					if (drawActionButton(eachActionInfo))
 						selectedAction = eachActionInfo.action;
			}
			EditorGUILayout.EndScrollView();

			return selectedAction;
		}

		private void _drawActionContents(GameAction targetAction)
		{
			if (targetAction == null)
				return;

			_contentScrollPosition = GUILayout.BeginScrollView(
				_contentScrollPosition,
				false, true,
				GUILayout.Width(this.position.width * 1 / 2),
				GUILayout.Height(this.position.height * 2 / 3));
			{
				var content = JsonUtility.ToJson(targetAction, prettyPrint: true);
				GUILayout.Label(content);
			}
			GUILayout.EndScrollView();
		}

		private int _selectedActionTypeIndex = 0;

		private static readonly System.Type[] ALL_TYPES_OF_GAMEACTION = ReflectionUtility.GetAllTypesOf<GameAction>().ToArray();
		private static readonly string[] ACTION_TYPENAMES = ALL_TYPES_OF_GAMEACTION.Select(type => type.Name).ToArray();

		private Dictionary<string, string> _currentFieldInputs = new Dictionary<string, string>();

		private void _drawAppender(ActionChain targetChain)
		{
			if(ALL_TYPES_OF_GAMEACTION == null || ALL_TYPES_OF_GAMEACTION.Length == 0)
			{
				GUILayout.Label("There's no GameAction types", EditorStyles.boldLabel);
				return;
			}

			var lastIndex = _selectedActionTypeIndex;
			_selectedActionTypeIndex 
				= EditorGUILayout.Popup("Action Type",_selectedActionTypeIndex, ACTION_TYPENAMES);
			if (lastIndex != _selectedActionTypeIndex)
				_currentFieldInputs.Clear();

			var selectedType = ALL_TYPES_OF_GAMEACTION[_selectedActionTypeIndex];

			// TODO(sorae): field들 하나씩 읽어서, field 입력창 만들어 준다.
			List<KeyValuePair<string, string>> typeNameAndValues = new List<KeyValuePair<string, string>>();
			foreach(var eachField in selectedType.GetFields())
			{
				if (false == _currentFieldInputs.ContainsKey(eachField.Name))
					_currentFieldInputs.Add(eachField.Name, string.Empty);

				var value = EditorGUILayout.TextField(eachField.Name, _currentFieldInputs[eachField.Name]);
				_currentFieldInputs[eachField.Name] = value;
				typeNameAndValues.Add(new KeyValuePair<string, string>(eachField.Name, value));
			}

			if(GUILayout.Button("Append Now"))
			{
				var json = "{";
				foreach(var eachField in typeNameAndValues)
				{
					var valueString = 
						eachField.Value == "true" 
						|| eachField.Value == "false" 
						? eachField.Value : $"\"{eachField.Value}\"";
					json += $"\"{eachField.Key}\":{valueString},";
				}
				// remove last char ','
				json = json.Remove(json.Length - 1);
				json += "}";

				var newInstance = Activator.CreateInstance(selectedType) as GameAction;
				JsonUtility.FromJsonOverwrite(json, newInstance);

				targetChain.TryAppendFromClient(newInstance);
			}
		}
	}
}