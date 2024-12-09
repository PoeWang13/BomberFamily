using DG.Tweening;
using UnityEngine;

public class Loot_Bomb_Nucleer : Loot_Bomb
{
    private ParticleSystem bombTrail;

    public override void OnAwake()
    {
        bombTrail = transform.Find("Bomb_Nucleer_Trail").GetComponent<ParticleSystem>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!IsTaked)
        {
            if (other.TryGetComponent(out Player_Base player_Base))
            {
                SetTaked(true);
                bombTrail.Play();
                LootEffect.SetActive(false);
                AddBomb();
                DOVirtual.DelayedCall(0.25f, () =>
                {
                    transform.DOLocalMoveY(10, 1).OnComplete(() =>
                    {
                        bombTrail.Stop();
                        EnterHavuz();
                    });
                });
            }
        }
    }
}