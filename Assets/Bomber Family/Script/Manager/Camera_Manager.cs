﻿using DG.Tweening;
using UnityEngine;

public class Camera_Manager : Singletion<Camera_Manager>
{
    [SerializeField] private int cameraSpeed;

    [SerializeField] private int xMaxLimit;
    [SerializeField] private int zMaxLimit;
    private int xMinLimit = 5;
    private int zMinLimit = 3;
    private Transform player;
    private Vector3 playerOldPos;
    // X min 5 max 
    // Y min 3 max 
    public void SetCameraLimit(Vector2Int limit)
    {
        xMaxLimit = limit.x - 5;
        zMaxLimit = limit.y - 5;
        player = Player_Base.Instance.transform;
    }
    [ContextMenu("Set Camera Pos")]
    public void SetCameraPos()
    {
        transform.position = new Vector3(xMaxLimit, 0, zMaxLimit);
        transform.DOMove(new Vector3(xMinLimit, 0, zMinLimit), 2.5f).OnComplete(() =>
        {
            Player_Base.Instance.SetMove(true);
        });
    }
    public void SetCameraPos(Vector3Int pos)
    {
        transform.position = pos + Vector3.left * 5 + Vector3.back * 5;
        transform.DOMove(new Vector3(xMinLimit, 0, zMinLimit), 2.5f).OnComplete(() =>
        {
            Player_Base.Instance.SetMove(true);
        });
    }
    private void Update()
    {
        if (!Game_Manager.Instance.LevelStart)
        {
            return;
        }
        playerOldPos = player.position;
        if (player.position.x < xMinLimit)
        {
            playerOldPos.x = xMinLimit;
        }
        else if(player.position.x > xMaxLimit)
        {
            playerOldPos.x = xMaxLimit;
        }
        if(player.position.z < zMinLimit)
        {
            playerOldPos.z = zMinLimit;
        }
        else if(player.position.z > zMaxLimit)
        {
            playerOldPos.z = zMaxLimit;
        }
        transform.position = Vector3.Lerp(transform.position, playerOldPos, Time.deltaTime * cameraSpeed);

    }
}