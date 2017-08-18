using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public static class Extensions {
	//extension method that shuffles a list
	public static void Shuffle<T>(this IList<T> list) {
		int n = list.Count;
		while (n > 1) {
			int k = (Random.Range(0, n) % n);
			n--;
			T value = list[k];
			list[k] = list[n];
			list[n] = value;
		}
	}
}

//how to change rich text
//https://docs.unity3d.com/Manual/StyledText.html

public class QuizFactory : MonoBehaviour {

	private DataHolder dataHolder;

	//Quiz related variables
	private List<string> wordsNotTested;
	private string currentWord;
	private int winStreak;
	private int numTested;

	void Start() {
		dataHolder = FindObjectOfType<DataHolder> ();

		wordsNotTested = new List<string> ();
		winStreak = 0;
		numTested = 0;

		//creates a shallow clone of questionsTested (at the started, all the words are not tested)
		foreach (var question in dataHolder.quiz.questions) {
//			questionsNotTested.Add(word);
		}

		wordsNotTested.Shuffle();

	}

	//returns a word if there are more words to be tested, otherwise return null
	string NextWord() {
		if (wordsNotTested.Count == 0) return null;

		numTested++;

		var result = wordsNotTested[0];
		wordsNotTested.RemoveAt(0);
		return result;
	}

}