using UnityEngine;
using UnityEngine.UI;

// =======================================================================================
// SHORTCUTS
// =======================================================================================
public partial class UCE_UI_Factions_Shortcuts : MonoBehaviour
{
    public Button FactionsButton;
    public GameObject FactionsPanel;

    // -----------------------------------------------------------------------------------
    // Update
    // -----------------------------------------------------------------------------------
    public void Update()
    {
        FactionsButton.onClick.SetListener(() =>
        {
            FactionsPanel.SetActive(!FactionsPanel.activeSelf);
        });
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================