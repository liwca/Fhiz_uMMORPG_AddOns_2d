using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class Player
{
    public void TakeAllLootItem()
    {
        CmdTakeLootGold();
        var items = target.inventory.Where(item => item.amount > 0).ToList();
        for (int i = 0; i < items.Count; ++i)
        {
            int itemIndex = target.inventory.FindIndex(item => item.amount > 0 && item.item.name == items[i].item.name);
            CmdTakeLootItem(itemIndex);
        }
    }
}