using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private CanvasGroup[] bulletTypes;
    public struct Entity
    {
        public int score;
        public int currentBulletType;
    }

    public void SetEntity(UIInGame.Entity entity)
    {
        scoreText.text = entity.score.ToString();
        
        for (int i = 0; i < bulletTypes.Length; i++)
        {
            bulletTypes[i].alpha = i == entity.currentBulletType ? 1.0f : 0.2f;
        }
    }

    public void Show(bool isShow)
    {
        gameObject.SetActive(isShow);
    }
}
