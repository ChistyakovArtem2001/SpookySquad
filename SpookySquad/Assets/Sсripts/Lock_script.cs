using System.Collections;
using UnityEngine;

//---------------- Скрипт для кодвого замка ----------------//

public class Lock_script : MonoBehaviour
{
    public Transform camera_TR;
    private RaycastHit hit;
    public AudioSource button_sound;
    public AudioSource lock_open_sound;
    public AudioSource wrong_code_sound;
    public DoorScript _Door;
    public LayerMask ray_layermask;

    public string userDefinedPassword = "1111"; 
    private string entered_password = "";
    private string password;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        password = userDefinedPassword;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(camera_TR.position, camera_TR.TransformDirection(Vector3.forward), out hit, 5, ray_layermask))
            {
                if (hit.collider.tag == "CodelockBtn")
                {
                    if (hit.collider.name != "enter")
                    {
                        if (entered_password.Length < password.Length)
                        {
                            entered_password += hit.collider.name;
                            print(hit.collider.name);
                            print("entered " + entered_password);
                        }
                    }
                    else
                    {
                        print("СheckPass");
                        if (entered_password == password)
                        {
                            print("OPEN");
                            lock_open_sound.Play();
                            _Door.canBeOpenedNow = true;
                            Destroy(this);
                        }
                        else
                        {
                            wrong_code_sound.Play();
                            print("Uncorrect pass");
                            entered_password = ""; 
                        }
                    }
                    button_sound.Play();
                }
            }
        }
    }
}
