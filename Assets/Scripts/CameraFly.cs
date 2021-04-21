using UnityEngine;
using System.Collections;
using System;

public class CameraFly : MonoBehaviour
{


    public float mouseSensitivity = 3.0f;
    public float speed = 200;
    private Vector3 transfer;

    public float minimumX = -360F;
    public float maximumX = 360F;
    public float minimumY = -60F;
    public float maximumY = 60F;
    float rotationX = 0F;
    float rotationY = 0F;
    Quaternion originalRotation;
    Vector3 originalPosition;
    Vector3 newPosition;
    GameObject lookingGameObject;

    bool isCounting = false;
    float timer = 2.2f;
    float countdownBeforeZoom;
    float currentMovementTime = 0f;
    float totalMovementTime = 2f;

    Vector3 Ray_start_position = new Vector3(Screen.width / 2, Screen.height / 2, 0);

    void Awake()
    {
        GetComponent<Camera>().orthographic = false;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        originalRotation = transform.rotation;
        originalPosition = transform.position;
        countdownBeforeZoom = timer;
    }

    void Update()
    {
        // Движения мыши -> Вращение камеры
        rotationX += Input.GetAxis("Mouse X") * mouseSensitivity;
        rotationY += Input.GetAxis("Mouse Y") * mouseSensitivity;
        rotationX = ClampAngle(rotationX, minimumX, maximumX);
        rotationY = ClampAngle(rotationY, minimumY, maximumY);
        Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
        Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, Vector3.left);
        transform.rotation = originalRotation * xQuaternion * yQuaternion;

        ReleaseRay();

        if (isCounting)
        {
            countdownBeforeZoom -= Time.deltaTime;
            Debug.LogWarning(countdownBeforeZoom);
            
        }
        else
        {
            isCounting = false;
            countdownBeforeZoom = timer;
        }
        if (countdownBeforeZoom <= 0)
        {
            StartCoroutine(ZoomTo());
            //isCounting = false;
            //currentMovementTime += Time.deltaTime;
            //transform.position = Vector3.Lerp(originalPosition, originalPosition - new Vector3(1f, 1f, 1f), currentMovementTime/totalMovementTime);
            ////countdown = timer;
            //Debug.LogWarning("countdown FINISH+++++++++++++++++");

        }

        // перемещение камеры

        //transfer = transform.forward * Input.GetAxis("Vertical");
        //transfer += transform.right * Input.GetAxis("Horizontal");
        //transform.position += transfer * speed * Time.deltaTime;
    }

    IEnumerator ZoomTo()
    {
        isCounting = false;
        while(originalPosition != newPosition)
        {
            currentMovementTime += Time.deltaTime;
            transform.position = Vector3.Lerp(originalPosition, originalPosition - new Vector3(1f, 1f, 1f), currentMovementTime / totalMovementTime);
        }
        
        //countdown = timer;
        Debug.LogWarning("countdown FINISH+++++++++++++++++");
        yield return null;
    }
    public string ReleaseRay()
    {
        // Сам луч
        Ray ray = Camera.main.ScreenPointToRay(Ray_start_position);
        // Запись объекта, в который пришел луч, в переменную
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        if (hit.collider.tag == "1" && transform.position == originalPosition)
        {
            isCounting = true;
            lookingGameObject = gameObject;
            //while (isCounting)
            //{
            //    countdown -= Time.deltaTime;
            //    Debug.LogWarning(countdown);
            //}
        }
        else
        {
            isCounting = false;
            countdownBeforeZoom = timer;
        }
        //if (countdown <= 0)
        //{
        //    isCounting = false;
        //    countdown = timer;
        //    Debug.LogWarning("countdown FINISH+++++++++++++++++");
        //    //transform.position = new Vector3(0, 0, 0);
        //}
        return hit.collider.tag;
    }

    void CountdownAndZoom()
    {

    }

    //IEnumerator DelayBeforeZoom()
    //{
    //    string tagReleaseRay = ReleaseRay();
    //    yield return new WaitForSeconds(2f);
    //    if (ReleaseRay() == tagReleaseRay)
    //    {
    //        ZoomTo();
    //    }
    //}

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F) angle += 360F;
        if (angle > 360F) angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }

}