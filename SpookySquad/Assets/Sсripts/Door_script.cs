using System.Collections;
using UnityEngine;


//---------------- Скрипт для всех дверей ----------------//

public class DoorScript : MonoBehaviour
{
    public enum OpenType { Rotate, Translate }
    public OpenType openType;

    public enum DoorAxis { X, Y, Z }
    public DoorAxis doorAxis;

    public bool onlyOpen;

    public bool canBeOpenedNow;

    private bool isOpen;
    private bool openCloseInProgress;

    public float openSpeed = 150f;
    public float openDistOrAngle = 90f;

    public AudioSource moveOrRotSound;
    public AudioSource openSound;
    public AudioSource closeSound;
    public AudioSource notOpeningSound;

    private Quaternion startRotation;
    private Vector3 startPosition;

    public Collider interactionCollider; 

    void Start()
    {
        if (openType == OpenType.Translate)
        {
            startPosition = transform.localPosition;
        }
        else
        {
            startRotation = transform.localRotation;
        }
    }


    void Update()
    {
        if (canBeOpenedNow && !openCloseInProgress)
        {
            if (Input.GetMouseButtonDown(0)) // ЛКМ
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit) && hit.collider == interactionCollider)
                {
                    OpenClose();
                }
            }
        }
    }

    public void OpenClose()
    {
        if (moveOrRotSound) moveOrRotSound.Play();

        isOpen = !isOpen;

        if (isOpen && openSound) openSound.Play();
        if (!isOpen && closeSound) closeSound.Play();

        openCloseInProgress = true;

        if (canBeOpenedNow)
        {
            canBeOpenedNow = false;
            StartCoroutine(EnableOpening());
        }
        else
        {
            if (notOpeningSound) notOpeningSound.Play();
            Debug.Log("Closed!!!");
        }
    }

    IEnumerator EnableOpening()
    {
        yield return new WaitForSeconds(1.0f);
        canBeOpenedNow = true;
        openCloseInProgress = false;
    }

    void FixedUpdate()
    {
        if (openCloseInProgress)
        {
            if (openType == OpenType.Translate)
            {
                HandleTranslation();
            }
            else
            {
                HandleRotation();
            }
        }
    }

    void HandleTranslation()
    {
        float targetDistance = isOpen ? openDistOrAngle : 0f;
        float step = openSpeed * Time.fixedDeltaTime;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, startPosition + GetTranslationDirection() * targetDistance, step);

        if (Vector3.Distance(transform.localPosition, startPosition + GetTranslationDirection() * targetDistance) < 0.001f)
        {
            StopOpenClose();
        }
    }

    void HandleRotation()
    {
        float targetAngle = isOpen ? openDistOrAngle : 0f;
        float step = openSpeed * Time.fixedDeltaTime;
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, startRotation * Quaternion.Euler(GetRotationAxis() * targetAngle), step);

        if (Quaternion.Angle(transform.localRotation, startRotation * Quaternion.Euler(GetRotationAxis() * targetAngle)) < 0.001f)
        {
            StopOpenClose();
        }
    }

    Vector3 GetTranslationDirection()
    {
        switch (doorAxis)
        {
            case DoorAxis.X:
                return Vector3.right;
            case DoorAxis.Y:
                return Vector3.up;
            case DoorAxis.Z:
                return Vector3.forward;
            default:
                return Vector3.right;
        }
    }

    Vector3 GetRotationAxis()
    {
        switch (doorAxis)
        {
            case DoorAxis.X:
                return Vector3.right;
            case DoorAxis.Y:
                return Vector3.up;
            case DoorAxis.Z:
                return Vector3.forward;
            default:
                return Vector3.right;
        }
    }

    void StopOpenClose()
    {
        openCloseInProgress = false;
        if (moveOrRotSound) moveOrRotSound.Stop();
    }
}
