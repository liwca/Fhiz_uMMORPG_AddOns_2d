// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================

using UnityEngine;
using UnityEngine.UI;

// =======================================================================================
// UCE_UIMobileControls
// =======================================================================================
public class UCE_UIMobileControls : MonoBehaviour
{
    public Button targetButton;
    public bool alwaysActive;

    private bool initalized = false;

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            GameObject go = transform.GetChild(i).gameObject;
            if (go)
                go.SetActive(false);
        }
    }

    private void Update()
    {
        if (!initalized)
        {
            Player player = Player.localPlayer;

            if (!player)
                return;

            if (Input.touchSupported || alwaysActive)
            {
                for (int i = 0; i < transform.childCount; ++i)
                {
                    GameObject go = transform.GetChild(i).gameObject;
                    if (go)
                        go.SetActive(true);
                }
            }
            initalized = true;
        }
    }
}