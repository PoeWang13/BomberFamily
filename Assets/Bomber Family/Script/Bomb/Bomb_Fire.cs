using UnityEngine;
using System.Collections.Generic;

public class Bomb_Fire : PoolObje
{
    [SerializeField] private float fireTime = 1;
    private float newFireTime = 1;
    private int power;
    private Transform boardFireParent;
    private List<ParticleSystem> particles = new List<ParticleSystem>();

    private void Awake()
    {
        boardFireParent = Utils.MakeChieldForGameElement("Board_Fire");
        transform.SetParent(boardFireParent);
        particles.AddRange(GetComponentsInChildren<ParticleSystem>());
    }
    public void SetFire(int power)
    {
        this.power = power;
        newFireTime = fireTime;
        particles.ForEach(p => { p.Play(); });
    }
    private void Update()
    {
        newFireTime -= Time.deltaTime;
        if (newFireTime < 0)
        {
            newFireTime = fireTime;
            EnterHavuz();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamegable IDamegable))
        {
            IDamegable.TakeDamage(power);
        }
    }
}