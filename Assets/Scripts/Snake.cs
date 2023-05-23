using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{

    private Vector2 direction = Vector2.right;
    private List<Transform> segments;
    public Transform segmentPrefab;
    private bool canMove = true;
    public int endScene;

    private void Start()
    {
        segments = new List<Transform>();
        segments.Add(transform);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            direction = Vector2.right;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            direction = Vector2.down;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Grow();
        }
    }

    private void FixedUpdate()
    {
        if(canMove)
        {
            for (int i = segments.Count - 1; i > 0; i--)
            {
                segments[i].position = segments[i - 1].position;
            }


            transform.position = new Vector3(Mathf.Round(this.transform.position.x) + direction.x, Mathf.Round(transform.position.y) + direction.y, 0);
        }
    }

    private void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = segments[segments.Count - 1].position;

        segments.Add(segment);
    }
    private void Die()
    {
        canMove = false;
        for(int i = segments.Count-1; i>0; i--)
        {
            Debug.Log(i + " Destroyed");
            Destroy(segments[i].gameObject);
            DestroyDelay();
        }

        SceneManager.LoadScene(endScene);
    }
    IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(.2f);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Food"))
        {
            Grow();
        }
        else if(collision.CompareTag("Wall"))
        {
            Die();
        }
    }
}
