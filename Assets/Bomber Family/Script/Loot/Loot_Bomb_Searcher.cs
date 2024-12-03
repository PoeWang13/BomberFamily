using DG.Tweening;
using UnityEngine;

public class Loot_Bomb_Searcher : PoolObje
{
    [SerializeField] private BombType myBombType;

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
                Save_Load_Manager.Instance.gameData.allBombAmount[(int)myBombType].bombAmount++;
                Canvas_Manager.Instance.SetBomb(myBombType);
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
    public override void ObjeHavuzExit()
    {
        isTaked = false;
        lootEffect.SetActive(true);
        transform.localScale = Vector3.zero;
        transform.eulerAngles = Vector3.zero;
        transform.localPosition = Vector3.zero;
        base.ObjeHavuzExit();
    }
}