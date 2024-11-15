using UnityEngine;

public class Trap_Diken : Board_Object
{
    [SerializeField] private int damage;
    private bool activeted;
    private Trap_Trigger board_Trigger;
    private bool waitingTrigged;
    private bool canTrigged = true;
    private Animator myAnimator;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }
    public override void OnStart()
    {
        if (Game_Manager.Instance.GameType == GameType.MapCreate)
        {
            Map_Creater_Manager.Instance.OnTriggerTime += Instance_OnTriggerTime;
        }
        Physics.IgnoreCollision(MyCollider, Player_Base.Instance.MyCollider);
    }
    private void Instance_OnTriggerTime(object sender, System.EventArgs e)
    {
        if (canTrigged)
        {
            waitingTrigged = true;
            transform.localScale = Vector3.one * 2;
        }
    }
    public override void SetMouseButton()
    {
        Map_Creater_Manager.Instance.ChooseStuckObject(gameObject);
        if (waitingTrigged)
        {
            canTrigged = false;
            waitingTrigged = false;
            transform.localScale = Vector3.one;
            Map_Creater_Manager.Instance.SetObjectForTrigger(this);
            // Creator Menu aç
            Canvas_Manager.Instance.SetCreatorPanel(true);
        }
    }
    public void SetDiken()
    {
        activeted = !activeted;
        // Diken görselini aktifleştir
        myAnimator.SetBool("Up", activeted);
    }
    public void SetDiken(bool active, Trap_Trigger trigger)
    {
        activeted = active;
        board_Trigger = trigger;
        // Diken görselini aktifleştir
        myAnimator.SetBool("Up", active);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!Game_Manager.Instance.LevelStart)
        {
            return;
        }
        if (other.CompareTag("Player"))
        {
            Debug.LogWarning("gameFinish", other.gameObject);
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