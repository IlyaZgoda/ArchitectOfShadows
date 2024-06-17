using Code.Infrastructure.States;
using Code.StaticData.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Costyl : MonoBehaviour
{
    private GameStateMachine _gameStateMachine;

    [Inject]
    public void Construct(GameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
    }
    void Start()
    {
        StartCoroutine(WaitForCredits());
    }

    private IEnumerator WaitForCredits()
    {
        yield return new WaitForSeconds(10); 
        _gameStateMachine.Enter<CreditsState, string>(Scenes.Credits);
    }

}
