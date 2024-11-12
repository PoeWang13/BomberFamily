using DG.Tweening;
using UnityEngine;

public class Trap_Black_Hole : Board_Object
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Game_Manager.Instance.GameType == GameType.Game)
            {
                Warning_Manager.Instance.ShowMessage("You are DEAD..", 3);
            }
            else if (Game_Manager.Instance.GameType == GameType.MapCreate)
            {
                Player_Base.Instance.SetScale(false);
                Canvas_Manager.Instance.GameLost();
            }
        }
        else if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Collider>().enabled = false;
            other.transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => other.GetComponent<Enemy_Base>().EnterHavuz());
        }
    }
}