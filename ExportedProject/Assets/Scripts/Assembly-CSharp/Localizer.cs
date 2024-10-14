using System.Collections.Generic;
using UnityEngine;

public class Localizer : MonoBehaviour
{
	private static Dictionary<string, string> dict = new Dictionary<string, string>
	{
		{ "School Run Away", "スクール鬼ごっこ" },
		{ "Just Run Away 90 seconds", "90秒間を逃げ切れ" },
		{ "Level", "レベル" },
		{ "Easy", "やさしい" },
		{ "Normal", "ふつう" },
		{ "Hard", "むずかしい" },
		{ "START", "スタート" },
		{ "You Win!!", "逃げ切り成功！" },
		{ "You Lose", "ざんねん。キミの負け" },
		{ "TRY AGAIN", "もういっかい" },
		{ "SCORE", "スコア" },
		{ "Time Left", "残り時間" }
	};

	private void Start()
	{
	}

	private void Update()
	{
	}

	public static string LocalizedString(string key)
	{
		string result = key;
		if (Application.systemLanguage == SystemLanguage.Japanese && dict.ContainsKey(key))
		{
			result = dict[key];
		}
		return result;
	}
}
