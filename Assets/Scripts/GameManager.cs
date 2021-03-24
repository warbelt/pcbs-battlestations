using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject m_startPanel;
    [SerializeField] private Button m_startButton;

    [SerializeField] private GameObject m_selectDeskPanel;
    [SerializeField] private Button m_selectDeskButton;
    
    [SerializeField] private GameObject m_propsPanel;

    [SerializeField] private PlaceHolderGroupController tablesController;

    private void Start()
    {
        Initialize();
    }

    private void StartGame()
    {
        m_startPanel.SetActive(false);
        m_selectDeskPanel.SetActive(true);
        tablesController.gameObject.SetActive(true);
    }

    private void SelectDesk()
    {
        m_selectDeskPanel.SetActive(false);
        m_propsPanel.SetActive(true);
        tablesController.gameObject.SetActive(false);

    }

    private void Initialize()
    {
        SetListeners();

        m_selectDeskPanel.SetActive(false);
        m_startPanel.SetActive(true);
        tablesController.gameObject.SetActive(false);
    }

    private void SetListeners()
    {
        m_startButton.onClick.AddListener(StartGame);
        m_selectDeskButton.onClick.AddListener(SelectDesk);
    }
}
