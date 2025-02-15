using UnityEngine;

public class BillBoard : MonoBehaviour
{
    private Camera cam;

    private void Update()
    {
        if (cam == null)
            cam = FindObjectOfType<Camera>();

        if (cam == null) return;

        transform.LookAt(cam.transform);
        transform.Rotate(Vector3.up * 180f);
    }
}
