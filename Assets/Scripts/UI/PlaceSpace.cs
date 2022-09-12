using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaceSpace : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        SelectorController.GetInstance().validPlaceSpace = gameObject;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
        if(SelectorController.GetInstance().validPlaceSpace == gameObject)
        {
            SelectorController.GetInstance().validPlaceSpace = null;
        }
    }

    //public void Update()
    //{
    //    if (EventSystem.current.IsPointerOverGameObject())
    //    {
    //        SelectorController.GetInstance().validPlaceSpace = gameObject;
    //    }
    //    else
    //    {
    //        SelectorController.GetInstance().validPlaceSpace = null;
    //    }
    //}
}
