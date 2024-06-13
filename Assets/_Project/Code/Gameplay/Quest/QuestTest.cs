using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QuestTest : IQuest
{
    void IQuest.Start()
    {
        Debug.Log("Quest started.");
    }

    void IQuest.EndAndCleanup()
    {
        Debug.Log("Quest gracefully finished.");
    }
}
