using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour
{
    public int Position = 0;

    public Sprite[] Sprites;
    public float StartY;
    public float EndY;
    public float FrameDuration = 0.2f;
    public float SlideDuration = 0.75f;
    public Sprite CompanySprite;
    public LayerMask BoardLayer;

    private SpriteRenderer sr;
    private SpriteRenderer childSr;
    private BoxCollider2D bc;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
        childSr = transform.GetChild(0).GetComponent<SpriteRenderer>();

        //transform.position = Vector3.up * StartY + Vector3.right * transform.position.x;
        transform.DOMoveY(EndY, SlideDuration).OnComplete(() =>
        {
            StartCoroutine(Animate(AnimationDirection.Opening, () =>
            {
                childSr.sprite = CompanySprite;
            }));
        });
    }

    enum AnimationDirection : int
    {
        Closing = 0,
        Opening = 1
    }
    IEnumerator Animate(AnimationDirection type, Action callback)
    {
        if (type == AnimationDirection.Opening)
        {
            for (int i = 0; i < Sprites.Length; i++)
            {
                sr.sprite = Sprites[i];
                yield return new WaitForSeconds(FrameDuration);
            }
        } else { 
            for (int i = Sprites.Length - 1; i >= 0; i--)
            {
                sr.sprite = Sprites[i];
                yield return new WaitForSeconds(FrameDuration);
            }
        }

        callback();
    }

    void Update()
    {
        if (!Input.GetMouseButtonUp(0)) return;
        if (!bc.IsTouchingLayers(BoardLayer)) return;
    }
}
