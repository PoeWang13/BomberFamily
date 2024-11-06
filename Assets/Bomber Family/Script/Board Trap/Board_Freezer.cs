using UnityEngine;

public class Board_Freezer : Board_Object
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Character_Base character_Base))
        {
            character_Base.Freeze(true);
        }
    }
}