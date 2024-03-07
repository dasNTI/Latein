using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperItem : MonoBehaviour
{
    public Caesar caesar;
    private float InitZ;
    private void Start()
    {
        InitZ = transform.position.z;    
    }

    public void Animate(Vector3 startPosition)
    {
        transform.position = new Vector3(startPosition.x, startPosition.y, InitZ);
        caesar.AnimateCatch(0);
        transform.DOMove(caesar.CatchPosition, 2f).OnComplete(() =>
        {
            caesar.AnimateCatch(1);
            Destroy(gameObject);
        });
    }
}
