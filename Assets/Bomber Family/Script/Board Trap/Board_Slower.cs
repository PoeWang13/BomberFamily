using UnityEngine;

public class Board_Slower : Board_Object
{
    [SerializeField, Range(0, 1)] private float slowerPercent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Character_Base>().SetSpeed(slowerPercent);
        }
        else if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Character_Base>().SetSpeed(slowerPercent);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Character_Base>().ResetSpeed();
        }
        else if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Character_Base>().ResetSpeed();
        }
    }
}