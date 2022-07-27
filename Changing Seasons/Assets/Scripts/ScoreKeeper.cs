using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    
    public GameObject playStuff;
    public float score;
    public float highScore;
    public Text scoreText;
    public Text highScoreText;
    public GameObject gameOverStuff;
    public Text finalScoreText;
    public Text finalHighScoreText;
    [HideInInspector] public bool gameOver;

    public GameObject highScoreMarker;
    public Text highScoreMarkerText;
    GameObject player;
    SaveManager saveManager;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        saveManager = gameObject.GetComponent<SaveManager>();
        gameOverStuff.SetActive(false);
        playStuff.SetActive(true);
        highScore = saveManager.state.highScore;
        highScoreText.text = "High Score: " + ((int)(highScore)).ToString();

        highScoreMarker.transform.position = new Vector3(player.transform.position.x + highScore, 0.0F, 0.0F);
        highScoreMarkerText.text = "High Score: " + ((int)(highScore)).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //score text matches score
        scoreText.text = "Score: " + ((int)(score)).ToString();
    }

    public void GameOver()
    {
        //turn everything else off
        player.GetComponent<PlayerButton>().GameOver();
        gameObject.GetComponent<ObjectMove>().GameOver();
        //do stuff
        gameOverStuff.SetActive(true);
        playStuff.SetActive(false);
        finalScoreText.text = "Score: " + ((int)(score)).ToString();

        if (score > highScore)
        {
            saveManager.state.highScore = score;
            finalHighScoreText.text = "New high score!";

        }
        else
        {
            finalHighScoreText.text = "High: " + ((int)(highScore)).ToString();
        }

        saveManager.Save();
        gameOver = true;

    }
}
