using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLaer : MonoBehaviour
{
    public Material material;
    LaserBeam beam;

    void Update()
    {
        Destroy(GameObject.Find("Laser Beam"));
        beam = new LaserBeam(gameObject.transform.position, gameObject.transform.right, material);
    }
}
