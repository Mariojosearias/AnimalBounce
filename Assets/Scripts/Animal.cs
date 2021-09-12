using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] bool movingRight;


    float minX, maxX;
    public int HealthPoints;
    bool isDamaged;
    Blink material;
    public SpriteRenderer sprite;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 esquinaInfDer = Camera.main.ViewportToWorldPoint(new Vector2(1, 0));
        Vector2 esquinaInfIzq = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        maxX = esquinaInfDer.x;
        minX = esquinaInfIzq.x;

        sprite = GetComponent<SpriteRenderer>();
        material = GetComponent<Blink>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if (movingRight)
        {
            Vector2 movimiento = new Vector2(speed * Time.deltaTime, 0);
            transform.Translate(movimiento);
        }
        else
        {
            Vector2 movimiento = new Vector2(-speed * Time.deltaTime, 0);
            transform.Translate(movimiento);
        }

        if (transform.position.x >= maxX)
        {
            //Se va a mover a la Izq
            movingRight = false;
        }
        else if (transform.position.x <= minX)
        {
            //Se va a mover a la Der
            movingRight = true;

        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (gameManager.isRunning == true)
            {
                (GameObject.Find("GameManager").GetComponent<GameManager>()).CaptureAnimal();
                Destroy(this.gameObject);
            }
            else
            {
                HealthPoints -= collision.gameObject.GetComponent<Bullet>().DamageDone();

                StartCoroutine(Damager());
                if (HealthPoints <= 0)
                {
                    (GameObject.Find("GameManager").GetComponent<GameManager>()).CaptureAnimal();
                    Destroy(this.gameObject);
                }

                IEnumerator Damager()
                {
                    isDamaged = true;
                    GetComponent<SpriteRenderer>().material = GetComponent<Blink>().blink;
                    yield return new WaitForSeconds(0.5f);
                    isDamaged = false;
                    GetComponent<SpriteRenderer>().material = GetComponent<Blink>().original;
                }

            }

        }
    }
}





    



