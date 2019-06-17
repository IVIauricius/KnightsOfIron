using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;


[System.Serializable]
public class PlayerSaveData
{
    public static string saveName = "playerData.boop";
    public float Health;
    public int rightNumSol;
    public int leftNumSol;
    public int levelIndex;
}

public class ScnManager : MonoBehaviour {

    public static ScnManager singleton;
    
    public string[] levels;
    public int sceneIndex = 0;
    public float loadingProgress;
    public PlayerSaveData save;

    void Awake()
    {
        if (singleton != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            singleton = this;
            string path = Path.Combine(Application.streamingAssetsPath, PlayerSaveData.saveName);
            if (File.Exists(path))
            {
                FileStream stream = File.Open(path, FileMode.Open);
                BinaryFormatter bin = new BinaryFormatter();
                save = (PlayerSaveData)bin.Deserialize(stream);
                stream.Close();
                print(save.Health);
            }
            else
            {
                save = new PlayerSaveData();
                save.levelIndex = 1;
                save.Health = 500;
                save.rightNumSol = 5;
                save.leftNumSol = 5;
            }

        }
       
    }

    // Use this for initialization
    void Start () {
      //  print("START");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void returnToMain()
    {

        SceneManager.LoadScene("MainMenu");
    }

    public void loadGameOver()
    {
        SceneManager.LoadSceneAsync("GameOver");
    }
    public void loadCredits()
    {
        print("Load Credits");
        SceneManager.LoadScene("Credits");
    }

    IEnumerator loadInOrderFromDeath()
    {
        yield return SceneManager.LoadSceneAsync("GameOver");

        yield return new WaitForSeconds(2f);

        yield return StartCoroutine(loadMenuFromDeath(levels[0]));
    }

    IEnumerator loadMenuFromDeath(string scene)
    {
        var async = SceneManager.LoadSceneAsync(scene);

        async.allowSceneActivation = false;

        while (!async.isDone)
        {
            loadingProgress = Mathf.Clamp01(async.progress / 0.9f) * 100;

            if (async.progress >= 0.9f)
            {

                async.allowSceneActivation = true;
            }

            yield return null;
        }
       // FindObjectOfType<PlayerSpawn>().SpawnPlayer();
    }

    IEnumerator loadInOrder()
    {
        yield return SceneManager.LoadSceneAsync("loading");

        yield return new WaitForSeconds(2f); 

        yield return StartCoroutine(loadGameScene(levels[sceneIndex]));
    }

    IEnumerator loadGameScene(string scene)
    {
        var async = SceneManager.LoadSceneAsync(scene);

        async.allowSceneActivation = false;

        while(!async.isDone)
        {
            loadingProgress = Mathf.Clamp01(async.progress / 0.9f) * 100;

            if (async.progress >= 0.9f)
            {

                async.allowSceneActivation = true;
            }

            yield return null;
        }
        FindObjectOfType<PlayerSpawn>().SpawnPlayer();
    }

    public void Continue()
    {
        if(File.Exists(Path.Combine(Application.streamingAssetsPath, PlayerSaveData.saveName)))
        {
            sceneIndex = save.levelIndex;
            Reloadscene();
        }
        
    }

    public void Reloadscene()
    {
        StartCoroutine(loadInOrder());
    }

    public void AdvanceScene()
    {
        sceneIndex++;
        if (FindObjectOfType<PlayerInputController>())
        {
            GameObject player = FindObjectOfType<PlayerInputController>().gameObject;
            save.Health = player.GetComponent<PlayerHealth>().currentHealth;
            save.rightNumSol = player.transform.parent.GetComponentInChildren<SolSpawnF1>().myRightSoldiers.Count;
            save.leftNumSol = player.transform.parent.GetComponentInChildren<SolSpawnF1>().myLeftSoldiers.Count;
            
        }
        save.levelIndex = sceneIndex;

        string path = Path.Combine(Application.streamingAssetsPath, PlayerSaveData.saveName);
        FileStream stream = File.Create(path);
        BinaryFormatter bin = new BinaryFormatter();
        bin.Serialize(stream, save);

        StartCoroutine(loadInOrder());
    }
}
