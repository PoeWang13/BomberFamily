using UnityEngine;

[CreateAssetMenu(menuName = "Item/Material")]
public class Item_Material : Item
{
    [SerializeField] private string myName;
    [SerializeField] private int myMax;
    [SerializeField] private InventoryType myInventoryType;

    public int MyMax { get { return myMax; } }
    public string MyName { get { return myName; } }
    public InventoryType MyInventoryType { get { return myInventoryType; } }
}