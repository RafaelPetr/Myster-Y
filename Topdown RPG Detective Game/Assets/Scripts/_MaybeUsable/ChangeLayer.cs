using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLayer : MonoBehaviour
{
    private int startLayer;
    private Vector2 bounds;
    private int changeLayer = 6;
    private Color startColor;

    private void Awake()
    {
        startLayer = GetComponent<SpriteRenderer>().sortingOrder;
        startColor = GetComponent<SpriteRenderer>().color;
        bounds = GetComponent<SpriteRenderer>().bounds.size;
    }

    /*private void FixedUpdate()
    {
        Debug.Log(bounds.x);
        if (Physics2D.OverlapBox(new Vector2(transform.position.x, transform.position.y), new Vector2(bounds.x,bounds.y),90f,9))
        {
            Debug.Log("Dae");
        }
    }*/
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().sortingOrder = changeLayer;
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, startColor.a-0.4f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            GetComponent<SpriteRenderer>().sortingOrder = startLayer;
            GetComponent<SpriteRenderer>().color = startColor;
        }
    }
}
