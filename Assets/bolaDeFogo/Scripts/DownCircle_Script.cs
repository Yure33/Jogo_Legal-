using DG.Tweening;
using UnityEngine;

public class DownCircle_Script : MonoBehaviour
{
    [SerializeField] SpriteRenderer spr;
    void Start()
    {
        var sequence = DOTween.Sequence().OnComplete(() =>
        {
            transform.DOKill();
            spr.DOKill();
            Destroy(gameObject);
        });
        sequence.Append(spr.DOFade(1, 0.1f));
        sequence.Append(transform.DOScale(2, 0.2f));
        sequence.AppendInterval(0.2f);
        sequence.Append(transform.DOScale(0, 0.2f));
        sequence.Append(spr.DOFade(0, 0.1f));
    }
}
