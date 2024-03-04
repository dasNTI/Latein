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

    public void NewPaper(int position)
    {
        GameObject instance = Instantiate(PaperInstance);
        
        Paper paper = instance.GetComponent<Paper>();
        instance.transform.position = new Vector3(PaperPositions[position], paper.StartY, 0);
        paper.Position = position;
    }

    public void NewBoard(int position)
    {

    }
}

[System.Serializable]
public class Company
{
    public Sprite Logo;
    public Sprite Board;
    public AudioClip Explaination;
}