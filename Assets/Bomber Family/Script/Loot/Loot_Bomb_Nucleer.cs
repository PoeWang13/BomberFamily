using DG.Tweening;
using UnityEngine;

public class Loot_Bomb_Nucleer : PoolObje
{
    [SerializeField] private BombType myBombType;

    private bool isTaked;
    private GameObject lootEffect;
    private ParticleSystem bombTrail;

    private void Awake()
    {
        bombTrail = transform.Find("Bomb_Nucleer_Trail").GetComponent<ParticleSystem>();
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
    public override void ObjeHavuzExit()
    {
        isTaked = false;
        lootEffect.SetActive(true);
        transform.localPosition = Vector3.zero;
        base.ObjeHavuzExit();
    }
}