using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;

// Handles the highlighting of the gameobject outline when hovered over

[RequireComponent(typeof(Outline))]
public class Placeholder : MonoBehaviour
{
    [SerializeField] Color m_idleColor;
    [SerializeField] Color m_highlightColor;
    [SerializeField] float m_idleOutlineWidth;
    [SerializeField] float m_highlightOutlineWidth;

    [Header("Overrides")]
    [SerializeField] private GameObject m_OverridePlacedItemPrefab;
    private GameObject m_OverridePlacedItemInstance;
    [SerializeField] private GameObject m_OverridePreviewItemPrefab;
    private GameObject m_OverridePreviewItemInstance;
    [SerializeField] private CinemachineVirtualCamera m_OverrideVCam;
    
    Outline m_outline;

    public event Action<Placeholder> OnMouseEnterEvent;
    public event Action<Placeholder> OnMouseExitEvent;
    public event Action<Placeholder> OnMouseDownEvent;

    private void Awake()
    {
        m_outline = GetComponent<Outline>();


        if (m_OverridePlacedItemPrefab != null)
        {
            m_OverridePlacedItemInstance = Instantiate(m_OverridePlacedItemPrefab);
            m_OverridePlacedItemInstance.SetActive(false);
        }

        if (m_OverridePreviewItemPrefab != null)
        {
            m_OverridePreviewItemInstance = Instantiate(m_OverridePreviewItemPrefab);
            m_OverridePreviewItemInstance.SetActive(false);
        }

        toggleOff();
    }

    private void Start()
    {

    }

    private void OnMouseEnter()
    {
        if(OnMouseEnterEvent != null)
        {
            OnMouseEnterEvent.Invoke(this);
        }
    }

    private void OnMouseExit()
    {
        if (OnMouseExitEvent != null)
        {
            OnMouseExitEvent.Invoke(this);
        }
    }

    private void OnMouseDown()
    {
        if (OnMouseDownEvent != null)
        {
            OnMouseDownEvent.Invoke(this);
        }
    }

    public void toggleOff()
    {
        m_outline.OutlineColor = m_idleColor;
        m_outline.OutlineWidth = m_idleOutlineWidth;
    }

    public void toggleOn()
    {
        m_outline.OutlineColor = m_highlightColor;
        m_outline.OutlineWidth = m_highlightOutlineWidth;
    }

    public GameObject getOverridePlacedItem()
    {
        return m_OverridePlacedItemInstance;
    }

    public GameObject getOverridePreviewItem()
    {
        return m_OverridePreviewItemInstance;
    }

    public CinemachineVirtualCamera getOverrideVirtualCamera()
    {
        return m_OverrideVCam;
    }
}
