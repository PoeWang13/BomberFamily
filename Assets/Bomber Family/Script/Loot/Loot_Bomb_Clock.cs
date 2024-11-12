using DG.Tweening;
using UnityEngine;

public class Loot_Bomb_Clock : PoolObje
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
    public override void ObjeHavuzExit()
    {
        isTaked = false;
        transform.eulerAngles = Vector3.zero;
        base.ObjeHavuzExit();
    }
}