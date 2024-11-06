using UnityEngine;
using System.Collections.Generic;

public class Board_Trigger : Board_Object
{
    private List<Board_Diken> myDikens = new List<Board_Diken>();
    public override void SetMouseButton()
    {
        if (IsStuck)
        {
            Map_Creater_Manager.Instance.SetTriggerObject(this);
            Canvas_Manager.Instance.SetCreatorPanel(false);
        }
    }
    public void AddDiken(Board_Diken diken)
    {
        myDikens.Add(diken);
    }
    public void RemoveDiken(Board_Diken diken)
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