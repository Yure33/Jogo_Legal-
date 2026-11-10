using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(RuneObjectUI))]
public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform parentAfterDrag;
    private Image imageItem;
    private RuneObjectUI runeUI;

    void Awake()
    {
        imageItem = GetComponent<Image>();
        runeUI = GetComponent<RuneObjectUI>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin drag");
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();

        imageItem.raycastTarget = false; //desliga o raycast para que o eventsystem detecte o hotbar
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(EventSystem.current.IsPointerOverGameObject())
        {
            //se achou a UI do proprio hotbar o lancamento é invalido
            transform.SetParent(parentAfterDrag);
        }else
        {
            //se nao, o lancamento eh valido e aplica o efeito da runa
            runeUI.ApplyEffect();
            transform.SetParent(parentAfterDrag); //retorna a carta para o hotbar
        }

        //liga o raycast novamente
        imageItem.raycastTarget = true;
        Debug.Log("End drag");
    }
}
