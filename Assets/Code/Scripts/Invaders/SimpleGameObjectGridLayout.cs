using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteAlways]
public class SimpleGameObjectGridLayout : MonoBehaviour
{
    [SerializeField] private Vector2 padding;
    [SerializeField] private Vector2 spacing;
    [SerializeField] private int numberElementPerRow;
    [SerializeField] private Vector2 gridSize;

    public void CalculateGridLayout()
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            var colIndex = i % numberElementPerRow;
            var rowIndex = i / numberElementPerRow;
            var positionX = colIndex * (gridSize.x + spacing.x);
            var positionY = -(rowIndex * (gridSize.y + spacing.y));
            transform.GetChild(i).localPosition = padding + new Vector2(positionX, positionY);
        }
    }
    
#if UNITY_EDITOR
    private void Update()
    {
        if (!Application.isPlaying)
        {
            CalculateGridLayout();
        }
    }
#endif
}
