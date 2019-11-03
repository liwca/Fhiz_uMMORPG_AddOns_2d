using System.Text;

public partial struct Item
{
    public string itemCategory
    {
        get { return data.itemCategory; }
    }

    [DevExtMethods("ToolTip")]
    private void ToolTip_UCE_NpcShop(StringBuilder tip)
    {
        tip.Replace("{ITEMCATEGORY}", itemCategory);
    }
}