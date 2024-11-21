using UnityEngine;

public class Level_Opener : MonoBehaviour
{
    private int levelOrder;
    private bool waitUpgrade;
    private Renderer levelRenderer;

    public void SetOpenerOrder(int order)
    {
        levelRenderer = GetComponent<Renderer>();
        levelOrder = order;
        waitUpgrade = levelOrder >= Save_Load_Manager.Instance.gameData.maxGameLevel;
        levelRenderer.material.color = levelOrder <= Save_Load_Manager.Instance.gameData.lastLevel ? Color.green : Color.gray;
    }
    public void SetOpener()
    {
        levelRenderer.material.color = Color.green;
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
    public void LevelFinish()
    {
        // Level bitirildi.
        levelRenderer.material.color = Color.green;
    }
    public void LevelFinishFailed()
    {
        // Level bitirme denemesi başarısız oldu.
        levelRenderer.material.color = Color.red;
    }
}