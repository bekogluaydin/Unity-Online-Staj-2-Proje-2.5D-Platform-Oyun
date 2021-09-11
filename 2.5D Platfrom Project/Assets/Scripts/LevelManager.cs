using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public static int nextLevel = 0;
    private float _target;

    public static bool isEndOfLevel = true;

    [SerializeField] private  GameObject _loaderCanvas;
    [SerializeField] private  Image _progressBar;
    [SerializeField] private TextMeshProUGUI levelNameText;
    private void Awake()
    {
        //if (Instance == null)
        //{
        //    Instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
    }

    void Start()
    {
        isEndOfLevel = true;
    }
 
    public static void ContinueGame()
    {
        SceneManager.LoadScene(LevelsUnlock.indexNumber);
    }

    //public void Deneme()
    //{
    //    LoadScene(nextLevel);
    //}

    public static void GetLevel()
    {
        int levelCount = SceneManager.GetActiveScene().buildIndex + 1;

        if (levelCount == SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log(levelCount + " : " + SceneManager.sceneCountInBuildSettings);
            nextLevel = 0;
        }
        else
        {
            nextLevel = levelCount;
            if (PlayerPrefs.GetInt("ReachedLevel", 1) < nextLevel)
                PlayerPrefs.SetInt("ReachedLevel", nextLevel);

            //SceneManager.LoadScene(nextLevel); 
        }
    }

    public async void LoadScene(int sceneIndex)
    {
        if (nextLevel == 0)
        {
            levelNameText.text = "Main Menu";
        }
        else
            levelNameText.text = "Level " + sceneIndex;

        _target = _progressBar.fillAmount = 0;
        var scene = SceneManager.LoadSceneAsync(sceneIndex);
        scene.allowSceneActivation = false;

        _loaderCanvas.SetActive(true);

        do
        {
            await Task.Delay(1100);
            _target = scene.progress;
        } while (scene.progress < 0.9f);

        scene.allowSceneActivation = true;
        _loaderCanvas.SetActive(false);
        isEndOfLevel = true;
        //if (nextLevel != 0)
        //{
        //   deneme = true;
        //}
    }

    private void Update()
    {
        if (PlayerManager.winLevel && isEndOfLevel)
        {
            isEndOfLevel = !isEndOfLevel;
            GetLevel();
            //nextLevelButton.SetActive(true);
            LoadScene(nextLevel);
        }
        _progressBar.fillAmount = Mathf.MoveTowards(_progressBar.fillAmount, _target + 10, 1.1f * Time.deltaTime);
    }
}
