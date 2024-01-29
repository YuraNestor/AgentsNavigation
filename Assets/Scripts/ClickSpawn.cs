using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.EventSystems;
using Zenject;

public class ClickSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnPrefab;
    [SerializeField]
    private Transform parent;

    [Inject] 
    private DiContainer container;  

    // Update is called once per frame
    void Update()
    {        
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("Touch UI");
            }
            else
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;                
                Physics.Raycast(ray, out hit);
                var clone = container.InstantiatePrefab(spawnPrefab, hit.point, Quaternion.identity, parent);                
            }
        }
    }
}
