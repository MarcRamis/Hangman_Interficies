using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LoadAnim : MonoBehaviour
{
    [SerializeField] private Image loadCircle;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("LoadAnimation", 0, 1.5f);
    }

    private void LoadAnimation()
    {
        loadCircle.transform.DORotate(new Vector3(0, 0, -360), 1, RotateMode.WorldAxisAdd);
    }

}
