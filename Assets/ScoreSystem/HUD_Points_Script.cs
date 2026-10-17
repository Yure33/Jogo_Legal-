using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class HUD_Points_Script : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtDistance, txtScore, txtSpecs;
    [Header("Obstaculos e Points")]
    [SerializeField] string[] IndexNames;
    public int[] IndexQnt;
    [SerializeField] int[] IndexPoints;
    int points = 0;
    [Header("Demais Referencias")]
    [SerializeField] GameObject[] HUD_Sections; 
    [SerializeField] lauchScript lauch;
    float Distance = 0f;
    public GameObject alvo;
    public Vector2 distZero;
    bool AtirandoAgr = true, podePassar = false;
    
    void OnEnable()
    {
        BolaFogo_Script.fimDePartida += ShowEndHUD;
    }
    void OnDisable()
    {
        BolaFogo_Script.fimDePartida -= ShowEndHUD;
    }

    void Start()
    {
        HUD_Sections[0].SetActive(true);
        HUD_Sections[1].SetActive(false);
        HUD_Sections[2].SetActive(false);
    }

    public void PassEndScreen(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            podePassar = true;
        }
        if(context.canceled)
        {
            podePassar = false;
        }
    }

    void LateUpdate()
    {
        if(alvo != null)
        {
            if(AtirandoAgr)
            {
                distZero = alvo.transform.position;
                HUD_Sections[0].SetActive(false);
                HUD_Sections[1].SetActive(true);
                HUD_Sections[2].SetActive(false);
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
        HUD_Sections[0].SetActive(false);
        HUD_Sections[1].SetActive(false);
        HUD_Sections[2].SetActive(true);
        int aux = 0;
        txtSpecs.text = "";
        txtSpecs.text += "Distance: " + Distance.ToString("F0") + "m\n";
        yield return new WaitForSecondsRealtime(0.5f);

        foreach(int i in IndexQnt)
        {
            if(i != 0)
            {
                points += i*IndexPoints[aux];
                txtSpecs.text += IndexNames[aux] + ": " + i + "x" + IndexPoints[aux] + "\n";
                yield return new WaitForSecondsRealtime(1f);
            }
            aux++;
        }
        txtSpecs.text += "\n";
        txtSpecs.text += "Total: " + points;
        yield return new WaitUntil(() => 
            podePassar
        );
        lauch.ResetLauch();
        HUD_Sections[0].SetActive(true);
        HUD_Sections[1].SetActive(false);
        HUD_Sections[2].SetActive(false);
    }
}
