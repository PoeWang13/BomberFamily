using DG.Tweening;
using UnityEngine;

public class Loot_Bomb_Anti : PoolObje
{
    [SerializeField] private BombType myBombType;

    private bool isTaked;
    private ParticleSystem bombTrail;

    private void Start()
    {
        bombTrail = transform.Find("Bomb_Anti_Trail").GetComponent<ParticleSystem>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isTaked)
        {
            if (other.TryGetComponent(out Player_Base player_Base))
            {
                isTaked = true;
                bombTrail.Play();
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
        transform.localPosition = Vector3.zero;
        base.ObjeHavuzExit();
    }
}