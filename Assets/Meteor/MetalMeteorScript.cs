using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MetalMeteorScript : MonoBehaviour
{
    [SerializeField] GameObject smallParticlePrefab;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Instantiate(smallParticlePrefab, other.transform.position, Quaternion.identity);
        }
    }
}
