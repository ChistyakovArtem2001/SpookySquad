using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
 public float mouseSensitivity = 200f;
 
    float xRotation = 0f;
    float YRotation = 0f;
 
    void Start()
    {
      //фиксируем курсор в центре
 Cursor.lockState = CursorLockMode.Locked;
    }
 
    void Update()
    {
       float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
       float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
 
       //вращение по оси X (смотрим вверх и низ)
       xRotation -= mouseY;
 
       //ограничение на вращение
       xRotation = Mathf.Clamp(xRotation, -90f, 90f);
 
       //вращение по оси Y (по сторонам)
       YRotation += mouseX;
 
       //объединение вращения по обеим осям
       transform.localRotation = Quaternion.Euler(xRotation, YRotation, 0f);
 
    }
}
