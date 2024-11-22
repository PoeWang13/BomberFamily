using DG.Tweening;
using UnityEngine;

public class Object_Thrower : Secret_Object
{
    [SerializeField] private Rigidbody object_Throw;
    [SerializeField] private float waitingAfterThrow;
    [SerializeField] private Vector3 throwForce = new Vector3(-100, 200, -50);

    [ContextMenu("Throw")]
    public void Throw()
    {
        if (LevelCondition())
        {
            object_Throw.useGravity = true;
            object_Throw.velocity = Vector3.zero;
            Debug.Log("Throw : " + Time.time);
            object_Throw.AddForceAtPosition(throwForce, object_Throw.transform.position, ForceMode.Impulse);
            DOVirtual.DelayedCall(waitingAfterThrow, () =>
            {
                object_Throw.velocity = Vector3.zero;
                Debug.Log("DelayedCall : " + Time.time);
                SetMissionComplete();
            });
        }
    }
}