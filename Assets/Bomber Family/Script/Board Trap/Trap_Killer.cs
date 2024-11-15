using DG.Tweening;
using UnityEngine;

public class Trap_Killer : Board_Object
{
    [SerializeField] private float sleepTime = 5;

    private bool isActive;
    private Animator myAnimator;
    private float sleepTimeNext;

    public override void OnStart()
    {
        myAnimator = GetComponent<Animator>();
        Physics.IgnoreCollision(MyCollider, Player_Base.Instance.MyCollider);
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
        sleepTimeNext += Time.deltaTime;
        if (sleepTimeNext > sleepTime)
        {
            sleepTimeNext = 0;
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