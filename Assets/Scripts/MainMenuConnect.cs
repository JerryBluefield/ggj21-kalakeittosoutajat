using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuConnect : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] MenuButtonController menuButtonController;

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        menuButtonController.joinGame();
    }
}
