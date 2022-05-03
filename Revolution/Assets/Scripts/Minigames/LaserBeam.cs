using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam
{
    #region Private Variables 

    public GameObject laserObj;
    private LineRenderer _laser;
    private List<Vector3> _laserIndices = new List<Vector3>();
    GameObject laserGO = GameObject.Find("Laser Pointer");
    Transform m_transform;
    #endregion

    
    public LaserBeam(Vector3 pos, Vector3 dir, Material material)
    {
        _laser = new LineRenderer();
        laserObj = new GameObject();
        laserObj.name = "Laser Beam";

        _laser = this.laserObj.AddComponent(typeof(LineRenderer)) as LineRenderer;
        _laser.startWidth = 0.1f;
        _laser.endWidth = 0.1f;
        _laser.startColor = Color.white;
        _laser.endColor = Color.grey;
        _laser.material = material;

        CastLaser(pos, dir);
    }


    private void CastLaser(Vector3 pos, Vector3 dir)
    {
        _laserIndices.Add(pos);
        m_transform = laserGO.transform;

        Ray ray = new Ray(pos, dir);
        RaycastHit hit;

        
        if (Physics.Raycast(ray, out hit, 30))
        {
            CheckHit(hit, dir);
        }
        else
        {
            _laserIndices.Add(ray.GetPoint(30));
            UpdateLaser();
        }
        /*
        if (Physics.Raycast(m_transform.position, m_transform.transform.right))
        {
            RaycastHit2D _hit = Physics2D.Raycast(laserGO.transform.position, m_transform.transform.right);
            CheckHit(_hit, dir);
        }
        else
        {
            _laserIndices.Add(ray.GetPoint(30));
            UpdateLaser(); 
        }*/
    }

    private void CheckHit(RaycastHit hit, Vector3 direction)
    {
        if (hit.collider.tag.Equals("Mirror"))
        {
            Vector3 pos = hit.point;
            Vector3 dir = Vector3.Reflect(direction, hit.normal);
            CastLaser(pos, dir);
        }
        else
        {
            _laserIndices.Add(hit.point);
            UpdateLaser();
        }
    }

    private void UpdateLaser()
    {
        int count = 0;
        _laser.positionCount = _laserIndices.Count;

        foreach (Vector3 index in _laserIndices)
        {
            _laser.SetPosition(count, index);
            count++;
        }
    }
}
