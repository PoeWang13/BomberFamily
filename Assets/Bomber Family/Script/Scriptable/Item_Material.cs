using UnityEngine;

[CreateAssetMenu(menuName = "Item/Material")]
public class Item_Material : Item
{
    [SerializeField] private string myName;
    [SerializeField] private int myMax;

    public string MyName { get { return myName; } }
    public int MyMax { get { return myMax; } }
}