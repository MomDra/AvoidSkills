using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LoginField : MonoBehaviour
{
    EventSystem system;

    [SerializeField]
    private Selectable firstInput;
    [SerializeField]
    private Button sumbitButton;

    void Start()
    {
        system = EventSystem.current;
        firstInput.Select();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift)){
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
            if(next != null){
                next.Select();
            }
        }else if(Input.GetKeyDown(KeyCode.Tab)){
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if(next != null){
                next.Select();
            }
        }else if(Input.GetKeyDown(KeyCode.Return)){
            sumbitButton.onClick.Invoke();
            
        }
    }
}
