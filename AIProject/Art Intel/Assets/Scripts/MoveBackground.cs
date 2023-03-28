using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MoveBackground : MonoBehaviour
{
    private float startPos;
    private GameObject camera;
    [SerializeField] private float parallaxEffect;

    [SerializeField] private float length = 85;


    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("Main Camera");
        startPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float temp = (camera.transform.position.x * (1 - parallaxEffect));
        float distance = (camera.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        if (temp > startPos + length)
        {
            startPos += length * 2;
        }
        else if (temp < startPos - length)
        {
            startPos -= length * 2;
        }
    }
}
