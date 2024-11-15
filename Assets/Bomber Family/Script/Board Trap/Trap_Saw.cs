using UnityEngine;

public class Trap_Saw : Board_Object
{
    [SerializeField] private int damage = 5;
    [SerializeField] private float sawTime = 5;

    private bool isActive;
    private float sawTimeNext;
    private Animator myAnimator;

    public override void OnStart()
    {
        myAnimator = GetComponent<Animator>();
        Physics.IgnoreCollision(MyCollider, Player_Base.Instance.MyCollider);
    }
    private void Update()
    {
        sawTimeNext += Time.deltaTime;
        if (sawTimeNext > sawTime)
        {
            SetSaw();
            sawTimeNext = 0;
        }
    }
    private void SetSaw()
    {
        isActive = !myAnimator.GetBool("Saw");
        myAnimator.SetBool("Saw", isActive);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!Game_Manager.Instance.LevelStart)
        {
            return;
        }
        if (isActive)
        {
            if (other.CompareTag("Player"))
            {
                Game_Manager.Instance.AddCaughtTrapAmount();
                Game_Manager.Instance.AddLoseLifeAmount(damage);
                other.GetComponent<Player_Base>().TakeDamage(damage);
            }
            else if (other.CompareTag("Enemy"))
            {
                other.GetComponent<Enemy_Base>().TakeDamage(damage);
            }
        }
    }
}