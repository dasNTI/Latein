using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyes : MonoBehaviour
{
    public Sprite[] Frames;
    private SpriteRenderer sr;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        yield return new WaitForSeconds(Random.Range(2, 8));

        int l = Frames.Length;
        for (int i = 0; i < 2 * l - 1; i++) {
            int f = l - Mathf.Abs(l - i - 1) - 1;
            sr.sprite = Frames[f];
            yield return new WaitForSeconds(.125f);
        }

        StartCoroutine(Blink());
    }
}
