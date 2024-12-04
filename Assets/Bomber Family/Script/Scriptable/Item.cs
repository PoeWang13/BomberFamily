using UnityEngine;

[CreateAssetMenu(menuName = "Item/Item")]
public class Item : ScriptableObject
{
    [SerializeField] private Sprite myIcon;

    public Sprite MyIcon { get { return myIcon; } }
    public void SetSprite(Sprite sprite)
    {
        myIcon = sprite;
    }
}