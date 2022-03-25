using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(10,10), ForceMode2D.Impulse);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
