using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeSceneButton : MonoBehaviour
{
    public string sceneName = "SampleScene";

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
