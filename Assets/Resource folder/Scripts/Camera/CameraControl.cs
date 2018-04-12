using UnityEngine;
using System.Collections.Generic;

public class CameraControl : MonoBehaviour
{
    public float dampTime = 0.2f;
    public float screenEdgeBuffer = 4f;
    public float minSize = 6.5f;
    public List<GameObject> targets = new List<GameObject>();

    private Camera mainCamera;
    private float zoomSpeed;
    private Vector3 moveVelocity;
    private Vector3 desiredPosition;
    private bool connected;


    private void Start()
    {
        mainCamera = GetComponentInChildren<Camera>();
        
    }

    private void Update()
    {
        List<GameObject> temp = new List<GameObject>();
        if (GameObject.FindGameObjectWithTag("Player")) {
            temp.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        }
        if(temp.Count > 0)
        {
            foreach(GameObject tank in temp)
            {
               if(tank.transform.FindChild("Camera tracker").gameObject.activeInHierarchy)
                {
                    targets.Add(tank);                   
                    tank.transform.FindChild("Camera tracker").gameObject.SetActive(false);
                    connected = true;
                }
            }
        }


        Move();
        Zoom();

    }


    private void Move()
    {
        if(connected == true){
            FindAveragePosition();

            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref moveVelocity, dampTime);
        }
    }


    private void FindAveragePosition()
    {

            Vector3 averagePos = new Vector3();
            int numTargets = 0;

            foreach(GameObject target in targets)
            {
                if (!target.gameObject.activeSelf)
                    continue;

                averagePos += target.transform.position;
                numTargets++;
            }

            if (numTargets > 0)
                averagePos /= numTargets;

            averagePos.y = transform.position.y;

            desiredPosition = averagePos;

    }


    private void Zoom()
    {
        if(connected == true){
            float requiredSize = FindRequiredSize();
            mainCamera.orthographicSize = Mathf.SmoothDamp(mainCamera.orthographicSize, requiredSize, ref zoomSpeed, dampTime);
        }
    }


    private float FindRequiredSize()
    {
        Vector3 desiredLocalPos = transform.InverseTransformPoint(desiredPosition);

        float size = 0f;

        foreach(GameObject target in targets)
        {
            if (!target.gameObject.activeSelf)
                continue;

            Vector3 targetLocalPos = transform.InverseTransformPoint(target.transform.position);

            Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;

            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.y));

            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.x) / mainCamera.aspect);
        }

        size += screenEdgeBuffer;

        size = Mathf.Max(size, minSize);

        return size;
    }


    public void SetStartPositionAndSize()
    {
        FindAveragePosition();

        transform.position = desiredPosition;

        mainCamera.orthographicSize = FindRequiredSize();
    }




}