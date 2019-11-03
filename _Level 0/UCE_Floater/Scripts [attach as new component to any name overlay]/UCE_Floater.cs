using UnityEngine;
using System.Collections;

// =======================================================================================
// UCE_Floater
// =======================================================================================
public class UCE_Floater : MonoBehaviour
{
    public float amplitude = 0.25f;
    public float frequency = 0.5f;

    private Vector3 posOffset;
    private Vector3 tempPos;

    private void Start()
    {
        posOffset = transform.localPosition;
    }

    private void Update()
    {
        if (amplitude != 0 && frequency != 0)
        {
            tempPos = posOffset;
            tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
        }

        if (transform.localPosition != tempPos)
            transform.localPosition = tempPos;
    }
}