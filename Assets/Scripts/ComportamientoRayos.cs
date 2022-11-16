using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoRayos : MonoBehaviour
{
    GameObject objetoAgarrado;
    Transform movimientoAgarrado;
    Vector3 mOffset;
    bool estaAgarrado;

    private void Start()
    {
        movimientoAgarrado = GetComponent<Transform>();
        estaAgarrado = false;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 posicionRaton = Input.mousePosition;
            Ray rayo = Camera.main.ScreenPointToRay(posicionRaton);
            RaycastHit infoToque;
            if (Physics.Raycast(rayo, out infoToque) == true)
            {
                if (infoToque.collider.tag.Equals("Agarrable"))
                {
                    Debug.Log(infoToque.collider.tag);
                    objetoAgarrado = infoToque.collider.gameObject;
                    movimientoAgarrado = infoToque.collider.GetComponent<Transform>();
                    objetoAgarrado.SetActive(false);
                    estaAgarrado = true;
                    if (objetoAgarrado != null || movimientoAgarrado != null)
                    {
                        Ray rayoAgarre = Camera.main.ScreenPointToRay(posicionRaton);
                        RaycastHit infoAgarre;

                        if (estaAgarrado == true)
                        {
                            if (Physics.Raycast(rayoAgarre, out infoAgarre) == true)
                            {
                                mOffset = new Vector3(0f, movimientoAgarrado.position.y, 0f);
                                var tiempoAgarrado = mOffset;
                                objetoAgarrado.SetActive(true);
                                movimientoAgarrado.position = infoToque.point + tiempoAgarrado;
                            }
                        }
                    }
                }
            }
        }
    }
}
