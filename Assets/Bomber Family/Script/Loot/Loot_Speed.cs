using UnityEngine;
using DG.Tweening;

public class Loot_Speed : PoolObje
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
            if (other.TryGetComponent(out Player_Base player_Base))
            {
                isTaked = true;
                lootEffect.SetActive(false);
                player_Base.IncreaseSpeed();
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