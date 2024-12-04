using UnityEngine;

[CreateAssetMenu(fileName = "Source_", menuName = "Item/Source")]
public class Item_Source : Item
{
    [SerializeField] private string myName;
    [SerializeField] private int myMax;
    [SerializeField] private InventoryType myInventoryType;

    public int MyMax { get { return myMax; } }
    public string MyName { get { return myName; } }
}