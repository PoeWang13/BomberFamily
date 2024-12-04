using DG.Tweening;
using UnityEngine;

public class Loot_Magic_Stone : Loot_Base
{
    private void OnTriggerEnter(Collider other)
    {
        if (!IsTaked)
        {
            if (other.CompareTag("Player"))
            {
                SetTaked(true);
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
}