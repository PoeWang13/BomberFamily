using UnityEngine;

public class Trap_Cutter : Board_Object
{
    [SerializeField] private int damage = 5;

    public override void OnStart()
    {
        Physics.IgnoreCollision(MyCollider, Player_Base.Instance.MyCollider);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!Game_Manager.Instance.LevelStart)
        {
            return;
        }
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