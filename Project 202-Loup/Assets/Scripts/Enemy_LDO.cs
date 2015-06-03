using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Enemy_LDO : MonoBehaviour
{
    public Collider _colliderValidate;
    public Collider _colliderKill;
    public Collider _colliderRespawn;
    public LDO_Manager LdoManager;

    private void Start()
    {
        LdoManager = GameObject.Find("_Manager").GetComponent<LDO_Manager>();
    }
}