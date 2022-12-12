using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;

public class RankManager : MonoBehaviour
{
    private Text nameRank;
    private Text pointsRank;


    private void Awake()
    {
        TextReader Reader = new StreamReader("Score.txt");  // se lee el .txt para escribir el score

        String str = Reader.ReadToEnd();

        string[] valuesRankArray = str.Split(',');

        for(int i=1; i < 5; i++)
        {
            nameRank = GameObject.Find("Pos" + i + "NameText").GetComponent<Text>();
            nameRank.text = valuesRankArray[(i-1)*2];

            pointsRank = GameObject.Find("PointsPos" + i + "Text").GetComponent<Text>();
            pointsRank.text = valuesRankArray[((i-1)*2)+1];
        }

        Reader.Close();
    }
}
