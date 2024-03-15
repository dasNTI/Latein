using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouth : MonoBehaviour
{
    public float FrameDuration = 0.2f;
    public Sprite[] Frames;
    public Voiceline test;

    private SpriteRenderer sr;
    private AudioSource audioSource;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        //Speak(test);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Speak(Voiceline vl)
    {
        audioSource.clip = vl.audio;
        audioSource.Play();

        int frames = Mathf.FloorToInt(vl.audio.length / FrameDuration);

        IEnumerator speak()
        {
            for (int i = 0; i < frames; i++)
            {
                sr.sprite = Frames[Random.Range(0, Frames.Length)];
                yield return new WaitForSecondsRealtime(FrameDuration);
            }
            sr.sprite = Frames[0];
        }
        StartCoroutine(speak());
    }
}

[System.Serializable]
public class Voiceline
{
    public AudioClip audio;
    public List<int> frames;
}