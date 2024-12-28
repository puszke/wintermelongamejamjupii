using UnityEngine;

public class BlackRed : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            GetComponent<Animator>().Play("redblack");
        }
        else
        {
            GetComponent<Animator>().Play("redblack", -1, 0);
        }
    }
}
