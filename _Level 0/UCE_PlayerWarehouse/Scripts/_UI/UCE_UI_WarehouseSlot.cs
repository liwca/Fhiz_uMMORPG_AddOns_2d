// warehouse addon for asset uMMORPG
// version: 1.3.3
// author: Janek Todoruk | https://bitbucket.org/janektodoruk/ummorpg-addon-warehouse

// Attach to the prefab for easier component access by the UI Scripts.
// Otherwise we would need slot.GetChild(0).GetComponentInChildren<Text> etc.
using UnityEngine;
using UnityEngine.UI;

public class UCE_UI_WarehouseSlot : MonoBehaviour
{
    public UIShowToolTip tooltip;
    public Button button;
    public UIDragAndDropable dragAndDropable;
    public Image image;
    public GameObject amountOverlay;
    public Text amountText;
}