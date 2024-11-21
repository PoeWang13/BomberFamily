using UnityEngine;

public class Trap_Slower : Trap_Has_Time_2
{
    [SerializeField, Range(0, 1)] private float slowerPercent;

    public override void BehaviourChange(bool giveDamage)
    {
        base.BehaviourChange(giveDamage);
        if (giveDamage)
        {
            GiveDebuff();
        }
    }
    private void GiveDebuff()
    {
        for (int e = 0; e < myCharacterList.Count; e++)
        {
            myCharacterList[e].DebuffMySpeed(slowerPercent);
        }
    }
    public override void SetTrapForSpecial()
    {

    }
    public override void CharacterEntered(Character_Base charBase)
    {
        charBase.DebuffMySpeed(slowerPercent);
    }
    private void OnTriggerExit(Collider other)
    {
        if (!Game_Manager.Instance.LevelStart)
        {
            return;
        }
        if (other.transform.TryGetComponent(out Character_Base charBase))
        {
            if (myCharacterList.Contains(charBase))
            {
                charBase.ResetSpeed();
                myCharacterList.Remove(charBase);
            }
        }
    }
}