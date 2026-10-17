using Unity.Cinemachine;
using UnityEngine;

public class VisualLaunch_Script : MonoBehaviour
{
    [SerializeField] lauchScript lauchSpecs;
    [SerializeField] CinemachineBasicMultiChannelPerlin noiseCam;
    [SerializeField] LineRenderer linha;
    [SerializeField] Transform mousePos, referenceDist;
    [SerializeField] float MaxAmp;

    void Update()
    {
        if(lauchSpecs.isHolding)
        {
            linha.positionCount = 2;
            linha.SetPosition(0, referenceDist.position);
            linha.SetPosition(1, mousePos.position);
            noiseCam.AmplitudeGain = (Vector2.Distance(mousePos.position, referenceDist.position))/MaxAmp;
        }
        else
        {
            linha.positionCount = 0;
            noiseCam.AmplitudeGain = 0f;
        }
    }
}
