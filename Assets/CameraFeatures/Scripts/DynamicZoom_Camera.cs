using Unity.Cinemachine;
using UnityEngine;

public class DynamicZoom_Camera : MonoBehaviour
{
    public Rigidbody2D bolaDeFuego;
    [SerializeField] CinemachineCamera minhaCam;
    [Header("Configurações de Zoom")]
    [SerializeField] float velToZoomMin;
    [SerializeField] AnimationCurve curvaDeMudança;
    float targetZoom;
    float velSmoothTransition;

    void FixedUpdate()
    {
        if(!bolaDeFuego) return;

        float velocidade = bolaDeFuego.linearVelocity.magnitude;
        Debug.Log(velocidade);
        targetZoom = curvaDeMudança.Evaluate(Mathf.Clamp(velocidade/velToZoomMin, 0, 1));
        minhaCam.Lens.OrthographicSize = Mathf.SmoothDamp(minhaCam.Lens.OrthographicSize, targetZoom, ref velSmoothTransition, 0.8f);
    }
}
