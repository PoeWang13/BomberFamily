using DG.Tweening;
using UnityEngine;

public class Loot_Magic_Stone : PoolObje
{
    private bool isTaked;
    private GameObject lootEffect;

    private void Awake()
    {
        lootEffect = transform.Find("Loot_Effect").gameObject;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isTaked)
        {
            if (other.CompareTag("Player"))
            {
                isTaked = true;
                lootEffect.SetActive(false);
                // Biraz yukarı çıkar
                transform.DOMoveY(1, 0.1f).OnComplete(() =>
                {
                    // Sağa sola döner
                    transform.DOLocalRotate(Vector3.up * 90, 0.1f).OnComplete(() =>
                    {
                        transform.DOLocalRotate(Vector3.down * 90, 0.1f).OnComplete(() =>
                        {
                            // Playera uçar
                            Map_Holder.Instance.BoardGate.FoundMagicStone(this);
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
        transform.eulerAngles = Vector3.zero;
        transform.localPosition = Vector3.zero;
        base.ObjeHavuzExit();
    }
}