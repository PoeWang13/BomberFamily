using DG.Tweening;
using UnityEngine;

public class Loot_Gold : PoolObje
{
    private bool isTaked;

    private void Start()
    {
        transform.localScale = Vector3.one;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isTaked)
        {
            if (other.CompareTag("Player"))
            {
                isTaked = true;
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
    public override void ObjeHavuzExit()
    {
        isTaked = false;
        transform.localScale = Vector3.zero;
        base.ObjeHavuzExit();
    }
}