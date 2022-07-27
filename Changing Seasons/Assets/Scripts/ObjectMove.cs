using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    public bool scoreKeeper;
    ScoreKeeper scoreScript;
    public static float startSpeed = 5.2F;
    [HideInInspector] public static float speed;
    public static float timeBetweenIncrease = 3.0F;
    public static float increaseAmount = 0.1F;
    public static float nextIncrease;
    public static float destroyPoint = -20;
    public static bool gameRunning = true;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        speed = startSpeed;
        if (!scoreKeeper)
        {
            rb = GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(-speed, 0.0F);
        }
        else
        {
            scoreScript = gameObject.GetComponent<ScoreKeeper>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!scoreKeeper)
        {
            if (transform.position.x < destroyPoint)
            {
                //UnityEngine.Debug.Log("Destroy me!!");
                Destroy(this.gameObject);
            }
            rb.velocity = new Vector2(-speed, 0.0F);
        }
        

    }

    void FixedUpdate()
    {
        if (!scoreKeeper)
        {
            if (gameRunning)
            {
                if (nextIncrease < Time.time)
                {
                    speed += increaseAmount;
                    nextIncrease = Time.time + timeBetweenIncrease;
                    //UnityEngine.Debug.Log("INCREASE SPEED: " + speed);
                }
            }
        }
        else
        {
            scoreScript.score += speed * Time.deltaTime;
        }
            
        
    }

    public void GameOver()
    {
        speed = 0.0F;
        //UnityEngine.Debug.Log("Setting speed to " + speed);
        gameRunning = false;
        
    }

    


}
