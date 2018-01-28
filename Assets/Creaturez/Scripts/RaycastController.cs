using UnityEngine;

public class RaycastController
{
    private Camera _cam;
    private LayerMask _layer;
    private Vector3 _hitVectorPoint;

    public RaycastController(Camera camera, LayerMask layer)
    {
        _cam = camera;
        _layer = layer;
    }

    public bool IsHit()
    {
        
        Ray ray = _cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 10, _layer))
        {
            if (hit.collider.tag == "Tentacle")
            {
                _hitVectorPoint = hit.point;
                return true;
            }
        }

        return false;
    }

    public Vector3 ReturnHitVectorPoint()
    {
        return _hitVectorPoint;
    }
}
