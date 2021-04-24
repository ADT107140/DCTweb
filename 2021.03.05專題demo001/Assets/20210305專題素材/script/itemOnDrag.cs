using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class itemOnDrag : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public Transform originalParent;
    public inventory mybag;
    private int currentItemId;

    

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        currentItemId = originalParent.GetComponent<Slot>().slotId;
        transform.SetParent(transform.parent.parent);
        transform.position = eventData.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(eventData.pointerCurrentRaycast.gameObject != null)
        {
            if (eventData.pointerCurrentRaycast.gameObject.name == "item_Image")
            {
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;
                //itemlist的物品儲存位置改變
                var temp = mybag.itemlist[currentItemId];
                mybag.itemlist[currentItemId] = mybag.itemlist[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotId];
                mybag.itemlist[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotId] = temp;

                eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originalParent.position;
                eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originalParent);
                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }
            if (eventData.pointerCurrentRaycast.gameObject.name == "slot(Clone)")
            {
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;

                mybag.itemlist[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotId] = mybag.itemlist[currentItemId];
                if (eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>().slotId != currentItemId)
                    mybag.itemlist[currentItemId] = null;

                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }
        }
        
        //其他任何位置都歸為
        transform.SetParent(originalParent);
        transform.position = originalParent.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    
}
