using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStartGame : MonoBehaviour
{
    [SerializeField] private Button startBtn;
    public struct Entity
    {
        public Action startAction;
    }

    public void Show(UIStartGame.Entity entity)
    {
        gameObject.SetActive(true);
        startBtn.onClick.RemoveAllListeners();
        startBtn.onClick.AddListener(() => entity.startAction());
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
