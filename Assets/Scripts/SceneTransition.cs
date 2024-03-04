using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public RawImage image;
    private AudioSource audioSource;
    private static int CurrentTransitionState = 1;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Transition();
    }

    // Update is called once per frame
    public void Transition()
    {
        image.gameObject.SetActive(true);
       
        image.color = new Color(0, 0, 0, CurrentTransitionState);
        if (CurrentTransitionState == 0) audioSource.Play();
        image.DOBlendableColor(new Color(0, 0, 0, 1 - CurrentTransitionState), 1.5f).OnComplete(() =>
        {
            if (CurrentTransitionState == 0)
            {
                image.gameObject.SetActive(false);
            }else
            {
                if (SceneManager.GetActiveScene().buildIndex == 0)
                {
                    SceneManager.LoadScene("Game");
                }else
                {
                    SceneManager.LoadScene("Menu");
                }
            }
        });
        CurrentTransitionState = 1 - CurrentTransitionState;
    }
}
