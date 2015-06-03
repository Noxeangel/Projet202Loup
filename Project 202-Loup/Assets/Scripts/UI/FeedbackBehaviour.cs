using UnityEngine;
using System.Collections;

public class FeedbackBehaviour : MonoBehaviour {

    public float timeBeforeDeath = 1.0f;
    public float movementSpeed = 2.0f;

	// Use this for initialization
	void Start () {
        Invoke("KillMe", timeBeforeDeath);
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = this.transform.position + new Vector3(0, 1, 0) * movementSpeed * Time.deltaTime;
	}

    private void KillMe()
    {
        Destroy(this.gameObject);
    }
}
