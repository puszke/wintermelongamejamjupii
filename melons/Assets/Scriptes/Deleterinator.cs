using System.Security.Cryptography;
using UnityEngine;

public class Deleterinator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {

        if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift)) // Lewy przycisk myszy
        {
            
            //Debug.Log(Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("fajny").transform.position));

            if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("fajny").transform.position) < 3f)
            {
                GlobalTrailManager.instance.trailNum++;
                Destroy(gameObject); // Usuwa bie¿¹cy obiekt 

            }


        }
    }
}
