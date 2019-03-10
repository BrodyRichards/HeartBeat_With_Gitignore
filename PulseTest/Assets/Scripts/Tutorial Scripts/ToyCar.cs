using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyCar : MonoBehaviour
{
    public Vector3 target;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Control.negativeAction))
        {
            StartCoroutine(GetInTheWay());
        }
    }

    IEnumerator GetInTheWay()
    {
        while(transform.position != target)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "MC")
        {
            Debug.Log("MC stepped on me");
        }
    }
}
