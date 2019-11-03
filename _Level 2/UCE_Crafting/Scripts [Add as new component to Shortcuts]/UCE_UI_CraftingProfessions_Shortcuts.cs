using UnityEngine;
using UnityEngine.UI;

// =======================================================================================
// SHORTCUTS
// =======================================================================================
public partial class UCE_UI_CraftingProfessions_Shortcuts : MonoBehaviour
{
    public Button CraftingProfessionsButton;
    public GameObject CraftingProfessionsPanel;

    // -----------------------------------------------------------------------------------
    // Update
    // -----------------------------------------------------------------------------------
    public void Update()
    {
        CraftingProfessionsButton.onClick.SetListener(() =>
        {
            CraftingProfessionsPanel.SetActive(!CraftingProfessionsPanel.activeSelf);
        });
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================