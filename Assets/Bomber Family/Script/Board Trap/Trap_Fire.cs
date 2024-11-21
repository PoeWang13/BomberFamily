using UnityEngine;

public class Trap_Fire : Trap_Has_Time_2
{
    [SerializeField] private int damage = 5;

    private ParticleSystem fireTrail;

    public override void OnStart()
    {
        base.OnStart();
        fireTrail = GetComponentInChildren<ParticleSystem>();
    }
    public override void BehaviourChange(bool giveDamage)
    {
        base.BehaviourChange(giveDamage);
        if (giveDamage)
        {
            GiveDamage();
            fireTrail.Play();
        }
        else
        {
            fireTrail.Stop();
        }
    }
    public override void SetTrapForSpecial()
    {
        if (alwaysActivated || activeted)
        {
            fireTrail.Play();
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