using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{

    public static GameData Data;
    public int ModeId;

    void Awake()
    {
        MakeThisTheOnlyData();
    }

    public void SetModeId(int Id)
    {
        ModeId = Id;
    }

    void MakeThisTheOnlyData()
    {
        if (Data == null)
        {
            DontDestroyOnLoad(gameObject);
            Data = this;
        }
        else
        {
            if (Data != this)
            {
                Destroy(gameObject);
            }
        }
    }
}

