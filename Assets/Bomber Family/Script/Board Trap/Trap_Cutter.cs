using UnityEngine;

public class Trap_Cutter : Board_Object
{
    public override void OnStart()
    {
        Physics.IgnoreCollision(MyCollider, Player_Base.Instance.MyCollider);
    }
}