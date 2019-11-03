// =======================================================================================
// Created and maintained by Fhiz
// Usable for both personal and commercial projects, but no sharing or re-sale
// * Discord Support Server.............: https://discord.gg/YkMbDHs
// * Public downloads website...........: https://www.indie-mmo.net
// * Pledge on Patreon for VIP AddOns...: https://www.patreon.com/IndieMMO
// =======================================================================================
using Mirror;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// =======================================================================================
//
// =======================================================================================
public partial class UCE_UI_CharacterCreation : MonoBehaviour
{
    [Header("-=-=-=- UCE Character Creation -=-=-=-")]
    public GameObject panel;

#if _iMMOTRAITS
    public UCE_UI_CharacterTraits traitsPanel;
#endif

    public List<UCE_CharacterCreationClass> classList = new List<UCE_CharacterCreationClass>();
    public bool lookAtCamera;
    public GameObject SpawnPoint;
    public NetworkManagerMMO manager;

    public Transform creationCameraLocation;

    [Header("-=-=-=- Creation Panel -=-=-=-")]
    public InputField nameInput;
    public Button createButton;
    public Button cancelButton;

    protected List<Player> players;
    protected int classIndex = 0;
    protected bool bInit = false;

    // -----------------------------------------------------------------------------------
    // currentPlayer
    // -----------------------------------------------------------------------------------
    public Player currentPlayer
    {
        get
        {
            return players[classIndex];
        }
    }

    // -----------------------------------------------------------------------------------
    // Show
    // -----------------------------------------------------------------------------------
    public void Show()
    {
        Camera.main.transform.position = creationCameraLocation.position;
        Camera.main.transform.rotation = creationCameraLocation.rotation;

        players = new List<Player>();
        players = manager.GetPlayerClasses();

        if (players == null || players.Count <= 0)
        {
            return;
        }

        for (int c = 0; c < players.Count; c++)
        {
            int temp = c;

            classList[temp].button.onClick.SetListener(() => SetCharacterClass(temp));
            classList[temp].label.text = players[temp].name;
            classList[temp].prefabID = temp;

#if _iMMOUNLOCKABLECLASSES
            if (manager.UCE_HasUnlockedClass(players[temp]))
            {
                classList[temp].button.gameObject.SetActive(true);
            }
            else
            {
                classList[temp].button.gameObject.SetActive(false);
            }
#else
			classList[temp].button.gameObject.SetActive(true);
#endif
        }

#if _iMMOUNLOCKABLECLASSES
        for (int c = 0; c < players.Count; c++)
        {
            int selectedClass = c;
            if (manager.UCE_HasUnlockedClass(players[selectedClass]))
            {
                SetCharacterClass(selectedClass);
                break;
            }
        }
#else
		SetCharacterClass(0);
#endif

        createButton.onClick.SetListener(() =>
        {
            CreateCharacter();
        });

        cancelButton.onClick.SetListener(() =>
        {
            Hide();
        });

        panel.SetActive(true);

        bInit = true;
    }

    // -----------------------------------------------------------------------------------
    // Update
    // -----------------------------------------------------------------------------------
    private void Update()
    {
        if (!bInit) return;

        if (nameInput.text.Length == 0)
            createButton.enabled = false;
        else
            createButton.enabled = true;
    }

    // -----------------------------------------------------------------------------------
    // CreateCharacter
    // -----------------------------------------------------------------------------------
    public void CreateCharacter()
    {
        if (SpawnPoint.transform.childCount > 0)
            Destroy(SpawnPoint.transform.GetChild(0).gameObject);

#if _iMMOTRAITS
        int[] iTraits = new int[traitsPanel.currentTraits.Count];

        for (int i = 0; i < traitsPanel.currentTraits.Count; i++)
        {
            iTraits[i] = traitsPanel.currentTraits[i].name.GetStableHashCode();
        }

        CharacterCreateMsg message = new CharacterCreateMsg
        {
            name = nameInput.text,
            classIndex = classIndex,
            traits = iTraits
        };
#else
		CharacterCreateMsg message = new CharacterCreateMsg
        {
            name 		= nameInput.text,
            classIndex 	= classIndex
        };

#endif

        NetworkClient.Send(message);

        Hide();
    }

    // -----------------------------------------------------------------------------------
    //
    // -----------------------------------------------------------------------------------
    public void SetCharacterClass(int _classIndex)
    {
        classIndex = _classIndex;

        if (SpawnPoint.transform.childCount > 0)
            Destroy(SpawnPoint.transform.GetChild(0).gameObject);

        GameObject go = Instantiate(players[classIndex].gameObject, SpawnPoint.transform.position, SpawnPoint.transform.rotation);

        go.transform.parent = SpawnPoint.transform;

        if (lookAtCamera)
            go.transform.LookAt(creationCameraLocation);

        Player player = go.GetComponent<Player>();

        for (int i = 0; i < players[classIndex].equipmentInfo.Length; ++i)
        {
            EquipmentInfo info = players[classIndex].equipmentInfo[i];
            player.equipment.Add(info.defaultItem.item != null ? new ItemSlot(new Item(info.defaultItem.item), info.defaultItem.amount) : new ItemSlot());
            player.RefreshLocation(i);
        }

#if _iMMOTRAITS
        traitsPanel.Show();
#endif
    }

    // -----------------------------------------------------------------------------------
    // Hide
    // -----------------------------------------------------------------------------------
    public void Hide()
    {
        if (SpawnPoint.transform.childCount > 0)
            Destroy(SpawnPoint.transform.GetChild(0).gameObject);

#if _iMMOTRAITS
        traitsPanel.Hide();
#endif

        panel.SetActive(false);
        bInit = false;

        Camera.main.transform.position = manager.selectionCameraLocation.position;
        Camera.main.transform.rotation = manager.selectionCameraLocation.rotation;
    }

    public bool IsVisible()
    {
        return panel.activeSelf;
    }

    // -----------------------------------------------------------------------------------
}