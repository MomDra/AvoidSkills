using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointer : MonoBehaviour
{
    [SerializeField]
    private Camera camera;
    private MeshCollider meshCollider;
    public Vector3 worldPosition;
    private GameObject ground;

    void Start()
    {
        ground = GameObject.Find("Ground");
        meshCollider = ground.GetComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        ScreenMousePointer();
    }

    void ScreenMousePointer(){
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if(meshCollider.Raycast(ray, out hit, 10000)){
            worldPosition = hit.point + new Vector3(0, 1, 0);
        }
    }
}
