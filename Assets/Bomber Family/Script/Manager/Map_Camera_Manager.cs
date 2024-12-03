using UnityEngine;

public class Map_Camera_Manager : Singletion<Map_Camera_Manager>
{
    [SerializeField] private int xMinLimit;
    [SerializeField] private int zMinLimit;
    [SerializeField] private int xMaxLimit;
    [SerializeField] private int zMaxLimit;
    [SerializeField] private int cameraSpeed = 1;
    [SerializeField] private Joystick joystickMap;

    private Vector3 newPos;
    private void Update()
    {
        if (Game_Manager.Instance.LevelStart)
        {
            return;
        }
        newPos = joystickMap.Direction();
        if (transform.position.x < xMinLimit && newPos.x < 0)
        {
            newPos.x = 0;
        }
        if (transform.position.x > xMaxLimit && newPos.x > 0)
        {
            newPos.x = 0;
        }
        if (transform.position.z < zMinLimit && newPos.z < 0)
        {
            newPos.z = 0;
        }
        if (transform.position.z > zMaxLimit && newPos.z > 0)
        {
            newPos.z = 0;
        }
        transform.Translate(newPos * Time.deltaTime * cameraSpeed);

    }
}