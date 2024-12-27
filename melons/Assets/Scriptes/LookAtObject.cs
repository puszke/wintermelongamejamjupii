using UnityEngine;

public class LookAtObject : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        Vector3 relativePos = target.position - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 1 * Time.deltaTime);
    }
}
