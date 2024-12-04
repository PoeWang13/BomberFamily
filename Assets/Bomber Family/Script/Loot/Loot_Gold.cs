using DG.Tweening;
using UnityEngine;

public class Loot_Gold : Loot_Base
{
    private void OnTriggerEnter(Collider other)
    {
        if (!IsTaked)
        {
            if (other.CompareTag("Player"))
            {
                SetTaked(true);
                int earnGold = Random.Range(2, 20);
                Canvas_Manager.Instance.SetGoldSmooth(earnGold);
                Game_Manager.Instance.AddGoldAmount(earnGold);
                transform.DOScale(Vector3.one * 1.5f, 0.1f).OnComplete(() =>
                {
                    transform.DOScale(Vector3.zero, 0.1f).OnComplete(() =>
                    {
                        EnterHavuz();
                    });
                });
            }
        }
    }
}