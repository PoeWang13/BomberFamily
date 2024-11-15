using UnityEngine;

public class Trap_Freezer : Board_Object
{
    public override void OnStart()
    {
        Physics.IgnoreCollision(MyCollider, Player_Base.Instance.MyCollider);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Character_Base character_Base))
        {
            character_Base.Freeze(true);
        }
    }
}