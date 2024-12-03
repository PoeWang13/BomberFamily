using UnityEngine;

public class Level_Opener : MonoBehaviour
{
    [SerializeField] private int levelOrder;
    [SerializeField] private BoardSaveType boardSaveType;

    private bool waitUpgrade;
    private Renderer levelRenderer;

    private void Start()
    {
        levelRenderer = GetComponent<Renderer>();
        waitUpgrade = levelOrder >= Save_Load_Manager.Instance.gameData.maxGameLevel;
        levelRenderer.material = levelOrder <= Save_Load_Manager.Instance.gameData.lastLevel ? Game_Manager.Instance.MaterialLevelOpen : levelRenderer.material;
    }
    public void SetOpenerOrder(int order)
    {
        levelOrder = order;
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
        Canvas_Manager.Instance.OpenLevel(boardSaveType, levelOrder);
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