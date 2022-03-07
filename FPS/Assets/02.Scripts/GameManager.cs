using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 태어날때 GameOverUI를 보이지 않게 하고싶다.
// 플레이어가 죽었을때 GameOverUI를 보이게하고싶다.
// 종료/재시작 기능을 만들고싶다.
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }

    public GameObject gameOverUI;

    public Button buttonQuit;
    public Button buttonRestart;

    // Start is called before the first frame update
    void Start()
    {
        buttonQuit.onClick.AddListener(OnClickQuit);
        buttonRestart.onClick.AddListener(OnClickRestart);

        // 태어날때 GameOverUI를 보이지 않게 하고싶다.
        gameOverUI.SetActive(false);
    }

    // 종료/재시작 기능을 만들고싶다.
    public void OnClickQuit()
    {
        Application.Quit();
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
