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
    public int CompanyIndex;
    public LayerMask BoardLayer;
    public GameObject PaperItem;

    private SpriteRenderer sr;
    private SpriteRenderer childSr;
    private BoxCollider2D bc;
    private bool available = false;

    private Vector3 BoardSlidePosition;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
        childSr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        if (transform.position.x == 15) return;

        //transform.position = Vector3.up * StartY + Vector3.right * transform.position.x;
        transform.DOMoveY(EndY, SlideDuration).OnComplete(() =>
        {
            BoardSlidePosition = transform.position + Vector3.back * 2;
            StartCoroutine(Animate(AnimationDirection.Opening, () =>
            {
                childSr.sprite = CompanySprite;
                available = true;
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
        if (!available) return;
        if (!Input.GetMouseButtonUp(0)) return;
        if (Board.CurrentDraggingBc == null || !bc.OverlapPoint(Board.CurrentDraggingBc.transform.position)) return;

        GameObject CurrentBoard = Board.CurrentDraggingBc.gameObject;
        Board board = CurrentBoard.GetComponent<Board>();
        if (board.CompanyIndex != CompanyIndex)
        {
            board.Return();
            return;
        }

        board.CorrectlyPlaced = true;
        
        Board.CurrentBoards[Array.IndexOf(Board.CurrentBoards, CompanyIndex)] = -1;
        CurrentBoard.transform.DOScale(Vector3.one, .5f);
        CurrentBoard.transform.DOMove(BoardSlidePosition, .5f).OnComplete(() =>
        {
            ElementGenerator.LastPaperPosition = Position;
            ElementGenerator.LastBoardPosition = board.Position;
            Destroy(CurrentBoard);
            Destroy(childSr.gameObject);
            StartCoroutine(Animate(AnimationDirection.Closing, () =>
            {
                GameObject item = Instantiate(PaperItem);
                item.GetComponent<PaperItem>().Animate(transform.position);
                GameObject.Find("Generator").GetComponent<ElementGenerator>().Invoke("NewSet", 2f);
                Destroy(gameObject);
            }));
        });
        Board.CurrentDraggingBc = null;
    }
}
