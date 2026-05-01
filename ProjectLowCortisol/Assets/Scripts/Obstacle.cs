using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float speed = 5f;
    private float despawnX;

    private float targetSize = 2f;

    [SerializeField] private Sprite[] sprites;

    public void Init(float despawnX)
    {
        this.despawnX = despawnX;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Sprite randomSprite = sprites[Random.Range(0, sprites.Length)];

        sr.sprite = randomSprite;

        FitSprite(sr);
    }

    void Update()
    {

        transform.Translate(Vector2.left * speed * Time.deltaTime);
        //if (transform.position.x < despawnX)
            //Destroy(gameObject);
    }
    void FitSprite(SpriteRenderer sr)
    {
        float spriteWidth = sr.bounds.size.x;

        float scaleFactor = targetSize / spriteWidth;

        transform.localScale = Vector3.one * scaleFactor;
    }
}
