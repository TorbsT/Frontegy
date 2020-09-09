using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] Vector2 fovLimits;
    [SerializeField] Vector2 horizontalAngleLimits;
    [SerializeField] Vector2 verticalAngleLimits;
    [SerializeField] float height;
    [SerializeField] bool orthographic = false;
    [SerializeField] float cameraPositionSpeed = 1f;
    [SerializeField] float cameraRotationSpeed = 1f;
    [SerializeField] float cameraFovSpeed = 1f;


    [Range(30f, 90f)]
    [SerializeField] float verticalAngle;
    [Range(0f, 359f)]
    [SerializeField] float horizontalAngle;
    float fov;
    Camera camera;
    float circleRadius;
    float zoomSpeed;
    
    void Start()
    {
        camera = GetComponent<Camera>();
    }
    void Update()
    {
        if (orthographic)
        {
            camera.orthographic = true;
            Quaternion newRotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
            transform.localRotation = Quaternion.Lerp(transform.localRotation, newRotation, cameraRotationSpeed);
            Vector3 newPosition = new Vector3(0f, height, 0f);
            transform.position = Vector3.Lerp(transform.position, newPosition, cameraPositionSpeed);
        }
        else if (!orthographic)
        {
            camera.orthographic = false;
            horizontalAngle += Time.deltaTime * -Input.GetAxis("Horizontal") * 200f;
            if (horizontalAngle < 0f) horizontalAngle += 360f;
            else if (horizontalAngle >= 360f) horizontalAngle -= 360f;
            verticalAngle += Time.deltaTime * Input.GetAxis("Vertical") * 20f;
            verticalAngle = Mathf.Clamp(verticalAngle, verticalAngleLimits[0], verticalAngleLimits[1]);
            Quaternion newRotation = Quaternion.Euler(new Vector3(verticalAngle, horizontalAngle, 0f));
            transform.localRotation = Quaternion.Lerp(transform.localRotation, newRotation, cameraRotationSpeed);

            circleRadius = AdvancedMafs(verticalAngle, transform.position.y);
            Vector2 periferalVector = GetPeriferalVector(horizontalAngle, circleRadius);

            Vector3 newPosition = new Vector3(periferalVector[0], height, periferalVector[1]);
            transform.position = Vector3.Lerp(transform.position, newPosition, cameraPositionSpeed);
            fov = GetFov();
            camera.fieldOfView = Maffs.FloatLerp(camera.fieldOfView, fov, cameraFovSpeed);
        }
        /*
        zoomSpeed = zoomSpeedLimits[0];
        zoom = (transform.position.y-zoomLimits[0]) / (zoomLimits[1] - zoomLimits[0]);
        transform.position += new Vector3(Time.deltaTime*Input.GetAxis("Horizontal")*GetPanSpeed(), 0f, Time.deltaTime*Input.GetAxis("Vertical")*GetPanSpeed());
        transform.position += Time.deltaTime * transform.forward * Input.GetAxis("Mouse ScrollWheel")*zoomSpeed;
        if (transform.position.y < zoomLimits[0]) transform.position = new Vector3(transform.position.x, zoomLimits[0], transform.position.z);
        else if (transform.position.y > zoomLimits[1]) transform.position = new Vector3(transform.position.x, zoomLimits[1], transform.position.z);
        */
        if (Input.GetKeyDown("o")) orthographic = !orthographic;
    }
    float GetFov()
    {
        float x = fovLimits[0] + GetRelativeAngle()*(fovLimits[1] - fovLimits[0]);
        return x;
    }
    float GetRelativeAngle()
    {
        return (verticalAngle - verticalAngleLimits[0]) / (verticalAngleLimits[1] - verticalAngleLimits[0]);
    }
    Vector2 GetPeriferalVector(float degs, float mag)
    {
        float rads = GetRadians(degs);
        Vector2 a = Vector2.up; // a: base vector to rotate degs degrees
        float x = a[0];
        float y = a[1];
        Vector2 returnVector = new Vector2(x * Mathf.Cos(rads) + y * Mathf.Sin(rads), -x * Mathf.Sin(rads) + y * Mathf.Cos(rads));
        returnVector *= mag;
        return returnVector;
    }
    float AdvancedMafs(float degs, float h)
    {
        float x;
        x= Mathf.Tan(GetRadians(degs+90)) * h;
        return x;
    }
    float GetRadians(float degs)
    {
        return (degs * Mathf.PI / 180f);
    }
}
