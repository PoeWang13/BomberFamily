using UnityEngine;

public enum WorldType
{
    Starting,
    Magical,
    Cowboy,
    Korsan,
    Samuray,
    Viking,
    Alien,
    Zombie,
    Monster,
    TarihÖncesi,
    OrtaÇağ,
    ModernTime,
    Elements,
    Robot,
    Underwater,
    Animal,
    Darkness, // Kurtadam, vampir, frenkestain
    KızılDerililer,
    Ankalar,
    Yamyamlar,
    BoşlukCanavarları,

}
[CreateAssetMenu(menuName = "Item/Character")]
public class Item_Character : Item
{
    [SerializeField] private WorldType myWorldType;
    [SerializeField] private string myName;

    public string MyName { get { return myName; } }
    public WorldType MyWorldType { get { return myWorldType; } }
}