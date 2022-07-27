using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seasons : MonoBehaviour
{
    public int season = 0;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextSeason()
    {
        season += 1;
        season = season % 4;
        animator.SetInteger("Season", season);
    }
}
