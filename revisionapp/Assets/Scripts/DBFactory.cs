using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//https://www.packtpub.com/books/content/using-rest-api-unity-part-1-what-rest-and-basic-queries

[System.Serializable]
public class Student {
    public string display_name;
	//this is the student id in smartschool (current) database
	public string id;
	//this is student id in legacy boshipanda database
	public string legacy_mongodb_id;
    public int coins;
    public string avatar;
 }

[System.Serializable]
public class Quiz {
	public string term_id;
	//term_index refers to index position of quiz within the term (i.e. 0 could mean first quiz of term 3)
	public int term_index;
	//index refers to index position of quiz within the whole year; used to sort quizzes
	public int index;
	public string chinese_title;
	public List<Question> questions;
}

[System.Serializable]
public class Question {
	public string question_text;
	public List<string> choices;
	public string correct_answer;
}


[System.Serializable]
public class AuthenticationKey {
	public string key;
}

//note that the way GET and POST is set up in revisionapp is slightly different from previous apps
//instead of using Results in previous apps, it is accessed as an argument to the onComplete function passed to it
public class DBFactory : MonoBehaviour {

    //for get requests that don't require a token
    public WWW GET(string url, System.Action<string> onComplete, System.Action onError) {
        return GET(url, null, onComplete, onError);
    }

    public WWW GET(string url, string token, System.Action <string> onComplete, System.Action onError) {
        var form = new WWWForm();
        var headers = new Dictionary<string, string> ();
        if (token != null) {
            headers.Add("Authorization", "Token " + token);
        }
        
        WWW www = new WWW(url, null, headers);
        
        StartCoroutine(WaitForRequest(www, onComplete, onError));
        return www;
    }

    //for post requests that don't require a token
    public WWW POST(string url, Dictionary<string, string> post, System.Action <string> onComplete, System.Action onError) {
        WWWForm form = new WWWForm();

        foreach (KeyValuePair<string, string> post_arg in post) {
            form.AddField(post_arg.Key, post_arg.Value);
        }

        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequest(www, onComplete, onError));
        return www;
    }

	//public WWW AUTHENTICATION (string url, string token, System.Action <string> onComplete, System.Action onError) {
		//WWWForm form = new WWWForm();

		//Hashtable headers = new Hashtable();
		//headers["Authorization"] = "Token " + token;

		//WWW www = new WWW(url, form, headers);
		//StartCoroutine(WaitForRequest(www, onComplete, onError));
		//return www;
	//}

    //KIV -> if onError is not passed in and there is an error, nothing will happen
	private IEnumerator WaitForRequest(WWW www, System.Action <string> onComplete, System.Action onError) {
        yield return www;

        if (www.error == null) {
            //some GET/POST requests do not have an onComplete function
            if (onComplete != null) {
				onComplete(www.text);
            }
        }
        else if (onError != null) {
            onError();
        }
    }

}
