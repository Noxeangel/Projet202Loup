using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {


    public Transform target;
    public bool isFinished = false;



	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        this.transform.position = new Vector3(target.transform.position.x,this.transform.position.y, this.transform.position.z);


        if(isFinished == true)
        {

        }

	}


}


