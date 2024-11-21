using UnityEngine;

public class Trap_Cutter : Trap_Has_Time_2
{

    [SerializeField] private int damage = 5;

    private Animator myAnimator;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }
    public override void BehaviourChange(bool giveDamage)
    {
        base.BehaviourChange(giveDamage);
        myAnimator.SetBool("Activate", activeted);
        if (giveDamage)
        {
            GiveDamage();
        }
    }
    private void GiveDamage()
    {
        for (int e = 0; e < myCharacterList.Count; e++)
        {
            CharacterEntered(myCharacterList[e]);
        }
    }
    public override void SetTrap()
    {
        base.SetTrap();
        myAnimator.SetBool("Activate", activeted);
        myAnimator.SetBool("AlwaysActivate", alwaysActivated);
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