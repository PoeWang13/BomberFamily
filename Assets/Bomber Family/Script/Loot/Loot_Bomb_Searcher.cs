using DG.Tweening;
using UnityEngine;

public class Loot_Bomb_Searcher : Loot_Bomb
{
    private void OnTriggerEnter(Collider other)
    {
        if (!IsTaked)
        {
            if (other.TryGetComponent(out Player_Base player_Base))
            {
                SetTaked(true);
                AddBomb();
                transform.DOLocalMoveY(1, 0.1f).OnComplete(() =>
                {
                    transform.DOLocalRotate(Vector3.right * 180, 0.1f).OnComplete(() =>
                    {
                        transform.DOLocalRotate(Vector3.right * 360, 0.1f).OnComplete(() =>
                        {
                            transform.DOLocalMoveY(0, 0.1f);
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