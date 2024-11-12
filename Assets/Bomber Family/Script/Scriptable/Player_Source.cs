using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Item/Player Source")]
public class Player_Source : Item_Character
{
    [SerializeField] private Pooler myBombPooler;
    [SerializeField] private CharacterStat myUpgrade;
    [SerializeField] private List<Sprite> myBombFire = new List<Sprite>();

    private int spriteOrder;

    public Pooler MyBombPooler { get { return myBombPooler; } }
    public CharacterStat MyUpgrade { get { return myUpgrade; } }

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
}