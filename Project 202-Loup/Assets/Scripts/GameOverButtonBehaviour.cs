using UnityEngine;
using System.Collections;

public class GameOverButtonBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnButtonClicked()
    {
        Application.LoadLevel("MainMenu");
    }
}
