using UnityEngine;

public class Trap_Base : Board_Object
{
    [SerializeField] private TrapType myTrapType;

    public TrapType MyTrapType { get { return myTrapType; } }

    public virtual void SetTrap()
    {

    }
}