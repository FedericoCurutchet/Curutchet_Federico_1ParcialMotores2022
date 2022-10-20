using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotDispara : MonoBehaviour
{
    private GameObject jugador;
    public int rapidez;
    public TMPro.TMP_Text TextoGameOver;
    public GameObject proyectil;
   
  


    void Start()
    {

        jugador = GameObject.Find("jugador");
        TextoGameOver.text = "";

    }

    private void Update()
    {

        if (gameObject.CompareTag("Llave") == true)
        {
            transform.LookAt(jugador.transform);
            transform.Translate(rapidez * Vector3.forward * Time.deltaTime);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("jugador"))
        {


        }

    }

}
