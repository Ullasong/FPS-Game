using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// �¾�� GameOverUI�� ������ �ʰ� �ϰ�ʹ�.
// �÷��̾ �׾����� GameOverUI�� ���̰��ϰ�ʹ�.
// ����/����� ����� �����ʹ�.
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

        // �¾�� GameOverUI�� ������ �ʰ� �ϰ�ʹ�.
        gameOverUI.SetActive(false);
    }

    // ����/����� ����� �����ʹ�.
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
