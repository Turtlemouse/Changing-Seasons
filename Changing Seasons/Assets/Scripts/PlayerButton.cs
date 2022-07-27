using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerButton : MonoBehaviour
{
    
    public bool buttonPressed;
    public float holdTime = 0.00F;
    public float minHoldTime = 0.30F;
    public float cycleTime = 0.80F;
    public float cycleTimeDec = 0.03F;
    private float curCycleTime;
    public float squashAmount;

    float originalScale;
    float allPosLost;
    Transform trans;
    int chargeUp;

    bool gameOver = false;
    public Animator animator;

    void Start()
    {
        originalScale = animator.gameObject.transform.localScale.y;
        trans = animator.gameObject.transform;
    }
    void FixedUpdate()
    {
        if (buttonPressed == true)
        {
            holdTime += 0.02F;
        }
        //cycle if you hold for the minimum hold time
        if (holdTime > minHoldTime && holdTime < cycleTime)
        {
            holdTime = cycleTime; //so that it does a full cycle time next time
        }
        if (holdTime > curCycleTime && (holdTime % curCycleTime < 0.03))
        {
            HoldContinue();
            curCycleTime -= cycleTimeDec;
        }

        if (gameOver)
        {
            Unsquash();
        }
    }

    //track inputs (down and up of clicking or space)
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Press();
        }
        if (Input.GetKeyUp("space"))
        {
            Release();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Press();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Release();
    }

    //determine what each press and release does
    public void Press()
    {
        buttonPressed = true;
        holdTime = 0.00F;
        curCycleTime = cycleTime;
    }

    public void Release()
    {
        buttonPressed = false;
        if (holdTime <= minHoldTime)
        {
            Click();
        }
        else
        {
            HoldRelease();
        }
    }

    //what actually happens
    void Click()
    {
        if (!gameOver)
        {
            //Jump(0);
            ChangeSeasons();
            
        }
        
    }

    void HoldContinue()
    {

        if (!gameOver)
        {
            //ChangeSeasons();
            //squash down
            Squash();
            chargeUp += 1;
            //ChangeSeasons();
        }
        
    }

    void HoldRelease()
    {
        //set season
        //UnityEngine.Debug.Log("Set Season");
        //Click();

        if (gameOver)
        {
            //restart game
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            
            Jump(chargeUp);
        }
    }

    public void GameOver()
    {
        //GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        gameOver = true;
        animator.SetTrigger("Game Over");
        holdTime = 0.0F;
        buttonPressed = false;
        Unsquash();
    }

    void Jump(int charge)
    {
        Unsquash();
        this.GetComponent<Jump>().pressJump(charge);
    }

    void ChangeSeasons()
    {
        this.GetComponent<Seasons>().nextSeason();
    }

    void Squash()
    {
        /*
        float posLost = 0.5f * trans.position.y * (1.0f - squashAmount);
        allPosLost += posLost;
        Vector3 posChange = new Vector3(trans.position.x, trans.position.y - posLost, trans.position.z);
        trans.position = posChange;


        Vector3 scaleChange = new Vector3(trans.localScale.x, trans.localScale.y - squashAmount, trans.localScale.z);
        trans.localScale = scaleChange;
        */
        this.GetComponent<Jump>().holdJump();

    }

    void Unsquash()
    {
        /*
        Vector3 scaleChange = new Vector3(trans.localScale.x, originalScale, trans.localScale.z);
        trans.localScale = scaleChange;

        Vector3 posChange = new Vector3(trans.position.x, trans.position.y - allPosLost, trans.position.z);
        trans.position = posChange;
        */
        this.GetComponent<Jump>().squasher.SetBool("Squash", false);
    }




}
