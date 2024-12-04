using DG.Tweening;
using UnityEngine;

public class Loot_Bomb_Nucleer : Loot_Base
{
    [SerializeField] private BombType myBombType;

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
                Save_Load_Manager.Instance.gameData.allBombAmount[(int)myBombType].bombAmount++;
                Canvas_Manager.Instance.SetBomb(myBombType);
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