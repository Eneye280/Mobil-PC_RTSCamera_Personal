using UnityEngine;
using Cinemachine;

public class CMMovementCamera : MonoBehaviour
{

    [SerializeField] private Camera mainCamera;
    [SerializeField] private CinemachineVirtualCamera cmCameraGeneral;

    [Header("Checker")]
    [SerializeField] private bool isMovementCamera;

    [Header("Min and Max Position Camera")]
    [Range(-250,250)] [SerializeField] private float minPositionX;
    [Range(-250,250)] [SerializeField] private float maxPositionX;
    [Space]     
    [Range(-250,250)] [SerializeField] private float minPositionY;
    [Range(-250,250)] [SerializeField] private float maxPositionY;
    [Space]     
    [Range(-250,250)] [SerializeField] private float minPositionZ;
    [Range(-250,250)] [SerializeField] private float maxPositionZ;

    private Vector3 touchStart;
    private float groundZ;
    private Vector3 directionCamera;


    private void Start()
    {
        ActiveMovementCamera();
    }

    public void MovementCamera()
    {
        if (isMovementCamera)
        {
            if (Input.GetMouseButtonDown(0))
            {
                touchStart = GetWorldPosition(groundZ);
            }

            if (Input.GetMouseButton(0))
            {
                directionCamera = touchStart - GetWorldPosition(groundZ);
                cmCameraGeneral.transform.position += directionCamera;

                LimitCamera();
            }
        }
    }

    private void LimitCamera()
    {
        cmCameraGeneral.transform.position = new Vector3(
            Mathf.Clamp(cmCameraGeneral.transform.position.x, minPositionX, maxPositionX),
            Mathf.Clamp(cmCameraGeneral.transform.position.y, minPositionY, maxPositionY),
            Mathf.Clamp(cmCameraGeneral.transform.position.z, minPositionZ, maxPositionZ)
            );
    }

    private Vector3 GetWorldPosition(float transformZ)
    {
        Ray mousePosition = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.up, new Vector3(0,0,transformZ));

        float distance;
        ground.Raycast(mousePosition, out distance);
        return mousePosition.GetPoint(distance);
    }

    public void ActiveMovementCamera()
    {
        isMovementCamera = true;
    }
    internal void DisableMovementCamera()
    {
        isMovementCamera = false;
    }

}
