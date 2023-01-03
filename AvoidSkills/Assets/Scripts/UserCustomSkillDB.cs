using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserCustomSkillDB : MonoBehaviour
{
    [SerializeField]
    private Sprite[] skillImages;

    [SerializeField]
    private Image chosenImage;
    [SerializeField]
    private Image prevImage;
    [SerializeField]
    private Image nextImage;

    private Sprite[] container = new Sprite[3];
    private int size;
    private int centerIndex = 1;

    private void Awake() {
        size = skillImages.Length;
        container[0] = skillImages[0];
        container[1] = skillImages[1];
        container[2] = skillImages[2];
    }

    public void Prev(){

        container[0] = container[1];
        container[1] = container[2];
        if(centerIndex + 2 >= size){
            container[2] = skillImages[centerIndex + 2 - size];
            centerIndex = centerIndex + 2 - size - 1;
        }else{
            container[2] = skillImages[centerIndex + 2];
            ++centerIndex;  
        }
        ContainerUpdate();
    }

    public void Next(){
        container[2] = container[1];
        container[1] = container[0];
        if(centerIndex - 2 < 0){
            container[0] = skillImages[centerIndex - 2 + size];
            centerIndex = centerIndex - 2 + size + 1;
        }else{
            container[0] = skillImages[centerIndex - 2];
            --centerIndex;
        }
        ContainerUpdate();
    }

    private void ContainerUpdate(){
        prevImage.sprite = container[0];
        chosenImage.sprite = container[1];
        nextImage.sprite = container[2];
    }    

}
