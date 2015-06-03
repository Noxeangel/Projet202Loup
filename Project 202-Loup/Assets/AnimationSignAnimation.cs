using UnityEngine;
using System.Collections;

public class AnimationSignAnimation : MonoBehaviour {

    public float angleSpeed = 45.0f;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(new Vector3(0, 0, 1), angleSpeed * Time.deltaTime);


	}
}
