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
            int v = Random.Range(0, SelectedCompanies.Count);
            SelectedCompanies.RemoveAt(i);
        }
    }

    public void NewPaper(int position)
    {
        GameObject instance = Instantiate(PaperInstance);
        instance.transform.position.Set(PaperPositions[position], 0, 0);
        
        Paper paper = instance.GetComponent<Paper>();
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