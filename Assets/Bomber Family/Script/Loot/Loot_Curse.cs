using DG.Tweening;
using UnityEngine;

public class Loot_Curse : PoolObje
{
    private bool isTaked;
    private GameObject lootEffect;
    private ParticleSystem curseParticle;

    private void Awake()
    {
        lootEffect = transform.Find("Loot_Effect").gameObject;
        curseParticle = transform.Find("Curse_Particle").GetComponent<ParticleSystem>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isTaked)
        {
            if (other.TryGetComponent(out Player_Base player_Base))
            {
                isTaked = true;
                curseParticle.Play();
                player_Base.GiveCurse();
                lootEffect.SetActive(false);
                // Sağa döner
                transform.DOLocalRotate(Vector3.up * 30, 0.15f).OnComplete(() =>
                {
                    // Sola döner
                    transform.DOLocalRotate(Vector3.down * 30, 0.15f).OnComplete(() =>
                    {
                        // Sağa döner
                        transform.DOLocalRotate(Vector3.up * 60, 0.15f).OnComplete(() =>
                        {
                            // Sola döner
                            transform.DOLocalRotate(Vector3.down * 60, 0.15f).OnComplete(() =>
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
        base.ObjeHavuzExit();
    }
}