using UnityEngine;

public class Loot_Base : PoolObje
{
    private bool isTaked;
    private GameObject lootEffect;

    public bool IsTaked { get { return isTaked; } }
    public GameObject LootEffect { get { return lootEffect; } }

    private void Awake()
    {
        transform.localScale = Vector3.one;
        lootEffect = transform.Find("Loot_Effect").gameObject;
        OnAwake();
    }
    public virtual void OnAwake()
    {
    }
    public void SetTaked(bool taked)
    {
        isTaked = taked;
    }
    public override void ObjeHavuzExit()
    {
        isTaked = false;
        lootEffect.SetActive(true);
        transform.localScale = Vector3.zero;
        transform.localPosition = Vector3.zero;
        transform.eulerAngles = Vector3.zero;
        base.ObjeHavuzExit();
    }
}