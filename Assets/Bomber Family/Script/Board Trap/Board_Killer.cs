using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class Board_Killer : Board_Object
{
    [SerializeField] private float sleepTime = 5;

    private bool sleeping;
    private Animator myAnimator;
    private float sleepTimeNext;

    public override void OnStart()
    {
        myAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (!Game_Manager.Instance.LevelStart)
        {
            return;
        }
        if (!sleeping)
        {
            return;
        }
        sleepTimeNext += Time.deltaTime;
        if (sleepTimeNext > sleepTime)
        {
            sleepTimeNext = 0;
            sleeping = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (sleeping)
        {
            return;
        }
        if (other.CompareTag("Player"))
        {
            KillerActivited();
            Game_Manager.Instance.AddCaughtTrapAmount();
            Character_Base character_Base = other.GetComponent<Character_Base>(); 
            Game_Manager.Instance.AddLoseLifeAmount(character_Base.MyLife);
            SendBossOutSide(character_Base);
        }
        else if (other.CompareTag("Enemy"))
        {
            KillerActivited();
            SendBossOutSide(other.GetComponent<Character_Base>());
        }
    }
    private void KillerActivited()
    {
        sleeping = true;
        //myAnimator.SetTrigger("Killer");
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