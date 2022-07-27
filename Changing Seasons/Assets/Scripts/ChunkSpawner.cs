using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkSpawner : MonoBehaviour
{
    public List<GameObject> chunks; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerExit2D(Collider2D other)
    {


        if (other.gameObject.tag == "ChunkSpawnerTrigger")
        {
            int rand = Random.Range(0, chunks.Count); //generate random number
            Instantiate(chunks[rand], this.transform.position, this.transform.rotation);

        }
        

    }
}
