using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogPersonajeP : MonoBehaviour
{

    public float velocidadMovimiento = 5.0f;
    public float velocidadRotacion = 200.0f;
    public float x,y;
    public Animator anim;
    public Rigidbody rb;
    public float fuerzaSalto = 8f;
    public bool puedoSaltar;
    public float velocidadInicial;
    public float velocidadAgachado;
    public CapsuleCollider colParado;
    public CapsuleCollider colAgachado;
    public GameObject cabeza;
    public LogicaCabeza logicaCabeza;
    public bool estoyAgachado;
    // Start is called before the first frame update
    void Start()
    {
        puedoSaltar = false;
        anim = GetComponent<Animator>();
        velocidadInicial = velocidadMovimiento;
        velocidadAgachado = velocidadMovimiento * 0.5f;
    }


    void FixedUpdate()
    {
        transform.Rotate(0, x*Time.deltaTime*velocidadRotacion, 0);
        transform.Translate(0,0, y*Time.deltaTime*velocidadMovimiento); 
    }
    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        

        anim.SetFloat("VelX", x);
        anim.SetFloat("VelY", y);


        if(puedoSaltar)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetBool("Salte", true);
                rb.AddForce(new Vector3(0, fuerzaSalto, 0), ForceMode.Impulse);
            }


            if(Input.GetKey(KeyCode.LeftControl))
            {
                anim.SetBool("Agachado", true);
                velocidadMovimiento = velocidadAgachado;
                colAgachado.enabled = true;
                colParado.enabled = false;

                cabeza.SetActive(true);
                estoyAgachado = true;
            }
            else
            {
                if(logicaCabeza.contadorColision <= 0)
                {
                    anim.SetBool("Agachado", false);
                    velocidadMovimiento = velocidadInicial;

                    cabeza.SetActive(false);
                    colAgachado.enabled = false;
                    colParado. enabled = true;
                    estoyAgachado = false; 
                }
                
            }
            anim.SetBool("TocaSuelo", true);
        } 
        else
        {
            Cayendo();
        }
    }

    public void Cayendo()
    {
        anim.SetBool("TocaSuelo", false);
        anim.SetBool("Salte", false);
    }
}