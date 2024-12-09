using UnityEngine;
using System.Collections;

public class Board_Gate : Board_Object
{
    [SerializeField] private GameObject effect1;
    [SerializeField] private GameObject effect2;

    private int magicStoneAmount;
    private bool gameFinish;

    public bool IsGameFinish { get { return gameFinish; } }

    private void Awake()
    {
        if (MyTriggeredCollider != null)
        {
            if (Player_Base.Instance.MyNotTriggeredCollider != null)
            {
                Physics.IgnoreCollision(MyTriggeredCollider, Player_Base.Instance.MyNotTriggeredCollider, true);
            }
        }
    }
    public override void SetMouseButton()
    {
        Canvas_Manager.Instance.OpenBaseSetting(true);
        Canvas_Manager.Instance.CloseSettingPanels();
        Map_Creater_Manager.Instance.ChooseStuckObject(this);
    }
    public void SetNeededMagicStone(int amount)
    {
        magicStoneAmount = amount;
        effect1.SetActive(false);
        effect2.SetActive(false);
    }
    public void FoundMagicStone(PoolObje magicStone)
    {
        magicStoneAmount--;
        StartCoroutine(SendMagicStone(magicStone));
    }
    IEnumerator SendMagicStone(PoolObje magicStone)
    {
        float distance = Vector3.Distance(magicStone.transform.position, transform.position);
        float yükseklik = distance * 0.1f * 5;
        if (distance < 10)
        {
            yükseklik = 5;
        }
        Vector3 magicStonePoint = Vector3.zero;
        Vector3 stonePoint = magicStone.transform.position;
        // Son ve ilk noktayı belirle
        Vector3 p0 = stonePoint;
        Vector3 p3 = transform.position;
        // Son ve ilk noktanın orta - tepe noktasını belirle
        Vector3 tepe = stonePoint + (transform.position - stonePoint).normalized * distance * 0.5f + Vector3.up * yükseklik;
        distance = Vector3.Distance(p0, tepe);
        Vector3 p1 = p0 + (tepe - p0).normalized * distance * 0.5f;
        Vector3 p2 = p3 + (tepe - p3).normalized * distance * 0.5f;

        float tParam = 0;
        while (tParam < 1)
        {
            tParam += Time.deltaTime;
            magicStonePoint = Mathf.Pow(1 - tParam, 3) * p0 + 3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 + 3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 + Mathf.Pow(tParam, 3) * p3;
            magicStone.transform.position = magicStonePoint;
            yield return new WaitForEndOfFrame();
        }
        if (magicStoneAmount <= 0)
        {
            // Açılma animasyonu yap ve çalıştır.
            if (Random.value < 0.33f)
            {
                effect1.SetActive(true);
            }
            else if (Random.value < 0.66f)
            {
                effect2.SetActive(true);
            }
            else
            {
                effect1.SetActive(true);
                effect2.SetActive(true);
            }
        }
        magicStone.EnterHavuz();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.LogWarning("gameFinish", other.gameObject);
            // Oyun bitti.
            if (Game_Manager.Instance.GameType == GameType.Game)
            {
                if (magicStoneAmount > 0)
                {
                    Warning_Manager.Instance.ShowMessage("You need " + magicStoneAmount + " more Magic Stones.", 1.5f);
                }
                else
                {
                    if (!gameFinish)
                    {
                        GameFinish();
                    }
                }
            }
            else if(Game_Manager.Instance.GameType == GameType.MapCreate)
            {
                if (magicStoneAmount <= 0)
                {
                    if (!gameFinish)
                    {
                        gameFinish = true;
                        Map_Creater_Manager.Instance.CheckingLevelMap();
                    }
                }
                else
                {
                    Warning_Manager.Instance.ShowMessage("You need " + magicStoneAmount + " more Magic Stones.", 1.5f);
                }
            }
        }
    }
    [ContextMenu("Game Finish")]
    private void GameFinish()
    {
        Canvas_Manager.Instance.GameWin();
        Game_Manager.Instance.GameWin();
        gameFinish = true;
    }
    public override void ObjeHavuzEnter()
    {
        ResetGate();
        // Kapanma animasyonu yap ve çalıştır.
        base.ObjeHavuzEnter();
    }
    public void ResetGate()
    {
        effect1.SetActive(false);
        effect2.SetActive(false);
        gameFinish = false;
    }
    public void ResetFinishing()
    {
        gameFinish = false;
    }
}