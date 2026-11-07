using UnityEngine;

public class PlayMenuDELETARTESTE : MonoBehaviour
{
    [SerializeField] string cenaUm;
    public void OnPlay()
    {
        TransitionEffect_Script.ChangeScene(cenaUm);
    }
}
