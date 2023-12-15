using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leash : MonoBehaviour
{
    private LineRenderer line;
    [SerializeField] private Transform _dogNeck, _playerNeck;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
        const float width = .02f;
        line.startWidth = width;
        line.endWidth = width;
        line.material.color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0, _dogNeck.position);
        line.SetPosition(1, _playerNeck.position);
    }
}
