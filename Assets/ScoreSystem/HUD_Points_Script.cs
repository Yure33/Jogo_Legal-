using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class HUD_Points_Script : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtDistance, txtScore;
    [SerializeField] lauchScript lauch;
    float Distance = 0f;
    public GameObject alvo;
    public Vector2 distZero;
    bool AtirandoAgr = true;
    
    void OnEnable()
    {
        BolaFogo_Script.fimDePartida += ShowEndHUD;
    }
    void OnDisable()
    {
        BolaFogo_Script.fimDePartida -= ShowEndHUD;
    }
    void LateUpdate()
    {
        if(alvo != null)
        {
            if(AtirandoAgr)
            {
                distZero = alvo.transform.position;
                AtirandoAgr = false;
            }

            Distance = Vector2.Distance(distZero, alvo.transform.position);
            txtDistance.text = Distance.ToString("F0") + " m";
        }
    }

    void ShowEndHUD()
    {
        Debug.Log("Bola Morreu :(");
        alvo = null;
        AtirandoAgr = true;
        StartCoroutine(EndHUD());
    }

    IEnumerator EndHUD()
    {
        Debug.Log("Mostrando infos..");
        yield return new WaitForSecondsRealtime(1);
        lauch.ResetLauch();
    }
}
