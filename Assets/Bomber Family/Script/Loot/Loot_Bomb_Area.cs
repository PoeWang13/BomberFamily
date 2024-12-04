using DG.Tweening;
using UnityEngine;

public class Loot_Bomb_Area : Loot_Base
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
}