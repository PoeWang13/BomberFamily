using UnityEngine;

public class Trap_Freezer : Trap_Has_Time_2
{
    private ParticleSystem freezeTrail;

    public override void OnStart()
    {
        base.OnStart();
        freezeTrail = GetComponentInChildren<ParticleSystem>();
    }
    public override void BehaviourChange(bool giveDamage)
    {
        base.BehaviourChange(giveDamage);
        if (giveDamage)
        {
            GiveDeBuff();
            freezeTrail.Play();
        }
        else
        {
            freezeTrail.Stop();
        }
    }
    private void GiveDeBuff()
    {
        for (int e = 0; e < myCharacterList.Count; e++)
        {
            myCharacterList[e].Freeze(true);
        }
    }
    public override void SetTrapForSpecial()
    {
        if (alwaysActivated || activeted)
        {
            freezeTrail.Play();
        }
    }
    public override void CharacterEntered(Character_Base charBase)
    {
        charBase.Freeze(true);
    }
}