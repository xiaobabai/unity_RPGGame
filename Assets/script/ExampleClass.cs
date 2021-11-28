using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ExampleClass : MonoBehaviour
{
    public Image image;

    void Update()
    {        
        if (Input.GetMouseButtonDown(0))//点击鼠标左键;
        {
            bool isUI = RectTransformUtility.RectangleContainsScreenPoint(image.GetComponent<RectTransform>(), Input.mousePosition);//判断是否在规定ui里面点击
            if (isUI)
            {
                print("在ui里面点击");
            }
            else
                print("其他部位发生点击");
            if (EventSystem.current.IsPointerOverGameObject())//只要是在摄像机里的ui点击就触发.
                print("第2种方法判断ui点击");

            print("点击");
        }
       
    }
}
