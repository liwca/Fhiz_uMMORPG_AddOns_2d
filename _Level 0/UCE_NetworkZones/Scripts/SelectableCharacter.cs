// small helper script that is added to character selection previews at runtime
using UnityEngine;
using Mirror;

public class SelectableCharacter : MonoBehaviour
{
    // index will be set by networkmanager when creating this script
    public int index = -1;

    public NetworkManagerMMO manager;

    private void OnMouseDown()
    {
        // set selection index
        manager.selection = index;

        // show selection indicator for better feedback
        GetComponent<Player>().SetIndicatorViaParent(transform);
    }

    private void Update()
    {
        // remove indicator if not selected anymore
        if (manager && manager.selection != index)
        {
            Player player = GetComponent<Player>();
            if (player.indicator != null)
                Destroy(player.indicator);
        }
    }
}