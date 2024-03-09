using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caesar : MonoBehaviour
{
    public Vector3 CatchPosition;

    public Frame[] Frames;
    public float FrameDuration;

    private Eyes eyes;
    private SpriteRenderer sr;
    private Transform EyeTransform;
    private Transform MouthTransform;

    void Start()
    {
        eyes = transform.GetChild(0).GetComponent<Eyes>();
        EyeTransform = transform.GetChild(0);
        MouthTransform = transform.GetChild(1);
        sr = GetComponent<SpriteRenderer>();
    }

    public void AnimateCatch(int phase)
    {
        IEnumerator animate()
        {
            if (phase == 0)
            {
                eyes.SetDirection(1);
                for (int i = 0; i < 3; i++)
                {
                    sr.sprite = Frames[i].sprite;
                    EyeTransform.localPosition = new Vector3(Frames[i].EyesPosition.x, Frames[i].EyesPosition.y, Frames[i].EyesPosition.z);
                    MouthTransform.localPosition = new Vector3(Frames[i].MouthPosition.x, Frames[i].MouthPosition.y, Frames[i].MouthPosition.z);
                    yield return new WaitForSecondsRealtime(FrameDuration);
                }
            }else
            {
                eyes.SetDirection(2);
                for (int i = 3; i < Frames.Length; i++)
                {
                    sr.sprite = Frames[i].sprite;
                    EyeTransform.localPosition = new Vector3(Frames[i].EyesPosition.x, Frames[i].EyesPosition.y, Frames[i].EyesPosition.z);
                    EyeTransform.localRotation = Quaternion.AngleAxis(Frames[i].EyesPosition.w, Vector3.forward);

                    MouthTransform.localPosition = new Vector3(Frames[i].MouthPosition.x, Frames[i].MouthPosition.y, Frames[i].MouthPosition.z);
                    MouthTransform.localRotation = Quaternion.AngleAxis(Frames[i].MouthPosition.w, Vector3.forward);
                    yield return new WaitForSecondsRealtime(FrameDuration);
                }
                eyes.SetDirection(0);
                Board.available = true;
            }
        }
        StartCoroutine(animate());
    }
}

[System.Serializable]
public class Frame
{
    public Sprite sprite;
    public Vector4 MouthPosition;
    public Vector4 EyesPosition;
}