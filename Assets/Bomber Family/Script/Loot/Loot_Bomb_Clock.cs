using DG.Tweening;
using UnityEngine;

public class Loot_Bomb_Clock : Loot_Bomb
{
    private void OnTriggerEnter(Collider other)
    {
        if (!IsTaked)
        {
            if (other.TryGetComponent(out Player_Base player_Base))
            {
                SetTaked(true);
                LootEffect.SetActive(false);
                AddBomb();
                transform.DOLocalRotate(Vector3.forward * 30, 0.1f).OnComplete(() =>
                {
                    transform.DOLocalRotate(Vector3.back * 30, 0.1f).OnComplete(() =>
                    {
                        transform.DOLocalRotate(Vector3.forward * 60, 0.1f).OnComplete(() =>
                        {
                            transform.DOLocalRotate(Vector3.back * 60, 0.1f).OnComplete(() =>
                            {
                                transform.DOLocalRotate(Vector3.forward * 0, 0.1f).OnComplete(() =>
                                {
                                    EnterHavuz();
                                });
                            });
                        });
                    });
                });
            }
        }
    }
}