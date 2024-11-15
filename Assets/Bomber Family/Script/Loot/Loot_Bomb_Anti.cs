using DG.Tweening;
using UnityEngine;

public class Loot_Bomb_Anti : PoolObje
{
    [SerializeField] private BombType myBombType;

    private bool isTaked;
    private GameObject lootEffect;
    private ParticleSystem bombTrail;

    private void Awake()
    {
        lootEffect = transform.Find("Loot_Effect").gameObject;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isTaked)
        {
            if (other.TryGetComponent(out Player_Base player_Base))
            {
                isTaked = true;
                bombTrail.Play();
                lootEffect.SetActive(false);
                player_Base.IncreaseBombAmount(myBombType);
                transform.DOLocalMoveY(10, 1).OnComplete(() =>
                {
                    bombTrail.Stop();
                    EnterHavuz();
                });
            }
        }
    }
    public override void ObjeHavuzExit()
    {
        isTaked = false;
        lootEffect.SetActive(true);
        transform.localScale = Vector3.zero;
        base.ObjeHavuzExit();
    }
}