using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f; // Скорость движения игрока
    public float gravity = -9.81f * 3; // Сила гравитации
    public float jumpHeight = 3f; // Высота прыжка

    public Transform groundCheck; // Точка для проверки, находится ли игрок на земле
    public float groundDistance = 0.4f; // Расстояние для проверки земли
    public LayerMask groundMask; // Слой земли

    Vector3 velocity; // Вектор скорости игрока

    bool isGrounded; // Флаг, указывающий, находится ли игрок на земле

    // Обновление каждый кадр
    void Update()
    {
        // Проверка на приземление (сброс вертикальной скорости)
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Движение
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        // Проверка, находится ли игрок на земле (чтобы не прыгать в воздухе)
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Прыжок
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
