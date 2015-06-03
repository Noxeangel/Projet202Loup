using UnityEngine;
using System.Collections;

public class CollectibleBehaviour : MonoBehaviour {

    public CollectibleType type;
    public int value;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void KillMe()
    {
        this.gameObject.GetComponent<AudioSource>().Play();
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        Invoke("Death", 2.0f);
    }

    private void Death()
    {
        Destroy(this.gameObject);
    }
}

public enum CollectibleType
{
    TYPE1,
    TYPE2,
    TYPE3
}
