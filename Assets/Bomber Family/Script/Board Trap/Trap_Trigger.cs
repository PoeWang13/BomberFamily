using UnityEngine;
using System.Collections.Generic;

public class Trap_Trigger : Board_Object
{
    private List<Trap_Diken> myDikens = new List<Trap_Diken>();
    public override void SetMouseButton()
    {
        if (IsStuck)
        {
            Map_Creater_Manager.Instance.SetTriggerObject(this);
            Canvas_Manager.Instance.SetCreatorPanel(false);
        }
    }
    public void AddDiken(Trap_Diken diken)
    {
        myDikens.Add(diken);
    }
    public void RemoveDiken(Trap_Diken diken)
    {
        myDikens.Remove(diken);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myDikens.ForEach(d => d.SetDiken());
            Game_Manager.Instance.AddActiveTrapAmount();
        }
    }
}