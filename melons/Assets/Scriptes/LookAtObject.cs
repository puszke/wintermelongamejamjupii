using UnityEngine;

public class LookAtObject : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 1f;

    public bool lookAtCamera = false;

    void Update()
    {
        if(lookAtCamera)
        {
            target = Camera.main.transform;
        }
        Vector3 relativePos = target.position - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, moveSpeed * Time.deltaTime);
    }
}
