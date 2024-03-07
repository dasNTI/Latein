using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public float DraggedScale = 1.1f;
    public float DraggedScaleDuration = 0.2f;
    public float ReturnDuration = 0.5f;
    public float StartY;
    public float EndY;
    public float FrameDuration = 0.2f;
    public float SlideDuration = 0.75f;
    public LayerMask PaperLM;

    public int GoalPaperPosition = 0;
    public int Position = 0;
    public bool CorrectlyPlaced = false;

    Vector3 DragOffset;
    bool Draggable = true;
    bool Dragging = false;
    Vector3 InitDragPosition;

    private SpriteRenderer sr;
    private BoxCollider2D bc;
    public static BoxCollider2D CurrentDraggingBc = null;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
        if (transform.position.x == 12) return;

        transform.DOMoveY(EndY, SlideDuration);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!Draggable) return;
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouse.Scale(new Vector3(1, 1, 0));

            if (bc.OverlapPoint(mouse))
            {
                Dragging = true;
                DragOffset = transform.position - mouse;
                CurrentDraggingBc = bc;
                InitDragPosition = transform.position;
                transform.DOScale(Vector3.one * DraggedScale, DraggedScaleDuration);
            }
        }
        if (!Dragging) return;

        Vector3 Mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Mouse.Scale(new Vector3(1, 1, 0));
        transform.position = Mouse + DragOffset - Vector3.forward;
            
        if (Input.GetMouseButtonUp(0))
        {
            Draggable = false;
            Dragging = false;
            Invoke("CheckTouching", 2 * Time.deltaTime);
        }
    }

    void CheckTouching()
    {
        if (!CorrectlyPlaced) Return();
    }

    public void Return()
    {
        Draggable = false;
        Dragging = false;
        transform.DOScale(Vector3.one, DraggedScaleDuration);
        transform.DOMove(InitDragPosition, ReturnDuration).OnComplete(() =>
        {
            Draggable = true;
        });
    }
}
