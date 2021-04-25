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
            //Debug.Log(countdownBeforeZoom);

        }
        else
        {
            //isCounting = false;
            countdownBeforeZoom = timer;
        }
        if (countdownBeforeZoom <= 0)
        {
            if (transform.position == originalPosition)
            {
                Debug.LogError("ZoomIn");
                StartCoroutine(ZoomIn());
                countdownBeforeZoom = timer;
            }
            if (transform.position != originalPosition)
            {
                Debug.LogError("ZoomOut");
                StartCoroutine(ZoomOut());
                countdownBeforeZoom = timer;
            }


        }

        // перемещение камеры

        //transfer = transform.forward * Input.GetAxis("Vertical");
        //transfer += transform.right * Input.GetAxis("Horizontal");
        //transform.position += transfer * speed * Time.deltaTime;
    }
    
    IEnumerator ZoomIn()
    {
        while (currentMovementTime < totalMovementTime)
        {
            if (Vector3.Distance(gameObject.transform.position, lookingGameObject.transform.position) <= 1.7f)
            {
                currentMovementTime = totalMovementTime;
                break;
            }
            transform.position = Vector3.Lerp(originalPosition, lookingGameObject.transform.position, currentMovementTime / totalMovementTime);
            currentMovementTime += Time.deltaTime;
            yield return null;
        }
        currentMovementTime = 0;
        transform.parent = lookingGameObject.transform;
        Debug.LogWarning("countdown FINISH+++++++++++++++++");   
    }

    IEnumerator ZoomOut()
    {
        transform.parent = null;
        while (currentMovementTime < totalMovementTime)
        {
            //if (Vector3.Distance(gameObject.transform.position, lookingGameObject.transform.position) <= 1.7f)
            //{
            //    currentMovementTime = totalMovementTime;
            //    break;
            //}
            transform.position = Vector3.Lerp(transform.position, originalPosition, currentMovementTime / totalMovementTime);
            currentMovementTime += Time.deltaTime;
            yield return null;// Vector3.Distance(originalPosition, lookingGameObject.transform.position);
        }

        currentMovementTime = 0;
        Debug.LogWarning("countdown FINISH+++++++++++++++++");
    }

    public string ReleaseRay()
    {
        // Сам луч
        Ray ray = Camera.main.ScreenPointToRay(Ray_start_position);
        // Запись объекта, в который пришел луч, в переменную
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        if (hit.collider.tag == "1" && transform.position == originalPosition) // ИЗ-ЗА ЭТОГО СНОВА НЕ ЗАПУСКАЕТ ТАЙМЕР
        {
            isCounting = true;
            lookingGameObject = hit.transform.gameObject;
        }
        else if (hit.collider.tag == "category" && transform.position != originalPosition)
        {
            Debug.Log("category");
            isCounting = true;
            //lookingGameObject = hit.transform.gameObject;
        }
        else
        {
            Debug.Log("else");
            isCounting = false;
            countdownBeforeZoom = timer;
        }

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