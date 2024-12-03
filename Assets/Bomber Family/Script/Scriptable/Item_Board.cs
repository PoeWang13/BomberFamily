using UnityEngine;

[CreateAssetMenu(menuName = "Item/Board")]
public class Item_Board : Item
{
    [SerializeField] private int myPrice;
    [SerializeField] private BoardType boardType;
    [SerializeField] private Pooler myPool;

    public int MyPrice { get { return myPrice; } }
    public BoardType MyBoardType { get { return boardType; } }
    public Pooler MyPool { get { return myPool; } }
}