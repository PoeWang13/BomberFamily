using UnityEngine;

[CreateAssetMenu(menuName = "Item/Character")]
public class Item_Character : Item
{
    [SerializeField] private BombType myBombType;
    [SerializeField] private string myName;

    public string MyName { get { return myName; } }
    public BombType MyBombType { get { return myBombType; } }
    public string MyBombingType { get { return myBombType.ToString() + " Bomb"; } }
}