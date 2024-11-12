using DG.Tweening;
using UnityEngine;

public class Loot_Life : PoolObje
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
            if (other.TryGetComponent(out Player_Base player_Base))
            {
                isTaked = true;
                player_Base.Increaselife();
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