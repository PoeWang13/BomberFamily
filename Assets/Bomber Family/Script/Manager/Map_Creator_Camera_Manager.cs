using DG.Tweening;
using UnityEngine;

public class Map_Creator_Camera_Manager : Singletion<Map_Creator_Camera_Manager>
{
    [SerializeField] private int xMaxLimit;
    [SerializeField] private int zMaxLimit;
    [SerializeField] private int cameraSpeed = 1;
    [SerializeField] private Joystick joystickMap;

    private Vector3 newPos;
    [SerializeField] private int xMinLimit = 5;
    [SerializeField] private int zMinLimit = 3;

    public void SetCameraLimit(Vector2Int limit)
    {
        xMaxLimit = limit.x - 5;
        zMaxLimit = limit.y - 5;
        DOTween.To(value =>
        {
            transform.position = new Vector3(xMinLimit, 0, zMinLimit) * value;
        }, startValue: 0, endValue: 1, duration: 0.5f);
    }
    private void Update()
    {
        if (Game_Manager.Instance.LevelStart)
        {
            return;
        }
        newPos = joystickMap.Direction();
        if (transform.position.x < xMinLimit && newPos.x == -1)
        {
            newPos.x = 0;
        }
        if (transform.position.x > xMaxLimit && newPos.x == 1)
        {
            newPos.x = 0;
        }
        if (transform.position.z < zMinLimit && newPos.z == -1)
        {
            newPos.z = 0;
        }
        if (transform.position.z > zMaxLimit && newPos.z == 1)
        {
            newPos.z = 0;
        }
        transform.Translate(newPos * Time.deltaTime * cameraSpeed);

    }
}