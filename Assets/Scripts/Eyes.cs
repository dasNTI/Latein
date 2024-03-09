using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyes : MonoBehaviour
{
    public List<EyeDirection> Frames;
    private SpriteRenderer sr;
    private bool blinking = false;
    private int direction = 0;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(Blink());
    }

    public void SetDirection(int dir)
    {
        direction = dir;
        if (blinking) return;
        sr.sprite = Frames[dir].frames[0];
    }

    IEnumerator Blink()
    {
        yield return new WaitForSeconds(Random.Range(2, 8));

        blinking = true;
        int l = Frames[direction].frames.Length;
        for (int i = 0; i < 2 * l - 1; i++) {
            int f = l - Mathf.Abs(l - i - 1) - 1;
            sr.sprite = Frames[direction].frames[f];
            yield return new WaitForSeconds(.125f);
        }
        blinking = false;

        StartCoroutine(Blink());
    }
}
[System.Serializable]
public class EyeDirection
{
    public Sprite[] frames;
}