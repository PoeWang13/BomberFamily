using DG.Tweening;
using UnityEngine;

public class Loot_Life : Loot_Base
{
    private void OnTriggerEnter(Collider other)
    {
        if (!IsTaked)
        {
            if (other.TryGetComponent(out Player_Base player_Base))
            {
                SetTaked(true);
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
}