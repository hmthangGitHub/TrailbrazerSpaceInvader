using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;
    private int currentScore;

    private void Start()
    {
        EventDispatcher.Subscribe<InvaderDeadEvent>(OnInvaderDead);
    }

    private void OnInvaderDead(InvaderDeadEvent invaderDeadEvent)
    {
        currentScore += invaderDeadEvent.score;
        score.text = currentScore.ToString();
    }

    private void OnDestroy()
    {
        EventDispatcher.Unsubscribe<InvaderDeadEvent>(OnInvaderDead);
    }
}
