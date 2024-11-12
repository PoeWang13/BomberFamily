using UnityEngine;

[CreateAssetMenu(menuName = "Item/Item")]
public class Item : ScriptableObject
{
    [SerializeField] private Sprite myIcon;
    [SerializeField] private int myPrice;
    [SerializeField] private BoardType boardType;
    [SerializeField] private Pooler myPool;

    public Sprite MyIcon { get { return myIcon; } }
    public int MyPrice { get { return myPrice; } }
    public BoardType MyBoardType { get { return boardType; } }
    public Pooler MyPool { get { return myPool; } }
}