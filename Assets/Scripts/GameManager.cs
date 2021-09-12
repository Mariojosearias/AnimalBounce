using System.Collections;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject NextLevel;
    [SerializeField] int numAnimals;
    [SerializeField] public bool isRunning;

    private int NumberTimes = 0;    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
           
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ChangeGameRunningState();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);

    }
    public void NextLvl(int nivel)
    {
        SceneManager.LoadScene(nivel);
        Time.timeScale = 1;
    }
    public void PauseGame()
    {
       if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                PauseMenu.SetActive(true);
                Debug.Log("Game Pause");
            }
       else if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                PauseMenu.SetActive(false);
                Debug.Log("Game Resume");
            }
        
    }

    public void CaptureAnimal()
    {
        numAnimals--;
        if (numAnimals < 1)
        {
            Time.timeScale = 0;
            NextLevel.SetActive(true);
        }
    }

    public bool Running()
    {
        return isRunning;
    }

           public void ChangeGameRunningState()
        {
            NumberTimes++;
        if (NumberTimes <= 3)
        {
            isRunning = !isRunning;
            StartCoroutine(gameRunning());
        }
                
            IEnumerator gameRunning()
            {       isRunning = true;
                    Time.timeScale = 0.5f;
                    yield return new WaitForSeconds(1.5f);
                    isRunning = false;
                    Time.timeScale = 1;
            }
        }
}

