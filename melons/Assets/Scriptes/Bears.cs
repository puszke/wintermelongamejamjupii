using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bears : MonoBehaviour
{
    private Rigidbody rb;
    public Vector3 rnd;
    bool shouldMove=false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(ChoosePath());
    }
    IEnumerator ChoosePath()
    {
        yield return new WaitForSeconds(Random.Range(1,3));
        Choose();
        StartCoroutine(ChoosePath());
        shouldMove = false;
        if(Random.Range(1,4)==1)
        {
            shouldMove = true;
        }
    }

    void Choose()
    {
        float x = Random.Range(5, 55);
        float z = Random.Range(5, 55);
        rnd = new Vector3(x, 2, z);
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.transform.tag=="Player")
        {
            GetComponent<Animator>().SetTrigger("Dead");
            Destroy(this);
        }
    }
    // Update is called once per frame
    void Update()
    {
      
        //GetComponent<Animator>().speed = 2;
        Vector3 targetPostition = new Vector3(rnd.x,
        this.transform.position.y,
                                       rnd.z);
        this.transform.LookAt(targetPostition);
        if (shouldMove)
        {
            rb.AddForce(transform.forward * Time.deltaTime * 15f, ForceMode.Impulse);
            GetComponent<Animator>().Play("Bears");
        }
        else
            GetComponent<Animator>().Play("Bears", -1, 0);
    }
}
