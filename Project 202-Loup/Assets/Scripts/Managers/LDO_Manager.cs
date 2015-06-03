using System.Collections;
using System.Linq;
using UnityEngine;

public class LDO_Manager : MonoBehaviour
{
    private AudioManager _audioManager;

    public AudioClip[] audioCollectible;
    public AudioClip[] audioJump;
    public AudioClip[] audioEnemy;

    public enum Ldo
    {
        Collectible,
        Jump,
        Enemy
    }

    private void OnEnable()
    {
        _audioManager = GameObject.FindObjectOfType<AudioManager>();
    }

    private AudioClip RandomClip(Ldo type)
    {
        AudioClip clip = null;
        switch (type)
        {
            case Ldo.Collectible:
                clip = audioCollectible[0];
                break;

            case Ldo.Jump:
                clip = audioJump[Random.Range(0, audioJump.Length)];
                break;

            case Ldo.Enemy:
                clip = audioEnemy[Random.Range(0, audioEnemy.Length)];
                break;
        }
        return clip;
    }

    public void Ldo_Validate(bool isValidate, GameObject sender, Ldo type, GameObject player)
    {
        if (isValidate)
        {
            FeedBack_Good(sender, type);
        }
        else
        {
            FeedBack_Bad(sender, type, player);
        }
    }

    private void FeedBack_Good(GameObject sender, Ldo type)
    {
        sender.GetComponent<AudioSource>().PlayOneShot(RandomClip(type));
    }

    private void FeedBack_Bad(GameObject sender, Ldo type, GameObject player)
    {
        sender.GetComponent<AudioSource>().PlayOneShot(RandomClip(type));
        //Destroy(player);
    }
}