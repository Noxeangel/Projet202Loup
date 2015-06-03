using System.Collections;
using UnityEngine;

public class JumpCollider_LDO : MonoBehaviour
{
    private Jump_LDO jumpLdo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            jumpLdo = this.GetComponentInParent<Jump_LDO>();
            jumpLdo.Collision(this.GetComponent<Collider>(), other.gameObject);
        }
    }
}