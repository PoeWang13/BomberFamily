using UnityEngine;

public enum TrapType
{
    None = 0,
    Hole = 1,

    Trigger = 99,

    Home = 100,
    Killer = 101,

    Cutter = 1000,
    Diken = 1001,
    Fire = 1002,
    Freeze = 1003,
    Lazer = 1004,
    Poison = 1005,
    Saw = 1006,
    Slower = 1007,
}
[CreateAssetMenu(menuName = "Item/Item Trap")]
public class Item_Trap : Item_Board
{
    [SerializeField] private TrapType myTrapType;

    public TrapType MyTrapType { get { return myTrapType; } }
}