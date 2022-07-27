using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public bool randomize = true;
    public int mySeason;
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    ScoreKeeper scoreKeeper;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        if (randomize)
        {
            mySeason = Random.Range(0, 4);
        }
        
        animator.SetInteger("Season", mySeason);

        scoreKeeper = GameObject.Find("Manager").GetComponent<ScoreKeeper>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<Seasons>().season == mySeason)
            {
                //UnityEngine.Debug.Log("My season!");
                //animation to go through or something idk
            }
            else
            {
                scoreKeeper.GameOver();
                UnityEngine.Debug.Log("GAME OVER");
                
            }
        }
    }
}
