using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovimientosJugador : MonoBehaviour
{
    public float rapidezDesplazamiento = 5.0f;
    public float timer = 60;
    public float magnitudSalto;
    public int cont;
    public int y;
    public int x;
    public int z;

    
    public bool doblesalto;
    private Rigidbody rb;
    public LayerMask capaPiso;
   
    public CapsuleCollider col;
    public TMPro.TMP_Text TextoLlave;
    public TMPro.TMP_Text TextoEscapaste;
    public TMPro.TMP_Text TextoTiempo;
    public TMPro.TMP_Text TextoGameOver;
    public TMPro.TMP_Text TextoVida;
   
    public Camera camaraPrimeraPersona;
    public GameObject jugador;
  
    public int hp;



    void Start()
    {
        y = 1;
        x = 1;
        z = 1;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        Cursor.lockState = CursorLockMode.Locked;
        TextoLlave.text = "";
        TextoEscapaste.text = "";
        TextoGameOver.text = "";
        TextoTiempo.text = "";
        TextoVida.text = "";
        hp = 100;
        
        MostrarTextos();
       

    }

    private void MostrarTextos()
    {

        TextoLlave.text = "Llaves: " + cont.ToString();
        TextoEscapaste.text = "";
        TextoVida.text = "Vida: " + hp.ToString();

       

    }

    
    void FixedUpdate()
    {
        float movimientoAdelanteAtras = Input.GetAxis("Vertical") * rapidezDesplazamiento;
        float movimientoCostados = Input.GetAxis("Horizontal") * rapidezDesplazamiento;
        movimientoAdelanteAtras *= Time.deltaTime; movimientoCostados *= Time.deltaTime;
        transform.Translate(movimientoCostados, 0, movimientoAdelanteAtras);
        TextoTiempo.text = "Tiempo: " + Time.time.ToString();

        if (Time.time == 60)
        {
            perdiste();

        }

        if (Input.GetKeyDown(KeyCode.Space) && EstaEnPiso())
        {
    
            rb.AddForce(Vector3.up * magnitudSalto, ForceMode.Impulse);

        }
        else if (doblesalto)
        {
            rb.AddForce(Vector3.up * magnitudSalto, ForceMode.Impulse);
            doblesalto = false;
        }

        if (Input.GetKeyDown("escape"))
        {

            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKeyDown(KeyCode.E)) { 

            Ray ray = camaraPrimeraPersona.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); RaycastHit hit; 
            if ((Physics.Raycast(ray, out hit) == true) && hit.distance < 5) {

                Debug.Log("El rayo tocó al objeto: " + hit.collider.name);
                if ((hit.collider.tag == "Salida") && cont == 1)
                {

                    ganaste();
                }
                

            } 


        }

       


        if (Input.GetKeyDown(KeyCode.R) || transform.position.y <= -20){

            ReiniciarJuego();
        }

        

    }

    private bool EstaEnPiso()
    {
        return Physics.CheckCapsule(col.bounds.center,
            new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), col.radius * .9f, capaPiso);

    }

   

    private void OnTriggerEnter(Collider other) { 

        if (other.gameObject.CompareTag("Llave") == true) {

            
            cont = cont + 1;
            MostrarTextos();
            other.gameObject.SetActive(false);

        }
        if (other.gameObject.CompareTag("PowerUp") == true)
        {
            rapidezDesplazamiento = 10.0f;
            transform.localScale = new Vector3(x, y, z);
            
            other.gameObject.SetActive(false);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Bot"))
        {
            hp -= 25;
            MostrarTextos();

        } else if (collision.gameObject.CompareTag("Gendarmen"))
        {
            hp -= 100;
            MostrarTextos();

        } else if(collision.gameObject.CompareTag("Motorizada"))
        {
            hp -= 50;
            MostrarTextos();

        }

        if (hp <= 0)
        {
            perdiste();
        }


    }

    private void ReiniciarJuego()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void perdiste()
    {
        TextoGameOver.text = "Game Over";
        Time.timeScale = 0;
    }

    private void ganaste()
    {
        TextoEscapaste.text = "GANASTE!!!";
        Time.timeScale = 0;
    }

    
}
