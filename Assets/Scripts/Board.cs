using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public float DraggedScale = 1.1f;
    public float DraggedScaleDuration = 0.2f;
    public float ReturnDuration = 0.5f;

    Vector3 DragOffset;
    bool Draggable = true;
    bool Dragging = false;
    Vector3 InitDragPosition;

    private SpriteRenderer sr;
    private BoxCollider2D bc;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
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
            transform.DOScale(Vector3.one, DraggedScaleDuration);
            transform.DOMove(InitDragPosition, ReturnDuration).OnComplete(() =>
            {
                Draggable = true;
            });
        }
    }
}
