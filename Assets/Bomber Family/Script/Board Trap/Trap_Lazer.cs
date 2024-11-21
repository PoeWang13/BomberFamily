using UnityEngine;
using System.Collections.Generic;

public class Trap_Lazer : Trap_Has_Time_2
{
    [SerializeField] private int damage;
    [SerializeField] private List<GameObject> allLazerGameObject = new List<GameObject>();
    [SerializeField] private List<Collider> allLazerCollider = new List<Collider>();

    public override void BehaviourChange(bool giveDamage)
    {
        base.BehaviourChange(giveDamage);
        allLazerCollider.ForEach(l => l.enabled = giveDamage);
        allLazerGameObject.ForEach(l => l.SetActive(giveDamage));
        if (giveDamage)
        {
            GiveDamage();
        }
    }
    public override void SetTrapForSpecial()
    {
        if (alwaysActivated || activeted)
        {
            allLazerCollider.ForEach(l => l.enabled = true);
            allLazerGameObject.ForEach(l => l.SetActive(true));
        }
    }
    private void GiveDamage()
    {
        for (int e = 0; e < myCharacterList.Count; e++)
        {
            CharacterEntered(myCharacterList[e]);
        }
    }
    public override void CharacterEntered(Character_Base charBase)
    {
        if (charBase.CompareTag("Player"))
        {
            Game_Manager.Instance.AddCaughtTrapAmount();
            Game_Manager.Instance.AddLoseLifeAmount(damage);
        }
        charBase.TakeDamage(damage);
    }
}