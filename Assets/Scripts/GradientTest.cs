using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GradientTest : MonoBehaviour
{
    public float GradientSpeed = 0.1f;

    [SerializeField]
    private Gradient _gradient;
    [SerializeField]
    private float _gradientTime = 0;
    private Text _spriteRend;
    void Start()
    {
        _spriteRend = GetComponent<Text>();
    }

    void Update()
    {
        _gradientTime += GradientSpeed * Time.unscaledDeltaTime;
        _gradientTime %= 1f;

        _spriteRend.color = _gradient.Evaluate(_gradientTime);
    }
}
