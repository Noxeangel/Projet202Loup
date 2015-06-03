using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(SpriteRenderer))]
public class Collectible_LDO : MonoBehaviour
{
    public enum ECollectible { Type1, Type2, Type3 };

    [Header("Type of this Object :")]
    public ECollectible Type;

    [Header("Amount of Score Point :")]
    public int ValueLow;

    public int ValueHigh;

    [Header("Sprite To Display :")]
    [Range(0.0f, 15.0f)]
    public float SpriteSizeX = 5.0f;

    [Range(0.0f, 15.0f)]
    public float SpriteSizeY = 5.0f;

    public Sprite[] CollectibleSprite;

    private bool isInit = false;
    public LDO_Manager LdoManager;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
            LdoManager.Ldo_Validate(true, this.gameObject, LDO_Manager.Ldo.Collectible, col.gameObject);
    }

    private void Start()
    {
        if (this.isInit == false)
            this.Init();
    }

    private void Update()
    {
        if (this.isInit)
            this.SpriteScale();
    }

    private void Init()
    {
        this.TypeChangeSprite();
        this.ColliderInit();
        this.SpriteScale();
        this.isInit = true;
    }

    private void SpriteScale()
    {
        Bounds bounds = GetComponent<SpriteRenderer>().sprite.bounds;
        float xSize = bounds.size.x;
        float ySize = bounds.size.y;

        transform.localScale = new Vector3(SpriteSizeX / xSize, SpriteSizeY / ySize, 0.0f);
    }

    private void ColliderInit()
    {
        GetComponent<BoxCollider2D>().size = CollectibleSprite[(int)Type].bounds.size;
    }

    private void TypeChangeSprite()
    {
        this.GetComponent<SpriteRenderer>().sprite = CollectibleSprite[(int)Type];
    }
}