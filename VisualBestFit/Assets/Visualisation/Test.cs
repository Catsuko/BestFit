using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BestFit.Model;
using System.Linq;

public class Test : MonoBehaviour
{
    [SerializeField]
    private Container _container;

    // Start is called before the first frame update
    void Start()
    {
        _container.Display(new Rectangle(32, 43));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
