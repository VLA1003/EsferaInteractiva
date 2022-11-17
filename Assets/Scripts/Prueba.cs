using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prueba : MonoBehaviour
{
    // Enumeración de todos los estados posibles para manipular los objetos.
    public enum EstadoSelector
    {
        EnEspera,
        SeleccionadoMover,
        SeleccionadoRotar,
        SeleccionadoEscalar,
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
    
    //Diferentes estados posibles de los objetos.
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
            case EstadoSelector.Mover:
                MovimientoObjeto();
                break;
            case EstadoSelector.Soltar:
                SoltarObjeto();
                break;
        }
    }
    //Raycast para seleccionar el objeto para mover/rotar/escalar.
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
                    else
                    {
                        estadoActual = EstadoSelector.Mover;
                    }
                }
            }
        }
    }
    //Raycast para mover el objeto seleccionado.
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
