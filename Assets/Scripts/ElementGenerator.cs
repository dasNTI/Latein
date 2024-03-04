using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementGenerator : MonoBehaviour
{
    public List<Company> Companies;
    public GameObject PaperInstance;
    public GameObject BoardInstance;

    public float[] PaperPositions;
    public float[] BoardPositions;

    public List<Company> SelectedCompanies;
    public static int LastBoardPosition = 0;
    public static int LastPaperPosition = 0;

    void Start()
    {
        SelectedCompanies = new List<Company>(Companies);
        for (int i = 0; i < 5; i++)
        {
            int v = Random.Range(0, SelectedCompanies.Count - 1);
            SelectedCompanies.RemoveAt(v);
        }

        NewPaper(0);
        NewPaper(1);
        NewPaper(2);
    }

    public void NewPaper(int position = -1)
    {
        if (position == -1) position = LastPaperPosition;
        GameObject instance = Instantiate(PaperInstance);
        
        Paper paper = instance.GetComponent<Paper>();
        instance.transform.position = new Vector3(PaperPositions[position], paper.StartY, 0);
        paper.Position = position;
    }
    void NewPaper()
    {
        NewPaper(-1);
    }

    public void NewBoard(int position = -1)
    {

    }
    void NewBoard()
    {
        NewBoard(-1);
    }
}

[System.Serializable]
public class Company
{
    public Sprite Logo;
    public Sprite Board;
    public AudioClip Explaination;
}