﻿using UnityEngine;

public class Trap_Freezer : Board_Object
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Character_Base character_Base))
        {
            character_Base.Freeze(true);
        }
    }
}