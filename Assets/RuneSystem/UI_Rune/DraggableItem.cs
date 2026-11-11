using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(RuneObjectUI))]
public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    Transform parentAfterDrag;
    private Image imageItem;
    private RuneObjectUI runeUI;

    private bool estaArrastando = false;

    void Awake()
    {
        imageItem = GetComponent<Image>();
        runeUI = GetComponent<RuneObjectUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(estaArrastando) return;

        transform.DOKill();
        transform.DOScale(Vector3.one * 1.1f, 0.2f).SetEase(Ease.OutBack);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(estaArrastando) return;

        transform.DOKill();
        transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutQuad);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin drag");
        estaArrastando = true;

        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();

        imageItem.raycastTarget = false; //desliga o raycast para que o eventsystem detecte o hotbar

        Vector3 escalaOriginal = transform.localScale;

        transform.DOKill();
        transform.DOScale(escalaOriginal * 1.5f, 0.2f).SetEase(Ease.OutBack);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.DOKill();

        transform.SetParent(parentAfterDrag);

        if(EventSystem.current.IsPointerOverGameObject())
        {
            //se achou a UI do proprio hotbar o lancamento é invalido
            transform.DOScale(Vector3.one,0.3f).SetEase(Ease.OutBounce);
        }else
        {
            //se nao, o lancamento eh valido e aplica o efeito da runa
            runeUI.ApplyEffect();
            transform.DOScale(Vector3.one,0.15f).SetEase(Ease.OutQuad);
        }

        //liga o raycast novamente
        imageItem.raycastTarget = true;
        estaArrastando = false;

        Debug.Log("End drag");
    }
}
