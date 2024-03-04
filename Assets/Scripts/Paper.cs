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
    public GameObject PaperItem;

    private SpriteRenderer sr;
    private SpriteRenderer childSr;
    private BoxCollider2D bc;
    void Start()
    {
        if (transform.position.x == 12) return;
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
        if (Board.CurrentDraggingBc == null || !bc.OverlapPoint(Board.CurrentDraggingBc.transform.position)) return;
        GameObject CurrentBoard = Board.CurrentDraggingBc.gameObject;
        Board board = CurrentBoard.GetComponent<Board>();
        if (board.GoalPaperPosition != Position)
        {
            return;
        }

        CurrentBoard.transform.DOKill();
        Board.CurrentDraggingBc = null;
        CurrentBoard.transform.DOScale(Vector3.one, .5f);
        CurrentBoard.transform.DOMove(transform.position, .5f).OnComplete(() =>
        {
            ElementGenerator.LastPaperPosition = Position;
            ElementGenerator.LastBoardPosition = board.Position;
            Destroy(CurrentBoard);
            Destroy(childSr.gameObject);
            StartCoroutine(Animate(AnimationDirection.Closing, () =>
            {
                GameObject item = Instantiate(PaperItem);
                item.GetComponent<PaperItem>().Animate(transform.position);
                GameObject.Find("Generator").GetComponent<ElementGenerator>().Invoke("NewPaper", 2f);
                GameObject.Find("Generator").GetComponent<ElementGenerator>().Invoke("NewBoard", 2f);
                Destroy(gameObject);
            }));
        });
    }
}
