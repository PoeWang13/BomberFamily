using DG.Tweening;
using UnityEngine;

public class Loot_Bomb_Clock : Loot_Base
{
    [SerializeField] private BombType myBombType;

    private void OnTriggerEnter(Collider other)
    {
        if (!IsTaked)
        {
            if (other.TryGetComponent(out Player_Base player_Base))
            {
                SetTaked(true);
                LootEffect.SetActive(false);
                Save_Load_Manager.Instance.gameData.allBombAmount[(int)myBombType].bombAmount++;
                Canvas_Manager.Instance.SetBomb(myBombType);
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