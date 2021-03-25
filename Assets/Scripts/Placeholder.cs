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

    // STATE
    bool m_isFixated = false;
    GameObject fixatedGameObject;

    private void Awake()
    {
        m_outline = GetComponent<Outline>();
        m_outline.enabled = false;

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

        ActivatePreview();
    }

    private void OnMouseEnter()
    {
        if(OnMouseEnterEvent != null & !m_isFixated)
        {
            OnMouseEnterEvent.Invoke(this);
        }
    }

    private void OnMouseExit()
    {
        if (OnMouseExitEvent != null && !m_isFixated)
        {
            OnMouseExitEvent.Invoke(this);
        }
    }

    private void OnMouseDown()
    {
        if (OnMouseDownEvent != null && !m_isFixated)
        {
            OnMouseDownEvent.Invoke(this);
        }
    }

    public void ActivatePreview()
    {
        m_outline.OutlineColor = m_idleColor;
        m_outline.OutlineWidth = m_idleOutlineWidth;
    }

    public void DeactivatePreview()
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

    public void Fixate(GameObject gameObject)
    {
        fixatedGameObject = gameObject;
        m_isFixated = true;
    }

    public void Clear()
    {
        Destroy(fixatedGameObject);
        fixatedGameObject = null;
        m_isFixated = false;
    }

    public void EnableOutline()
    {
        m_outline.enabled = true;
    }

    public void DisableOutline()
    {
        m_outline.enabled = false;
    }
}
