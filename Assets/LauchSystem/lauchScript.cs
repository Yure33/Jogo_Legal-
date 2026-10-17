using System;
using Unity.Cinemachine;
using UnityEngine;

public class lauchScript : MonoBehaviour
{
    [SerializeField] mouseScript MouseState;
    [SerializeField] CinemachineCamera[] cams;
    [SerializeField] Transform mousePos, referenceDist, referenceMin;
    [SerializeField] Transform FuegoParent;
    [SerializeField] GameObject bolaDeFuego;
    [SerializeField] HUD_Points_Script pointAlvo;
    bool isWaiting = true;
    public bool isHolding = false;

    Vector2 dirToMouse;
    float ang;

    void Start()
    {
        cams[0].Priority = 1;
        cams[1].Priority = 0;
    }
    void Update()
    {
        if(!isWaiting && !isHolding)
        {

            return;
        }

        dirToMouse = ((Vector2)mousePos.position - (Vector2)this.transform.position).normalized;
        if(MouseState.IsPressing)
        {
            if(Vector2.Distance(this.transform.position, mousePos.position) < 
                Vector2.Distance(this.transform.position, referenceDist.transform.position)
                && (Vector2.Distance(this.transform.position, mousePos.position) > 
                Vector2.Distance(this.transform.position, referenceMin.transform.position) || isHolding))
            {
                // PODE GIRAR?
                ang = Mathf.Atan2(dirToMouse.y, dirToMouse.x)*Mathf.Rad2Deg;
                Debug.Log(ang);
                if(ang >= 0 && ang <= 90)
                {
                    this.transform.rotation = Quaternion.Euler(0, 0, ang);
                    isHolding = true;
                }
                else
                {
                    isHolding = false;
                }
            }
            else
            {
                isHolding = false;
            }
        }
        else if(isHolding)
        {
            if(Vector2.Distance(mousePos.position, referenceDist.position) > 
                Vector2.Distance(referenceMin.position, referenceDist.position))
            {
                // ATIRARRRR
                isWaiting = false;

                GameObject bola = Instantiate(bolaDeFuego, referenceDist.position, Quaternion.identity, 
                    FuegoParent);
                bola.GetComponent<BolaFogo_Script>().Direction = dirToMouse;
                pointAlvo.alvo = bola;
                cams[0].Priority = 0;
                cams[1].Priority = 1;
                cams[1].Follow = bola.transform;
            }
            isHolding = false;
        }
    }

    public void ResetLauch()
    {
        isHolding = false;
        isWaiting = true;
        cams[0].Priority = 1;
        cams[1].Priority = 0;
        cams[1].transform.position = cams[0].transform.position;
    }
}
