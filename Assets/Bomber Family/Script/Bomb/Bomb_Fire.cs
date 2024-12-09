using UnityEngine;
using System.Collections.Generic;

public class Bomb_Fire : PoolObje
{
    private float fireTime = 1;
    private float newFireTime = 1;
    private int power;
    private Transform boardFireParent;
    private Collider myCollider;
    private List<ParticleSystem> particles = new List<ParticleSystem>();

    private void Awake()
    {
        boardFireParent = Utils.MakeChieldForGameElement("Board_Fire");
        transform.SetParent(boardFireParent);
        particles.AddRange(GetComponentsInChildren<ParticleSystem>());
        myCollider = GetComponent<Collider>();
    }
    public void SetFire(int power, float time = 1)
    {
        this.power = power;
        fireTime = time;
        newFireTime = fireTime;
        particles.ForEach(p => { p.Play(); });
        Player_Base.Instance.IgnoreCollider(myCollider);
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