using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SquareColorTest : MonoBehaviour
{
    public float delayAmount;
    private SpriteRenderer sr;
    private float delay;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (delay < 0)
        {
            sr.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            delay = delayAmount;
        } else {
            delay -= Time.deltaTime;
        }
    }
}
