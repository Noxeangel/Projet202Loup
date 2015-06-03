using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ScoreUIBehaviour : MonoBehaviour {

    public int Score = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.GetComponent<Text>().text = "SCORE : " + Score.ToString();
	}
}
