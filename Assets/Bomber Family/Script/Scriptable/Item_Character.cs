using UnityEngine;

[CreateAssetMenu(menuName = "Item/Character")]
public class Item_Character : Item
{
    [Header("Bomb Pooler")]
    [SerializeField] private Pooler bomb_Simple;
    [SerializeField] private Pooler bomb_Clock;
    [SerializeField] private Pooler bomb_Nucleer;
    [SerializeField] private Pooler bomb_Area;
    [SerializeField] private Pooler bomb_Anti_Wall;
    [SerializeField] private Pooler bomb_Searcher;

    public Pooler MyBombSimple { get { return bomb_Simple; } }
    public Pooler MyBombClock { get { return bomb_Clock; } }
    public Pooler MyBombNucleer { get { return bomb_Nucleer; } }
    public Pooler MyBombArea { get { return bomb_Area; } }
    public Pooler MyBombAnti_Wall { get { return bomb_Anti_Wall; } }
    public Pooler MyBombSearcher { get { return bomb_Searcher; } }
}