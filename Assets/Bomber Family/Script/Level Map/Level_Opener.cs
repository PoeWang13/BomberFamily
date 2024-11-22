using UnityEngine;

public class Level_Opener : MonoBehaviour
{
    public Game_Manager manager;
    public bool deneme;
    private void OnValidate()
    {
        if (deneme)
        {
            deneme = false;
            for (int e = 0; e < manager.levelOpenerList.Count; e++)
            {
                if (manager.levelOpenerList[e].transform == transform)
                {
                    transform.name = "Level Opener - " + e;
                }
            }
        }
    }
    private int levelOrder;
    private bool waitUpgrade;
    private Renderer levelRenderer;

    public void SetOpenerOrder(int order)
    {
        levelRenderer = GetComponent<Renderer>();
        levelOrder = order;
        waitUpgrade = levelOrder >= Save_Load_Manager.Instance.gameData.maxGameLevel;
        levelRenderer.material = levelOrder <= Save_Load_Manager.Instance.gameData.lastLevel ? Game_Manager.Instance.MaterialLevelOpen : levelRenderer.material;
    }
    public void SetOpener(Material materialLevelOpen)
    {
        levelRenderer.material = materialLevelOpen;
    }
    private void OnMouseUpAsButton()
    {
        if (waitUpgrade)
        {
            Warning_Manager.Instance.ShowMessage("This level will come very soon.", 2);
            return;
        }
        if (levelOrder > Save_Load_Manager.Instance.gameData.lastLevel)
        {
            Warning_Manager.Instance.ShowMessage("You should finish all previous level.", 2);
            return;
        }
        Canvas_Manager.Instance.OpenLevel(levelOrder);
    }
    public void LevelFinish(Material materialLevelFinish)
    {
        // Level bitirildi.
        levelRenderer.material = materialLevelFinish;
    }
    public void LevelFinishLost(Material materialLevelLost)
    {
        // Level bitirme denemesi başarısız oldu.
        levelRenderer.material = materialLevelLost;
    }
}