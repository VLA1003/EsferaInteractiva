using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoRayos : MonoBehaviour
{
    public GameObject objetoAgarrado;
    Vector3 escalaOriginal;

    // Condiciones para detectar los objetos.
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (objetoAgarrado == null)
            {
                SeleccionarObjeto();
            }
            else
            {
                SoltarObjeto();
            }
        }

        if (objetoAgarrado != null)
        {
            MoverObjeto();
        }
    }

    // Función para mover el objeto cuando se ha seleccionado.
    void MoverObjeto()
    {
        Vector3 posicionMoverCubo = Input.mousePosition;
        Ray rayoMoverCubo = Camera.main.ScreenPointToRay(posicionMoverCubo);
        RaycastHit infoToqueMoverCubo;
        objetoAgarrado.SetActive(false);
        if (Physics.Raycast(rayoMoverCubo, out infoToqueMoverCubo) == true)
        {
            objetoAgarrado.transform.position = infoToqueMoverCubo.point + Vector3.up * objetoAgarrado.transform.localScale.y / 2;
        }
        objetoAgarrado.SetActive(true);
    }

    // Función para soltar un objeto que se ha seleccionado.
    void SoltarObjeto()
    {
        LeanTween.cancel(objetoAgarrado);
        LeanTween.scale(objetoAgarrado, escalaOriginal, 0.5f).setEaseInBack();
        objetoAgarrado = null;
        
    }

    // Función para seleccionar un objeto.
    void SeleccionarObjeto()
    {
        Vector3 posicionSeleccion = Input.mousePosition;
        Ray rayoSeleccion = Camera.main.ScreenPointToRay(posicionSeleccion);
        RaycastHit infoSeleccion;
        if (Physics.Raycast(rayoSeleccion, out infoSeleccion) == true)
        {
            if (infoSeleccion.collider.tag.Equals("Agarrable"))
            {
                objetoAgarrado = infoSeleccion.collider.gameObject;
                LeanTween.scale(objetoAgarrado, objetoAgarrado.transform.localScale * 1.2f, 0.5f).setEaseOutBounce().setLoopPingPong();
            }
        }
    }
}

