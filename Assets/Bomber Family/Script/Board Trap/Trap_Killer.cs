using DG.Tweening;
using UnityEngine;

public class Trap_Killer : Trap_Has_Time_1
{
    private bool isActive;
    private Animator myAnimator;

    public override void OnStart()
    {
        myAnimator = GetComponent<Animator>();
        if (MyNotTriggeredCollider != null)
        {
            Physics.IgnoreCollision(MyNotTriggeredCollider, Player_Base.Instance.MyNotTriggeredCollider);
        }
    }
    private void Update()
    {
        if (!Game_Manager.Instance.LevelStart)
        {
            return;
        }
        if (!isActive)
        {
            return;
        }
        waitingTimeNext += Time.deltaTime;
        if (waitingTimeNext > waitingTime)
        {
            waitingTimeNext = 0;
            isActive = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isActive)
        {
            return;
        }
        if (other.CompareTag("Player"))
        {
            KillerActivited();
            Character_Base character_Base = other.GetComponent<Character_Base>();
            Game_Manager.Instance.AddLoseLifeAmount(character_Base.MyLife);
            Game_Manager.Instance.AddCaughtTrapAmount();
            Canvas_Manager.Instance.GameLost();

            //SendBossOutSide(character_Base);
        }
        else if (other.CompareTag("Enemy"))
        {
            KillerActivited();
            Character_Base character_Base = other.GetComponent<Character_Base>();
            character_Base.TakeDamage(character_Base.MyLife);
            //SendBossOutSide(other.GetComponent<Character_Base>());
        }
    }
    private void KillerActivited()
    {
        isActive = true;
        myAnimator.SetBool("Kill", true);
    }
    private void SendBossOutSide(Character_Base character_Base)
    {
        // Avı kendine doğru çek
        character_Base.transform.DOMove(new Vector3(MyCoor.x, 0, MyCoor.y), 1.0f).OnComplete(() =>
        {
            // Avı küçülterek erit
            character_Base.transform.DOMoveY(-5, 1.0f).OnComplete(() =>
            {
                // Bossu serbest bırak
                character_Base.TakeDamage(character_Base.MyLife);
            });
        });
    }
}