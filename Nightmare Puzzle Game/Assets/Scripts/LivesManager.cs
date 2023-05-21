using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesManager : MonoBehaviour
{

    public int defaultLives;
    public int livesCounter;
    private LevelController levelController;

    public Text livesText;


    // Start is called before the first frame update
    void Start()
    {
        livesCounter = defaultLives;
        levelController = FindObjectOfType<LevelController>();
    }

    // Update is called once per frame
    void Update()
    {
        livesText.text = "x " + livesCounter/2;


    }

    public void subtractLife()
    {
        livesCounter = livesCounter - 1;
    }
}
