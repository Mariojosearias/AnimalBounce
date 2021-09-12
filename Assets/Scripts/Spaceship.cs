using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float FireRate;
    [SerializeField] GameObject Bala;
    [SerializeField] GameObject BalaRafaga;
    [SerializeField] GameObject Disparador;
   
    float minX, maxX, minY, maxY;
    float nextFire = 0;
    float VelocidadDisparo = 0.15f;
    bool cambiarBala = true;
    
    // Start is called before the first frame update
    void Start()
    {
       Vector2 esquinaSupDer = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
       Vector2 puntoMinParaY = Camera.main.ViewportToWorldPoint(new Vector2(0, 0.7f));

        maxX = esquinaSupDer.x - 0.7f;
        maxY = esquinaSupDer.y - 0.7f;
        minX = puntoMinParaY.x + 0.7f;
        minY = puntoMinParaY.y;

              
    }

    // Update is called once per frame
    void Update()
    {
        MoverNave();
        if (cambiarBala)
            Disparar();
        else
            DisparoRafaga();

        if (Input.GetKey(KeyCode.Z))
           cambiarBala = cambiarBala ? false : true;
      
    }
        void DisparoRafaga()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Disparando Rafaga");
        }
        
        if (Input.GetKey(KeyCode.Space) && Time.time > nextFire)
        {
          Instantiate(BalaRafaga, Disparador.transform.position, transform.rotation);
            nextFire = Time.time + VelocidadDisparo;
        }
    }
    void Disparar()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextFire)
        {
            Instantiate(Bala, Disparador.transform.position, transform.rotation);
            nextFire = Time.time + FireRate;
        }
    }
    void MoverNave()
    {
        float movH = Input.GetAxis("Horizontal");
        float movV = Input.GetAxis("Vertical");

        Vector2 movimiento = new Vector2(movH * Time.deltaTime * speed, movV * Time.deltaTime * speed);

        //Aca se mueve
        transform.Translate(movimiento);

        //Aca voy a verificar la posición
        if (transform.position.x > maxX)
        {
            //Devuelvase a MaxX
            transform.position = new Vector2(maxX, transform.position.y);
        }
        if (transform.position.x < minX)
        {
            //Devuelvase a MinX
            transform.position = new Vector2(minX, transform.position.y);
        }
        if (transform.position.y > maxY)
        {
            //Devuelvase a MaxX
            transform.position = new Vector2(transform.position.x, maxY);
        }
        if (transform.position.y < minY)
        {
            //Devuelvase a MinX
            transform.position = new Vector2(transform.position.x, minY);
        }
    }
}
