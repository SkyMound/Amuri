using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnvilManager : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D col;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        rb = transform.GetChild(0).gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other) {
        Destroy(col);
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

}
