using UnityEngine;

public class Trap_Saw : Board_Object
{
    public override void OnStart()
    {
        Physics.IgnoreCollision(MyCollider, Player_Base.Instance.MyCollider);
    }
}