using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class lauchScript : MonoBehaviour
{
    [SerializeField] mouseScript MouseState;
    [SerializeField] CinemachineCamera[] cams;
    [SerializeField] Transform mousePos, referenceDist, referenceMin, referenceMax;
    [SerializeField] Transform FuegoParent;
    [SerializeField] GameObject bolaDeFuego;
    [SerializeField] HUD_Points_Script pointAlvo;
    [SerializeField] SpriteRenderer circuloVisual;
    [SerializeField] ParticleSystem circuloFechado;
    bool isWaiting = true, circuloAtivo = false;
    public bool isHolding = false;

    Vector2 dirToMouse;
    float ang;

    //animacoes
    [SerializeField] Animator anim;
    private readonly int hasStartLaunch = Animator.StringToHash("StartLaunch"); 
    private readonly int hasStopLaunch = Animator.StringToHash("StopLaunch");

    void Start()
    {
        cams[0].Priority = 1;
        cams[1].Priority = 0;
    }
    public void OnSummon(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            circuloVisual.transform.position = MouseState.transform.position;
            this.transform.position = MouseState.transform.position;
        }
    }
    void Update()
    {
        if(!isWaiting && !isHolding)
        {
            return;
        }

        dirToMouse = ((Vector2)referenceDist.position - (Vector2)mousePos.position).normalized;
        if(MouseState.IsPressing)
        {
            if(Vector2.Distance(referenceMax.position, mousePos.position) < 
                Vector2.Distance(referenceMax.position, referenceDist.position)
                && (Vector2.Distance(referenceMax.position, mousePos.position) > 
                Vector2.Distance(referenceMax.position, referenceMin.position) || isHolding))
            {
                // PODE GIRAR?
                ang = Mathf.Atan2(dirToMouse.y, dirToMouse.x)*Mathf.Rad2Deg;
                Debug.Log(ang);
                if(ang >= 0 && ang <= 90)
                {
                    if(!circuloAtivo)
                    {
                        circuloVisual.DOKill();
                        circuloVisual.DOFade(0.5f, 0.2f);
                        MouseState.particulaSpawn.Play();
                        circuloAtivo = true;
                    }
                    this.transform.rotation = Quaternion.Euler(0, 0, ang);

                    if(!isHolding) {
                        isHolding = true;
                        if(anim != null) anim.SetTrigger(hasStartLaunch);
                        AudioManager.instance.PlaySound("PrepararBola");
                    }
                }
                else
                {
                    if(isHolding){
                        if(anim != null) anim.SetTrigger(hasStopLaunch);
                        AudioManager.instance.StopSound("PrepararBola");
                        circuloVisual.DOKill();
                        circuloVisual.DOFade(0, 0.2f);
                        MouseState.particulaSpawn.Stop();
                        circuloAtivo = false;
                        isHolding = false;
                    }
                }
            }
            else
            {
                if(isHolding){
                    AudioManager.instance.StopSound("PrepararBola");
                    if(anim != null) anim.SetTrigger(hasStopLaunch);
                    circuloVisual.DOKill();
                    circuloVisual.DOFade(0, 0.2f);
                    MouseState.particulaSpawn.Stop();
                    circuloAtivo = false;
                    isHolding = false;
                }
            }
        }
        else if(isHolding)
        {
            if(Vector2.Distance(mousePos.position, referenceDist.position) > 
                Vector2.Distance(referenceMin.position, referenceDist.position))
            {
                // ATIRARRRR
                isWaiting = false;

                AudioManager.instance.StopSound("PrepararBola");
                circuloFechado.Play();
                AudioManager.instance.PlaySound("LancarBola");

                if(anim != null) anim.SetTrigger(hasStopLaunch);

                GameObject bola = Instantiate(bolaDeFuego, referenceDist.position, Quaternion.identity, 
                    FuegoParent);
                bola.GetComponent<BolaFogo_Script>().Direction = dirToMouse;
                pointAlvo.alvo = bola;
                cams[0].Priority = 0;
                cams[1].Priority = 1;
                cams[1].Follow = bola.transform;
            }else
            {
                AudioManager.instance.StopSound("PrepararBola");
                if(anim != null) anim.SetTrigger(hasStopLaunch);
            }
            circuloVisual.DOKill();
            circuloVisual.DOFade(0, 0.2f);
            MouseState.particulaSpawn.Stop();
            circuloAtivo = false;
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
