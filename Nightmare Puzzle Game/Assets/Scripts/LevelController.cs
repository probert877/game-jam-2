using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] string _nextLevelName;
    public GameObject[] _monsters;
    public GameObject GameOverScreen;
    public GameObject VictoryScreen;
    private LivesManager livesManager;

    // Keep track of all Monsters in the level, to make sure they're dead
    // To figure out if we should move to next level

    void Awake() {
        _monsters = GameObject.FindGameObjectsWithTag("Enemy");
        livesManager = FindObjectOfType<LivesManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MonstersAreAllDead() || livesManager.livesCounter <1 ) {
            StartCoroutine(FigureOutNextLevel());
            
        }
        else 
        {
            
            
            
        }
    }

    public void GameOver()
    {
        GameOverScreen.SetActive(true);
    }

    public void Victory()
    {
        VictoryScreen.SetActive(true);
    }

    public void Restart()
    {
        GameOverScreen.SetActive(false);
        VictoryScreen.SetActive(false);
        livesManager.livesCounter = livesManager.defaultLives;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator FigureOutNextLevel()
    {
        yield return new WaitForSeconds(5);
        if (livesManager.livesCounter < 1 && MonstersAreAllDead() == false) { 
            GameOver();
        }

        else if (MonstersAreAllDead()) 
        {
            Victory();
        }

    }

    void GoToNextLevel()
    {
        Debug.Log("Go to level " + _nextLevelName);
        // loads the next level, by the Level Name, if it is a valid level name
        SceneManager.LoadScene(_nextLevelName);
    }

    public bool MonstersAreAllDead()
    {   
        // loop over monsters to figure out the state to figure out if every one of them is dead
        foreach (var monster in _monsters){
            // if one is active, break out of the loop and return false
            if (monster.gameObject.activeSelf)
                return false;
        }
        // this only hits if all monsters are dead
        return true;
    }
}
