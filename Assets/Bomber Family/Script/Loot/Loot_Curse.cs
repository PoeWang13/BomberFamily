using DG.Tweening;
using UnityEngine;

public class Loot_Curse : PoolObje
{
    private bool isTaked;
    private ParticleSystem curseParticle;

    private void Start()
    {
        curseParticle = transform.Find("Curse_Particle").GetComponent<ParticleSystem>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isTaked)
        {
            if (other.TryGetComponent(out Player_Base player_Base))
            {
                isTaked = true;
                player_Base.GiveCurse();
                curseParticle.Play();
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
        transform.eulerAngles = Vector3.zero;
        base.ObjeHavuzExit();
    }
}