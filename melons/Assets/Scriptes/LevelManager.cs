using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int level = 0;
    public bool GameOver = false;
    public bool GameWin = false;


    bool winShowed = false;

    public GameObject WINSCREEN;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadLevel();
        level = PlayerPrefs.GetInt("level");

    }

    void LoadLevel()
    {
        Destroy(GameObject.Find((level-1).ToString()));
        GameObject newLevel = Instantiate(Resources.Load(level.ToString()) as GameObject);
        newLevel.transform.position = Vector3.zero;
    }

    public void NewLevel()
    {
        level++;
        PlayerPrefs.SetInt("level", level);
        LoadLevel();
    }

    IEnumerator WaitToWin()
    {
        winShowed = true;
        yield return new WaitForSeconds(2);
        WINSCREEN.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        foreach(GameObject p in GameObject.FindGameObjectsWithTag("Player"))
        {
            if(p.GetComponent<TrainController>().derailed && p.GetComponent<TrainController>().isImportant)
            {
                GameOver = true;
            }
        }
        int dab = 0;
        foreach(GameObject b in GameObject.FindGameObjectsWithTag("Town"))
        {
            if (b.GetComponent<Town>().isTrainIn)
                dab++;
        }
        if(dab>= GameObject.FindGameObjectsWithTag("Town").Length)
        {
            GameWin = true;
            if(!winShowed)
            {
                StartCoroutine(WaitToWin());
            }
        }
    }
}
