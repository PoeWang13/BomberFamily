using DG.Tweening;
using UnityEngine;

public class Loot_Bomb_Area : PoolObje
{
    [SerializeField] private BombType myBombType;

    private bool isTaked;

    private void OnTriggerEnter(Collider other)
    {
        if (!isTaked)
        {
            if (other.TryGetComponent(out Player_Base player_Base))
            {
                isTaked = true;
                player_Base.IncreaseBombAmount(myBombType);
                transform.DOLocalMoveY(1, 0.2f).OnComplete(() =>
                {
                    transform.DOLocalMoveY(0, 0.1f).OnComplete(() =>
                    {
                        transform.DOLocalMoveY(3, 0.2f).OnComplete(() =>
                        {
                            transform.DOLocalMoveY(0, 0.1f).OnComplete(() =>
                            {
                                transform.DOScale(Vector3.one * 3.0f, 0.2f).OnComplete(() =>
                                {
                                    transform.DOScale(Vector3.zero, 0.1f).OnComplete(() =>
                                    {
                                        EnterHavuz();
                                    });
                                });
                            });
                        });
                    });
                });
            }
        }
    }
    public override void ObjeHavuzExit()
    {
        isTaked = false;
        transform.localScale = Vector3.zero;
        transform.localPosition = Vector3.zero;
        base.ObjeHavuzExit();
    }
}