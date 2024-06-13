using Code.Infrastructure.Factories;
using Code.Infrastructure.States;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSystem
{
    private Dictionary<Type, IQuest> _quests;
    private IQuest _currentQuest;

    private QuestSystem _questSystem;
    private QuestFactory _questFactory;

    public QuestSystem(QuestSystem questSystem, QuestFactory questFactory)
    {
        _quests = new Dictionary<Type, IQuest>();
        _questSystem = questSystem;
        _questFactory = questFactory;
    }

    public void RegisterAllQuests()
    {
        _questSystem.RegisterQuest(_questFactory.Create<QuestTest>());
    }

    public void StartQuest<TQuest>() where TQuest: class, IQuest
    {
        Debug.Assert(_currentQuest == null);

        _currentQuest = GetQuest<TQuest>();
        _currentQuest.Start();
    }

    public void EndCurrentQuest()
    {
        Debug.Assert(_currentQuest != null);

        _currentQuest.EndAndCleanup();
        _currentQuest = null;
    }

    private TQuest GetQuest<TQuest>() where TQuest : class, IQuest =>
            _quests[typeof(TQuest)] as TQuest;
    public void RegisterQuest<TQuest>(TQuest quest) where TQuest : class, IQuest =>
            _quests.Add(typeof(TQuest), quest);
}
