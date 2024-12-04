using DG.Tweening;
using UnityEngine;

public class Loot_Material : Loot_Base
{
    [SerializeField] private All_Item_Holder all_Item_Holder;
    [SerializeField] private SpriteRenderer mySpriteRenderer;
    
    private MaterialHolder myMaterial = new MaterialHolder();

    public override void OnAwake()
    {
        int rnd = Random.Range(0, all_Item_Holder.MalzemeList.Count);
        mySpriteRenderer.sprite = all_Item_Holder.MalzemeList[rnd].MyIcon;
        myMaterial.recipeItem = all_Item_Holder.MalzemeList[rnd];
        myMaterial.recipeAmount = 1;
        myMaterial.inventoryType = InventoryType.Material;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!IsTaked)
        {
            if (other.CompareTag("Player"))
            {
                SetTaked(true);
                transform.DOScale(Vector3.one * 1.5f, 0.1f).OnComplete(() =>
                {
                    transform.DOScale(Vector3.zero, 0.1f).OnComplete(() =>
                    {
                        Inventory_Manager.Instance.AddItem(myMaterial);
                        EnterHavuz();
                    });
                });
            }
        }
    }
}