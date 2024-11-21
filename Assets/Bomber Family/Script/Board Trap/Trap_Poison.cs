using UnityEngine;

public class Trap_Poison : Trap_Has_Time_2
{
    [SerializeField] private int damage;

    private ParticleSystem poisonTrail;

    public override void OnStart()
    {
        base.OnStart();
        poisonTrail = GetComponentInChildren<ParticleSystem>();
    }
    public override void BehaviourChange(bool giveDamage)
    {
        base.BehaviourChange(giveDamage);
        if (giveDamage)
        {
            GiveDamage();
            poisonTrail.Play();
        }
        else
        {
            poisonTrail.Stop();
        }
    }
    public override void SetTrapForSpecial()
    {
        if (alwaysActivated || activeted)
        {
            poisonTrail.Play();
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