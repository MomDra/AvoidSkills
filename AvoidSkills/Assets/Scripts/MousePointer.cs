using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointer : MonoBehaviour
{
    private static MousePointer instance;
    public static MousePointer Instance { get => instance; }

    private Camera mainCamera;
    private MeshCollider meshCollider;
    public Vector3 MousePositionInWorld { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            mainCamera = Camera.main;
            meshCollider = GameObject.Find("Ground").GetComponentInChildren<MeshCollider>();
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        ScreenMousePointer();
    }

    private void ScreenMousePointer()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (meshCollider.Raycast(ray, out hit, 10000f))
        {
            MousePositionInWorld = hit.point + new Vector3(0, 1, 0);
        }
    }
}
