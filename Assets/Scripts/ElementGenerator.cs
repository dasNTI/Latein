using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public static int[] CurrentBoards = new int[3];
    public static int[] CurrentPapers = new int[3];
    private List<int> PossibleBoards = new List<int>();
    private int CompanyListIndex = 0;

    void Start()
    {
        SelectedCompanies = new List<Company>(Companies);
        for (int i = 0; i < 15; i++)
        {
            int v = Random.Range(0, SelectedCompanies.Count);
            SelectedCompanies.RemoveAt(v);
        }

        for (int i = 0; i < SelectedCompanies.Count; i++) {
            PossibleBoards.Add(i);
        }

        for (int i = 0; i < 3; i++)
        {
            NewBoard(i);
            NewPaper(i);
        }
    }

    public void NewPaper(int position = -1)
    {
        if (position == -1) position = LastPaperPosition;
        GameObject instance = Instantiate(PaperInstance);
        CurrentPapers[position] = CompanyListIndex;
        
        Paper paper = instance.GetComponent<Paper>();
        instance.transform.position = new Vector3(PaperPositions[position], paper.StartY, 0);
        paper.Position = position;
        paper.CompanySprite = SelectedCompanies[CompanyListIndex].Logo;
        paper.CompanyIndex = CompanyListIndex;
        CurrentPapers[position] = CompanyIndex;

        CompanyListIndex++;
    }
    void NewSet()
    {
        if (CompanyListIndex >= SelectedCompanies.Count) return;
        NewBoard(-1);
        NewPaper(-1);
    }

    public void NewBoard(int position = -1)
    {
        if (position == -1) position = LastBoardPosition;
        GameObject instance = Instantiate(BoardInstance);

        int index = -1;
        int[] CurrentPaperStack = Array.FindAll<int>(CurrentPapers, p => p is int);
        int Matches = 0;
        if (CompanyListIndex - SelectedCompanies.Count > 1) {
            foreach (int i in CurrentPaperStack) {
                if (Array.IndexOf(CurrentBoards, i) > -1) Matches++;
            }
        }
        if (Matches == 0) {
            index = CompanyListIndex;
        }else {
            index = (int)Random.Range(CompanyIndex, SelectedCompanies.Count);
        }

        Board board = instance.GetComponent<Board>();
        instance.transform.position = new Vector3(BoardPositions[position], board.StartY, 0);
        board.MainWord.text = SelectedCompanies[index].MainWord;
        board.Subtitle.text = SelectedCompanies[index].Origin;
        CurrentBoards[position] = index;
        PossibleBoards.Remove(index);
        board.CompanyIndex = CompanyListIndex;
        board.Position = position;
    }
}

[System.Serializable]
public class Company
{
    public Sprite Logo;
    public string MainWord;
    public string Origin;
    public Voiceline Explaination;
}