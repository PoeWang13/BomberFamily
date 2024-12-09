﻿using UnityEngine;

public enum WorldType
{
    Starting,
    Magical,
    Cowboy,
    Korsan,
    Samuray,
    Viking,
    Dead,
    Orc,
    Jungle,
    Mısır,
    Yunan,
    İskandinav,
    China,
    Aztek,
    Steam,
    Alien,
    Virüs,
    Zombie,
    Devil,
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
public class Item_Character : Item_Board
{
    [SerializeField] private WorldType myWorldType;
    [SerializeField] private string myName;

    public string MyName { get { return myName; } }
    public WorldType MyWorldType { get { return myWorldType; } }
}