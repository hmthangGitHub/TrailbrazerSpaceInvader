using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFinishGameScreen : MonoBehaviour
{
    [SerializeField] private GameObject youWinLabel;
    [SerializeField] private GameObject gameOverLabel;
    [SerializeField] private Button restartButton;
    
    public struct Entity
    {
        public bool isWin;
        public Action restartAction;
    }
    

    public void ShowResult(UIFinishGameScreen.Entity entity)
    {
        gameObject.SetActive(true);
        youWinLabel.SetActive(entity.isWin);
        gameOverLabel.SetActive(!entity.isWin);
        restartButton.onClick.RemoveAllListeners();
        restartButton.onClick.AddListener(() => entity.restartAction());
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
