using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prueba : MonoBehaviour
{
    //Vector2 de posición del ratón.
    Vector2 mousePos;

    //Prefabs para crear objetos nuevos.
    public GameObject NuevoCubo;
    public GameObject NuevaEsfera;
    public GameObject NuevoCilindro;

    // Enumeración de todos los estados posibles para manipular los objetos.
    public enum EstadoSelector
    {
        EnEspera,
        SeleccionadoMover,
        SeleccionadoRotar,
        SeleccionadoEscalar,
        SeleccionadoCrearCubo,
        SeleccionadoCrearEsfera,
        SeleccionadoCrearCilindro,
        CrearCubo,
        CrearEsfera,
        CrearCilindro,
        Mover,
        Rotar,
        Escalar,
        Soltar,
    }

    // Menú que nos muestra en el inspector el estado actual en el que nos encontramos.
    [SerializeField]
    EstadoSelector estadoActual = EstadoSelector.EnEspera;
    
    // Objeto que manipularemos.
    public GameObject objetoAgarrado;


    //Función para enumerar los estados de creación de cubos, esferas y cilindros.
    public void CreacionObjetos()
    {
        switch (estadoActual)
        {
            case EstadoSelector.CrearCubo:
                CrearCubo();
                break;
            case EstadoSelector.CrearEsfera:
                CrearEsfera();
                break;
            case EstadoSelector.CrearCilindro:
                CrearCilindro();
                break;
        }
    }

    //Función para enumerar los estados de movimiento, rotación, escalado y espera en objetos ya existentes.
    void Update()
    {
        switch (estadoActual)
        {
            case EstadoSelector.EnEspera:
                estadoActual = EstadoSelector.EnEspera;
                break;
            case EstadoSelector.SeleccionadoMover:
                SeleccionObjeto();
                break;
            case EstadoSelector.SeleccionadoRotar:
                SeleccionObjeto();
                break;
            case EstadoSelector.SeleccionadoEscalar:
                SeleccionObjeto();
                break;
            case EstadoSelector.Rotar:
                RotarObjeto();
                break;
            case EstadoSelector.Escalar:
                EscalarObjeto();
                break;
            case EstadoSelector.Mover:
                MovimientoObjeto();
                break;
            case EstadoSelector.Soltar:
                SoltarObjeto();
                break;
        }
    }
    //Función para lanzar un rayo para seleccionar el objeto a mover/rotar/escalar.
    void SeleccionObjeto()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 posicionSeleccion = Input.mousePosition;
            Ray rayoSeleccion = Camera.main.ScreenPointToRay(posicionSeleccion);
            RaycastHit infoSeleccion;
            if (Physics.Raycast(rayoSeleccion, out infoSeleccion) == true)
            {
                if (infoSeleccion.collider.tag.Equals("Agarrable"))
                {
                    objetoAgarrado = infoSeleccion.collider.gameObject;
                    if (estadoActual == EstadoSelector.SeleccionadoRotar)
                    {
                        estadoActual = EstadoSelector.Rotar;
                    }
                    else if (estadoActual == EstadoSelector.SeleccionadoEscalar)
                    {
                        estadoActual = EstadoSelector.Escalar;
                    }
                    else if (estadoActual == EstadoSelector.SeleccionadoMover)
                    {
                        estadoActual = EstadoSelector.Mover;
                    }
                }
            }
        }
    }
    
    //Función para lanzar un rayo para mover el objeto seleccionado.
    void MovimientoObjeto()
    {
        {
            Vector3 posicionMovimiento = Input.mousePosition;
            Ray rayoMovimiento = Camera.main.ScreenPointToRay(posicionMovimiento);
            RaycastHit infoMovimiento;
            objetoAgarrado.SetActive(false);
            if (Physics.Raycast(rayoMovimiento, out infoMovimiento) == true)
            {
                objetoAgarrado.transform.position = infoMovimiento.point + Vector3.up * 1.5f * objetoAgarrado.transform.localScale.y / 2;
            }
            objetoAgarrado.SetActive(true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            estadoActual = EstadoSelector.Soltar;
        }
    }

    //Función para rotar el objeto seleccionado.
    void RotarObjeto()
    {
        Vector2 mouseDelta = mousePos - (Vector2)Input.mousePosition;

        objetoAgarrado.transform.Rotate(mouseDelta.y, mouseDelta.x, 0f);

        mousePos = Input.mousePosition;
        
        if (Input.GetMouseButtonUp(0))
        {
            estadoActual = EstadoSelector.EnEspera;
        }
    }

    //Función para escalar el objeto seleccionado.
    void EscalarObjeto()
    {
        objetoAgarrado.transform.localScale += Vector3.one * Input.mouseScrollDelta.y;

        if (Input.GetMouseButtonUp(0))
        {
            estadoActual = EstadoSelector.EnEspera;
        }
    }

    // Función para crear un cubo nuevo con un color aleatorio.
    public void CrearCubo()
    {
        Vector3 posicionNuevoCubo = Input.mousePosition;
        Ray rayoNuevoCubo = Camera.main.ScreenPointToRay(posicionNuevoCubo);
        RaycastHit infoNuevoCubo;
        if (Physics.Raycast(rayoNuevoCubo, out infoNuevoCubo) == true)
        {
           GameObject cuboCreado = Instantiate(NuevoCubo, new Vector3(0, 1, 0), Quaternion.identity);
           cuboCreado.GetComponent<MeshRenderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        }
    }

    // Función para crear una esfera nueva con un color aleatorio.
    public void CrearEsfera()
    {
        Vector3 posicionNuevaEsfera = Input.mousePosition;
        Ray rayoNuevaEsfera = Camera.main.ScreenPointToRay(posicionNuevaEsfera);
        RaycastHit infoNuevaEsfera;
        if (Physics.Raycast(rayoNuevaEsfera, out infoNuevaEsfera) == true)
        {
            GameObject esferaCreada = Instantiate(NuevaEsfera, new Vector3(0, 1, 0), Quaternion.identity);
            esferaCreada.GetComponent<MeshRenderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        }
    }

    // Función para crear un cilindro nuevo con un color aleatorio.
    public void CrearCilindro()
    {
        Vector3 posicionNuevoCilindro = Input.mousePosition;
        Ray rayoNuevoCilindro = Camera.main.ScreenPointToRay(posicionNuevoCilindro);
        RaycastHit infoNuevoCilindro;
        if (Physics.Raycast(rayoNuevoCilindro, out infoNuevoCilindro) == true)
        {
            GameObject cilindroCreado = Instantiate(NuevoCilindro, new Vector3(0, 1.5f, 0), Quaternion.identity);
            cilindroCreado.GetComponent<MeshRenderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        }
    }

    //Función para soltar el objeto en movimiento.
    void SoltarObjeto()
    {
        estadoActual = EstadoSelector.EnEspera;
        objetoAgarrado = null;
    }

    //Función para cambiar el estado a seleccionado para mover el objeto.
    public void CambioEstadoSeleccionMover()
    {
        estadoActual = EstadoSelector.SeleccionadoMover;
    }

    //Función para cambiar el estado a seleccionado para rotar el objeto.
    public void CambioEstadoSeleccionRotar()
    {
        estadoActual = EstadoSelector.SeleccionadoRotar;
    }

    //Función para cambiar el estado a seleccionado para escalar el objeto.
    public void CambioEstadoSeleccionEscalar()
    {
        estadoActual = EstadoSelector.SeleccionadoEscalar;
    }
}
