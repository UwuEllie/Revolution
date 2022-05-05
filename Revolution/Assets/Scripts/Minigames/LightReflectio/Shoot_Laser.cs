using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot_Laser : MonoBehaviour
{
    public Material material;
    Transform m_transform;

    public GameObject laserObj;
    private LineRenderer _laser;
    private List<Vector3> _laserIndices = new List<Vector3>();

    private void Awake()
    {
        m_transform = GetComponent<Transform>();
    }

    public void createLaserBeam(Vector3 pos, Vector3 dir, Material material)
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


    void Update()
    {
        Destroy(GameObject.Find("Laser Beam"));
        createLaserBeam(gameObject.transform.position, gameObject.transform.right, material);

        /*
        if (Physics2D.Raycast(m_transform.position, transform.right))
        {
            RaycastHit2D _hit = Physics2D.Raycast(m_transform.position, transform.right);
            Debug.Log(_hit.transform.gameObject);
            Destroy(GameObject.Find("Laser Beam"));
            Debug.Log("Die");

        }
        else
        {
            Destroy(GameObject.Find("Laser Beam"));
            beam = new LaserBeam(gameObject.transform.position, gameObject.transform.right, material);
            Debug.Log("ugh");
        }*/

    }

    private void CastLaser(Vector3 pos, Vector3 dir)
    {
        _laserIndices.Add(pos);

        Ray ray = new Ray(pos, dir);
        

        /*
        if (Physics.Raycast(ray, out hit, 30))
        {
            CheckHit(hit, dir);
        }
        else
        {
            _laserIndices.Add(ray.GetPoint(30));
            UpdateLaser();
        }*/

        if (Physics2D.Raycast(m_transform.position, transform.right))
        {
            RaycastHit2D _hit = Physics2D.Raycast(m_transform.position, transform.right);
            CheckHit(_hit, dir);
        }
        else
        {
            _laserIndices.Add(ray.GetPoint(30));
            UpdateLaser();
        }
    }

    private void CheckHit(RaycastHit2D hit, Vector3 direction)
    {
        if (hit.transform.gameObject.tag == "Mirror")
        {
            Vector3 pos = hit.point;
            Vector3 dir = Vector3.Reflect(direction, hit.normal);
            //CastLaser(pos, dir);
            testing(pos, dir);
            
            Debug.Log("mirror");
        }
        else
        {
            _laserIndices.Add(hit.point);
            UpdateLaser();
        }
    }

    void testing(Vector3 pos, Vector3 dir)
    {
        _laserIndices.Add(pos);

        Ray ray = new Ray(pos, dir);
        _laserIndices.Add(ray.GetPoint(30));
            UpdateLaser();
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

        _laserIndices.Clear();
    }
}
