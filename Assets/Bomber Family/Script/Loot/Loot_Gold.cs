using DG.Tweening;
using UnityEngine;

public class Loot_Gold : PoolObje
{
    private bool isTaked;
    private GameObject lootEffect;

    private void Awake()
    {
        transform.localScale = Vector3.one;
        lootEffect = transform.Find("Loot_Effect").gameObject;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isTaked)
        {
            if (other.CompareTag("Player"))
            {
                isTaked = true;
                lootEffect.SetActive(false);
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
        lootEffect.SetActive(true);
        transform.localScale = Vector3.zero;
        base.ObjeHavuzExit();
    }
}