using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Jump_LDO : MonoBehaviour
{
    public Collider _colliderValidate;
    public Collider _colliderKill;
    public Collider _colliderRespawn;
    public LDO_Manager LdoManager;

    private void Start()
    {
        LdoManager = GameObject.Find("_Manager").GetComponent<LDO_Manager>();
    }

    public void Collision(Collider col, GameObject player)
    {
        if (col.name == _colliderValidate.name)
        {
            LdoManager.Ldo_Validate(true, this.gameObject, LDO_Manager.Ldo.Jump, player);
        }
        else if (col.name == _colliderKill.name)
        {
            LdoManager.Ldo_Validate(false, this.gameObject, LDO_Manager.Ldo.Jump, player);
        }
    }
}