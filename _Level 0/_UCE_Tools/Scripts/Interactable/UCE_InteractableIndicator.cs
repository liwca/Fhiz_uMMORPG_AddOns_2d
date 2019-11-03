// =======================================================================================
// Created and maintained by iMMO
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/iMMOban
// =======================================================================================

using UnityEngine;

// =======================================================================================
// UCE INTERACTABLE INDICATOR
// =======================================================================================
public partial class UCE_InteractableIndicator : MonoBehaviour
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

// =======================================================================================