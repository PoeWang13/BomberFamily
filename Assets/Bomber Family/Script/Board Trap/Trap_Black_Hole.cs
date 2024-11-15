using DG.Tweening;
using UnityEngine;

public class Trap_Black_Hole : Board_Object
{
    public override void OnStart()
    {
        Physics.IgnoreCollision(MyCollider, Player_Base.Instance.MyCollider);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Game_Manager.Instance.GameType == GameType.Game)
            {
            }
            else if (Game_Manager.Instance.GameType == GameType.MapCreate)
            {
                Warning_Manager.Instance.ShowMessage("You are DEAD..", 3);
            }
            Player_Base.Instance.SetScale(false);
            Canvas_Manager.Instance.GameLost();
        }
        else if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Collider>().enabled = false;
            other.transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => other.GetComponent<Enemy_Base>().EnterHavuz());
        }
    }
}