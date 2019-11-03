using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// =======================================================================================
// UCE JUKEBOX
// =======================================================================================
[CreateAssetMenu(fileName = "UCE Jukebox", menuName = "UCE Templates/New UCE Jukebox", order = 999)]
public class UCE_Tmpl_Jukebox : ScriptableObject
{
    [Header("-=-=-=- UCE JUKEBOX -=-=-=-")]
    public bool isActive;

    [Header("[MENU MUSIC (Only functional on compiled client)]")]
    [Tooltip("This music plays (looped) while the player is not logged in")]
    public AudioClip menuMusicClip;
    public float menuFadeInFadeOut = 1.0f;
    [Range(0, 1)] public float menuAdjustedVol = 1.0f;

    [Header("[DEFAULT GAME MUSIC]")]
    [Tooltip("This music plays (looped) while logged in but not inside any music area")]
    public AudioClip defaultMusicClip;
    public float defaultFadeInFadeOut = 1.0f;
    [Range(0, 1)] public float defaultAdjustedVol = 1.0f;
}

// =======================================================================================