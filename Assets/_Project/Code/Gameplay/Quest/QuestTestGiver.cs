using Code.Infrastructure.States;
using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class QuestTestGiver : MonoBehaviour
{
    private QuestSystem _questSystem;

    [Inject]
    public void Construct(QuestSystem questSystem) =>
            _questSystem = questSystem;

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.A))
        {
            UniTask.RunOnThreadPool(DoIt);
        }
    }

    private async void DoIt()
    {
        _questSystem.StartQuest<QuestTest>();
        await UniTask.Delay(1000);
        _questSystem.EndCurrentQuest();
    }
}
