using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlacer : MonoBehaviour
{
    [SerializeField] public GameObject itemToPlace;
    private GameObject m_placedInstance;
    private bool m_placing;

    private void Start()
    {
        m_placing = true;
    }

    void Update()
    {
        if (m_placing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100))
                {
                    if (m_placedInstance != null)
                    {
                        Destroy(m_placedInstance);
                    }

                    m_placedInstance = Instantiate(itemToPlace);
                    m_placedInstance.transform.parent = hit.transform;
                    m_placedInstance.transform.localPosition = new Vector3(0f, 0f, 0f);
                }
            }

            else
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100))
                {
                    if (m_placedInstance != null)
                    {
                        Destroy(m_placedInstance);
                    }

                    m_placedInstance = Instantiate(itemToPlace);
                    m_placedInstance.transform.parent = hit.transform;
                    m_placedInstance.transform.localPosition = new Vector3(0f, 0f, 0f);
                    m_placedInstance.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
                }
            }
        }

        
    }
}
