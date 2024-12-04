using DG.Tweening;
using UnityEngine;

public class Loot_Bomb_Simple : Loot_Base
{
    [SerializeField] private BombType myBombType;

    private void OnTriggerEnter(Collider other)
    {
        if (!IsTaked)
        {
            if (other.TryGetComponent(out Player_Base player_Base))
            {
                SetTaked(true);
                player_Base.IncreaseBombAmount();
                transform.DOScale(Vector3.one * 1.5f, 0.1f).OnComplete(() =>
                {
                    transform.DOScale(Vector3.one * 0.5f, 0.1f).OnComplete(() =>
                    {
                        transform.DOScale(Vector3.one * 1.5f, 0.1f).OnComplete(() =>
                        {
                            transform.DOScale(Vector3.zero, 0.1f).OnComplete(() =>
                            {
                                EnterHavuz();
                            });
                        });
                    });
                });
            }
        }
    }
}