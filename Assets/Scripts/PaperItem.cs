using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperItem : MonoBehaviour
{
    public Caesar caesar;

    public void Animate(Vector3 startPosition)
    {
        transform.position = startPosition;
        caesar.AnimateCatch(0);
        transform.DOMove(caesar.CatchPosition, 2f).OnComplete(() =>
        {
            caesar.AnimateCatch(1);
            Destroy(gameObject);
        });
    }
}
