using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

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

    public static int[] CurrentBoards = new int[3] {-2, -2, -2 };
    public static int[] CurrentPapers = new int[3] {-1, -1, -1 };
    private int CompanyListIndex = 0;

    void Start()
    {
        SelectedCompanies = new List<Company>(Companies);
        for (int i = 0; i < 15; i++)
        {
            int v = UnityEngine.Random.Range(0, SelectedCompanies.Count);
            SelectedCompanies.RemoveAt(v);
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
        CurrentPapers[position] = CompanyListIndex;
        paper.Voiceline = SelectedCompanies[CompanyListIndex].Explaination;

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

        int GetIndex()
        {
            int index = -1;
            int[] CurrentPaperStack = Array.FindAll(CurrentPapers, p => p > -1);
            int Matches = 0;
            if (SelectedCompanies.Count - CompanyListIndex > 1)
            {
                foreach (int i in CurrentPaperStack)
                {
                    if (Array.IndexOf(CurrentBoards, i) > -1) Matches++;
                }
            }

            int LookBack = 4;
            if (CompanyListIndex > SelectedCompanies.Count / 2 && Array.Exists(CurrentPapers, p => p < CompanyListIndex - LookBack))
            {
                return Array.Find(CurrentPapers, p => p < CompanyListIndex - LookBack);
            }

            if (Matches == 0)
            {
                index = CompanyListIndex;
            }
            else
            {
                if (true)
                {
                    index = (int)UnityEngine.Random.Range(CompanyListIndex, Mathf.Min(CompanyListIndex + 3, SelectedCompanies.Count - 1));
                }
                else
                {
                    index = CompanyListIndex;
                }
            }

            while (Array.IndexOf(CurrentBoards, index) > -1)
            {
                index++;
                if (index == SelectedCompanies.Count - 2)
                {
                    break;
                }
            }

            return index;
        }

        int index = Mathf.Clamp(GetIndex(), CompanyListIndex, SelectedCompanies.Count - 1);

        Board board = instance.GetComponent<Board>();
        instance.transform.position = new Vector3(BoardPositions[position], board.StartY, 0);
        board.MainWord.text = SelectedCompanies[index].MainWord;
        board.Subtitle.text = SelectedCompanies[index].Origin;
        CurrentBoards[position] = index;
        board.CompanyIndex = index;
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