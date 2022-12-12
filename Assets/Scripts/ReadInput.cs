using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;

public class ReadInput : MonoBehaviour
{
    private string input;
    private Text levelText;
    private string stringLvl;
    private int level;
    private int comparePoints;
    private string justOneValue;

    public void ReadStringInput(string namePlayer)
    {
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        stringLvl = levelText.text;
        level = (int)Char.GetNumericValue(stringLvl[0]);

        TextReader Reader = new StreamReader("Score.txt");
        string str = Reader.ReadToEnd();

        string[] valuesRankArray = str.Split(',');


        for (int i = 1; i < 5; i++)
        {
            justOneValue = valuesRankArray[((i - 1) * 2) + 1];
            comparePoints = int.Parse(justOneValue);

            if (level > comparePoints)
            {
                valuesRankArray[((i - 1) * 2) + 1] = level.ToString(); ;
                valuesRankArray[(i - 1)*2] = namePlayer;
            }
        }

        Reader.Close();

        TextWriter Writer = new StreamWriter("Score.txt");
        Writer.WriteLine(valuesRankArray);
        Writer.Close();

    }

}
