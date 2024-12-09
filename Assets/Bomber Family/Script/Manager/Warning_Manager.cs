using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

[System.Serializable]
public class Messages
{
    public float warningTime;
    public float warningTimeNext;
    public bool warningCanShow;
    public bool warningShowing;
    public RectTransform warningPanel;
    public TextMeshProUGUI warningText;
    public Messages(RectTransform warning)
    {
        warningPanel = warning;
        warningText = warning.GetChild(0).GetComponent<TextMeshProUGUI>();
    }
    public Messages(Messages warning)
    {
        warningPanel = warning.warningPanel;
        warningText = warning.warningPanel.GetChild(0).GetComponent<TextMeshProUGUI>();
    }
    public bool StopWarning()
    {
        if (warningShowing)
        {
            return false;
        }
        warningTimeNext += Time.deltaTime;
        if (warningTimeNext > warningTime)
        {
            warningTimeNext = 0;
            return true;
        }
        return false;
    }
}
public class Warning_Manager : Singletion<Warning_Manager>
{
    [SerializeField] private RectTransform mesajKutusu;
    [SerializeField] private Transform mesajKutusuParent;

    [SerializeField] private bool warning;
    [SerializeField] private bool warningVar;
    [SerializeField] private int warningAmount;
    [SerializeField] private List<Messages> allWarnings = new List<Messages>();

    public void NotHaveGold()
    {
        ShowMessage("You dont have enough Gold.", 2);
    }
    public void ShowMessage(string msg, float duration = 1)
    {
        bool findWarning = false;
        for (int e = 0; e < allWarnings.Count && !findWarning; e++)
        {
            if (allWarnings[e].warningCanShow)
            {
                findWarning = true;
                SetMessage(allWarnings[e], msg, duration);
            }
        }
        if (!findWarning)
        {
            RectTransform newWarning = Instantiate(mesajKutusu, mesajKutusuParent);
            Messages message = new Messages(newWarning);
            allWarnings.Add(message);
            SetMessage(message, msg, duration);
        }
    }
    private void SetMessage(Messages message, string msg, float duration = 1)
    {
        message.warningTime = duration + 1;
        message.warningText.text = msg;
        message.warningShowing = false;
        message.warningCanShow = false;
        int posY = -250 - (warningAmount * 70);
        message.warningPanel.anchoredPosition = new Vector2(-1900, posY);
        message.warningPanel.DOAnchorPos(new Vector2(0, posY), 1.0f).OnComplete(() => warningVar = true);
        warningAmount++;
    }
    private void Update()
    {
        if (warningVar)
        {
            warning = false;
            for (int e = allWarnings.Count - 1; e >= 0; e--)
            {
                if (allWarnings[e].StopWarning())
                {
                    warningAmount--;
                    SetMEsaj(allWarnings[e]);
                }
                else
                {
                    warning = true;
                }
            }
            if (!warning)
            {
                warningVar = false;
            }
        }
    }
    private void SetMEsaj(Messages message)
    {
        message.warningShowing = true;
        message.warningPanel.DOAnchorPos(new Vector2(0, 250), 1.5f).OnComplete(() =>
        {
            message.warningCanShow = true;
        });
    }
}