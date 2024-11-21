using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Item/Player Source")]
public class Player_Source : Item_Character
{
    [SerializeField] private Pooler myBombPooler;
    [SerializeField] private CharacterStat myUpgrade;
    [SerializeField] private CharacterStat myMaxUpgrade;
    [SerializeField] private List<Sprite> myBombFire = new List<Sprite>();

    private int spriteOrder;

    public Pooler MyBombPooler { get { return myBombPooler; } }
    public CharacterStat MyUpgrade { get { return myUpgrade; } }
    public CharacterStat MyMaxUpgrade { get { return myMaxUpgrade; } }

    public void SetSpriteOrder()
    {
        spriteOrder = 0;
    }
    public Sprite SetImageBombFireLimit(int next)
    {
        spriteOrder += next;
        if (spriteOrder == -1)
        {
            spriteOrder = myBombFire.Count - 1;
        }
        if (spriteOrder == myBombFire.Count)
        {
            spriteOrder = 0;
        }
        return myBombFire[spriteOrder];
    }
    public bool CanUpgradeLife(int life)
    {
        return life < myMaxUpgrade.myLife;
    }
    public bool CanUpgradeSpeed(int speed)
    {
        return speed < myMaxUpgrade.mySpeed;
    }
    public bool CanUpgradeBombAmount(int amount)
    {
        return amount < myMaxUpgrade.myBombAmount;
    }
    public bool CanUpgradeBombPower(int power)
    {
        return power < myMaxUpgrade.myBombPower;
    }
    public bool CanUpgradeBombFireLimit(int limit)
    {
        return limit < myMaxUpgrade.myBombFireLimit;
    }
    public bool CanUpgradeBoxPassing(int pass)
    {
        return pass < myMaxUpgrade.mySpeed;
    }
    public bool CanUpgradeBoxPushingTime(float time)
    {
        return time > myMaxUpgrade.myBombPushingTime;
    }
}