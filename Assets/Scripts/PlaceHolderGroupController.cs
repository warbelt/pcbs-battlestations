using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class PlaceHolderGroupController : MonoBehaviour
{
    [SerializeField] private List<Placeholder> m_placeholderElements = new List<Placeholder>();
    [SerializeField] private bool m_isEnabled = false;

    [Header("Placed Item")]
    [SerializeField] private GameObject m_PlacedItemPrefab;
    private GameObject m_PlacedItemInstance;

    [Header("Preview Item")]
    [SerializeField] private GameObject m_PreviewItemPrefab;
    private GameObject m_PreviewItemInstance;

    private GameObject m_activePlacedItem;
    private GameObject m_activePreviewItem;
    private CinemachineVirtualCamera m_activeVirtualCamera;

    public CinemachineVirtualCamera vcam;

    private void Start()
    {

        foreach (Placeholder placeholder in m_placeholderElements)
        {
            placeholder.OnMouseEnterEvent += Placeholder_OnMouseEnterEventHandler;
            placeholder.OnMouseExitEvent += Placeholder_OnMouseExitEventHandler;
            placeholder.OnMouseDownEvent += Placeholder_OnMouseDownEventHandler;
        }

        m_PlacedItemInstance = Instantiate(m_PlacedItemPrefab);
        m_PlacedItemInstance.SetActive(false);
        m_PreviewItemInstance = Instantiate(m_PreviewItemPrefab);
        m_PreviewItemInstance.SetActive(false);
    }

    private void Placeholder_OnMouseEnterEventHandler(Placeholder placeholder)
    {
        placeholder.toggleOn();
        
        GameObject placeHolderOverride = placeholder.getOverridePreviewItem();
        m_activePreviewItem = placeHolderOverride ? placeHolderOverride : m_PreviewItemInstance;

        m_activePreviewItem.transform.position = placeholder.transform.position;
        m_activePreviewItem.transform.rotation = placeholder.transform.rotation;
        m_activePreviewItem.SetActive(true);
    }

    private void Placeholder_OnMouseExitEventHandler(Placeholder placeholder)
    {
        placeholder.toggleOff();
        m_activePreviewItem.SetActive(false);
    }

    private void Placeholder_OnMouseDownEventHandler(Placeholder placeholder)
    {
        // Set previos active to inactive in case it was an override
        if (m_activePlacedItem)
        {
            m_activePlacedItem.SetActive(false);
        }


        GameObject placeHolderOverride = placeholder.getOverridePlacedItem();
        m_activePlacedItem = placeHolderOverride ? placeHolderOverride : m_PlacedItemInstance;

        m_activePlacedItem.transform.position = placeholder.transform.position;
        m_activePlacedItem.transform.rotation = placeholder.transform.rotation;

        m_activePlacedItem.SetActive(true);

        CinemachineVirtualCamera vcam = placeholder.getOverrideVirtualCamera();
        if (vcam != null)
        {
            if (m_activeVirtualCamera != null)
            {
                m_activeVirtualCamera.Priority = 10;
            }

            vcam.Priority = 100;
            m_activeVirtualCamera = vcam;
        }
    }

    public void SetPreviewPrefab(GameObject previewPrefab)
    {
        m_PreviewItemInstance = Instantiate(previewPrefab);
    }


    public void SetPlacePrefab(GameObject placePrefab)
    {
        m_PlacedItemInstance = Instantiate(placePrefab);
    }
}
