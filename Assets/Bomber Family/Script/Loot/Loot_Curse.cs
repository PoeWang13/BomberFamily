using DG.Tweening;
using UnityEngine;

public class Loot_Curse : Loot_Base
{
    private ParticleSystem curseParticle;

    public override void OnAwake()
    {
        curseParticle = transform.Find("Curse_Particle").GetComponent<ParticleSystem>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!IsTaked)
        {
            if (other.TryGetComponent(out Player_Base player_Base))
            {
                SetTaked(true);
                curseParticle.Play();
                player_Base.GiveCurse();
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
}