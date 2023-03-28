using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    public Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        //Add time to timer
        timer.addTime();

        //Move coin further down by randomly 50-80
        transform.position = new Vector2(transform.position.x + Random.Range(50, 80), transform.position.y);

        //Y is randomly between -1.3 and 3.1
        transform.position = new Vector2(transform.position.x, Random.Range(-1.3f, 3.1f));
    }
}
