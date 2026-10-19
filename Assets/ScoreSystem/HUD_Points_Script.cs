using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class HUD_Points_Script : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtDistance, txtScore, txtSpecs;
    [SerializeField] CinemachineCamera shopCam;
    [Header("Slider References")]
    [SerializeField] Slider reachSlider;
    [SerializeField] int TowerDistance;
    [Header("Obstaculos e Points")]
    [SerializeField] string[] IndexNames;
    public int[] IndexQnt;
    [SerializeField] int[] IndexPoints;
    int points = 0;
    [Header("Demais Referencias")]
    [SerializeField] GameObject[] HUD_Sections; 
    [SerializeField] lauchScript lauch;
    [SerializeField] Animator livroAnime;
    [SerializeField] GameObject lojaOBJ;
    float Distance = 0f;
    public GameObject alvo;
    public Vector2 distZero;
    bool AtirandoAgr = true, podePassar = false;
    
    void OnEnable()
    {
        BolaFogo_Script.fimDePartida += ShowEndHUD;
        Obstacle.OnGetPoints += UpdateScore;
    }
    void OnDisable()
    {
        BolaFogo_Script.fimDePartida -= ShowEndHUD;
        Obstacle.OnGetPoints -= UpdateScore;
    }

    public void OnExit()
    {
        DOTween.KillAll();
        Debug.Log("Saindo...");
        Application.Quit();
    }

    void Start()
    {
        //reachSlider.maxValue = TowerDistance;
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
    public void EnterShop(int type)
    {
        if(type == 0)
        {
            shopCam.Priority = 2;
            lauch.enabled = false;
            StartCoroutine(abrirLoja());
        }
        else
        {
            shopCam.Priority = 0;
            lauch.enabled = true;
            livroAnime.SetBool("Abrido", false);
            //StartCoroutine(fecharLoja());
        }
    }

    IEnumerator abrirLoja()
    {
        yield return new WaitUntil(() => lojaOBJ.activeSelf);
        livroAnime.SetBool("Abrido", true);
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
            //reachSlider.value = Distance;
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
        yield return new WaitForSecondsRealtime(0.3f);
        txtSpecs.text += "\n";
        txtSpecs.text += "Learning Points: " + points;
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money")+points);
        yield return new WaitUntil(() => 
            podePassar
        );
        TransitionEffect_Script.ChangeScene(SceneManager.GetActiveScene().name);
    }

    private void UpdateScore(int points)
    {
        Debug.Log("Pontos recebidos: " + points);
    }
}
