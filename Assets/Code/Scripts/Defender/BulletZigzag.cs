using System;
using UnityEngine;
using UnityEngine.Splines;

public class BulletZigzag : BulletBase
{
    [SerializeField] private SplineContainer path;
    private float curveLength;
    private float currentLength = 0;
    private Vector3 enablePosition;

    private void Start()
    {
        curveLength = path.Spline.GetLength();
    }

    private void OnEnable()
    {
        currentLength = 0;
        enablePosition = transform.position;
    }

    void Update()
    {
        currentLength += speed * Time.deltaTime;
        path.Spline.Evaluate(currentLength / curveLength, out var position, out var tangent, out var upVector);
        transform.position = enablePosition + (Vector3)position;
    }
}
