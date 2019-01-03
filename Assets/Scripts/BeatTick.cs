using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatTick : MonoBehaviour
{
    public float speed;
    public float direction = 1;

    private float screenX;

    private void Start()
    {
        screenX = Screen.width / 2;
        speed = screenX / (float)FindObjectOfType<SongController>().secondsBetweenBeats * direction;
    }

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        if (direction == 1)
        {
            if (transform.position.x >= screenX)
            {
                Destroy(gameObject);
            }
        }
        else if (direction == -1)
        {
            if (transform.position.x <= screenX)
            {
                Destroy(gameObject);
            }
        }
    }
}
