using UnityEngine;

public class Trap_Fire : Board_Object
{
    public override void OnStart()
    {
        Physics.IgnoreCollision(MyCollider, Player_Base.Instance.MyCollider);
    }
}