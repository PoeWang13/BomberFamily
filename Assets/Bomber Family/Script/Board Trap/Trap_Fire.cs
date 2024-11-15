using UnityEngine;

public class Trap_Fire : Board_Object
{
    [SerializeField] private int damage = 5;
    [SerializeField] private float fireTime = 5;

    private bool isActive;
    private float fireTimeNext;
    private ParticleSystem fireTrail;

    public override void OnStart()
    {
        fireTrail = GetComponentInChildren<ParticleSystem>();
        Physics.IgnoreCollision(MyCollider, Player_Base.Instance.MyCollider);
    }
    private void Update()
    {
        fireTimeNext += Time.deltaTime;
        if (fireTimeNext > fireTime)
        {
            SetFire();
            fireTimeNext = 0;
        }
    }
    private void SetFire()
    {
        isActive = !isActive;
        if (isActive)
        {
            fireTrail.Play();
        }
        else
        {
            fireTrail.Stop();
        }
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