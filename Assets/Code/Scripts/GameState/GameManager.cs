using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UIInGame uiInGame;
    [SerializeField] private UIFinishGameScreen uiFinishGameScreen;
    [SerializeField] private UIStartGame uiStartGame;
    [SerializeField] private GameObject gamePlayContainer;
    private int currentScore = 0;
    private BulletBase.BulletTypes bulletType = BulletBase.BulletTypes.Fast;
    private State currentState = State.None;
    private UIInGame.Entity uiInGameEntity = new();

    public enum State
    {
        None,
        Start,
        Playing,
        Win,
        GameOver
    }

    private void Start()
    {
        EventDispatcher.Subscribe<ChangeGameStateEvent>(OnGameStateChange);
        
        OnGameStateChange(new ChangeGameStateEvent()
        {
            nextState = State.Start
        });
    }
    
    private void OnInvaderDead(InvaderDeadEvent invaderDeadEvent)
    {
        currentScore += invaderDeadEvent.score;
        uiInGameEntity.score = currentScore;
        uiInGame.SetEntity(uiInGameEntity);
    }

    private void OnGameStateChange(ChangeGameStateEvent changeGameStateEvent)
    {
        if (currentState == changeGameStateEvent.nextState) return;
        currentState = changeGameStateEvent.nextState;
        
        switch (changeGameStateEvent.nextState)
        {
            case State.GameOver:
                OnGameFinish(false);
                break;
            case State.Win:
                OnGameFinish(true);
                break;
            case State.Start:
                OnStartGame();
                break;
            case State.Playing:
                OnStartPlaying();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void OnStartPlaying()
    {
        EventDispatcher.Subscribe<InvaderDeadEvent>(OnInvaderDead);
        EventDispatcher.Subscribe<BulletTypeChangeEvent>(OnChangeBulletType);
        currentScore = 0;
        bulletType = BulletBase.BulletTypes.Fast;
        uiInGameEntity.score = currentScore;
        uiInGameEntity.currentBulletType = (int)bulletType;
        uiInGame.Show(true);
        uiInGame.SetEntity(uiInGameEntity);
        gamePlayContainer.gameObject.SetActive(true);
    }

    private void OnChangeBulletType(BulletTypeChangeEvent bulletTypeChangeEvent)
    {
        bulletType = bulletTypeChangeEvent.bulletType;
        uiInGameEntity.currentBulletType = (int)bulletType;
        uiInGame.SetEntity(uiInGameEntity);
    }

    private void OnStartGame()
    {
        uiStartGame.Show(new UIStartGame.Entity()
        {
            startAction = () =>
            {
                uiStartGame.Hide();
                OnGameStateChange(new ChangeGameStateEvent()
                {
                    nextState = State.Playing
                });
            }
        });
        uiInGame.Show(false);
        uiFinishGameScreen.Hide();
        gamePlayContainer.SetActive(false);
    }

    private void OnGameFinish(bool isWin)
    {
        EventDispatcher.Unsubscribe<InvaderDeadEvent>(OnInvaderDead);
        EventDispatcher.Unsubscribe<BulletTypeChangeEvent>(OnChangeBulletType);
        uiFinishGameScreen.ShowResult(new UIFinishGameScreen.Entity()
        {
            isWin = isWin,
            restartAction = () =>
            {
                OnGameStateChange(new ChangeGameStateEvent()
                {
                    nextState = State.Playing
                });
                uiFinishGameScreen.Hide();
            }
        });
        uiInGame.Show(false);
        gamePlayContainer.SetActive(false);
    }

    private void OnDestroy()
    {
        EventDispatcher.Unsubscribe<ChangeGameStateEvent>(OnGameStateChange);
        EventDispatcher.Unsubscribe<InvaderDeadEvent>(OnInvaderDead);
    }
}