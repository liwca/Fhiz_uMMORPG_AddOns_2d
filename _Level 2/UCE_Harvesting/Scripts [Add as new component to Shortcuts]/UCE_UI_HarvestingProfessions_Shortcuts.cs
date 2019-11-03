using UnityEngine;
using UnityEngine.UI;

// =======================================================================================
// SHORTCUTS
// =======================================================================================
public partial class UCE_UI_HarvestingProfessions_Shortcuts : MonoBehaviour
{
    public Button HarvestingProfessionsButton;
    public GameObject HarvestingProfessionsPanel;

    // -----------------------------------------------------------------------------------
    // Update
    // -----------------------------------------------------------------------------------
    public void Update()
    {
        HarvestingProfessionsButton.onClick.SetListener(() =>
        {
            HarvestingProfessionsPanel.SetActive(!HarvestingProfessionsPanel.activeSelf);
        });
    }

    // -----------------------------------------------------------------------------------
}

// =======================================================================================