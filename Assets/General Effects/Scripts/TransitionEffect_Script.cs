using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionEffect_Script : MonoBehaviour
{
    public static event Action<string> TerminarCena;
    [SerializeField] Image Open, Close;

    void OnEnable()
    {
        TerminarCena += TransitionAndExit;
    }
    void OnDisable()
    {
        TerminarCena -= TransitionAndExit;
    }
    void Start()
    {
        var sequence = DOTween.Sequence().SetUpdate(true).OnComplete(() =>
        {
            Open.DOKill();
        });
        sequence.AppendInterval(0.2f);
        sequence.Append(Open.rectTransform.DOSizeDelta(new Vector2(2500, 5), 0.5f).SetEase(Ease.OutExpo));
    }

    // Update is called once per frame
    public static void ChangeScene(string which)
    {
        TerminarCena?.Invoke(which);
    }

    void TransitionAndExit(string which)
    {
        var sequence = DOTween.Sequence().SetUpdate(true).OnComplete(() =>
        {
            DOTween.KillAll();
            SceneManager.LoadScene(which);
        });
        sequence.Append(Close.rectTransform.DOSizeDelta(new Vector2(2500, 1600), 0.5f).SetEase(Ease.OutExpo));
        sequence.AppendInterval(0.1f);
    }
}
