using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "new Tool Class", menuName = "Item/Tool")]
public class ToolClass : ItemClass
{
    [Header("Tools")] // Data Directed towards tools

    public ToolType toolType;
    public enum ToolType

    {
        sword,
        bow
    }
    public override ItemClass GetItem() { return this; }
    public override ToolClass GetTool() { return this; }
    public  override MiscClass GetMisc() { return null; }
}
