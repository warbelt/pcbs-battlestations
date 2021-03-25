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

    [SerializeField] private GameObject leftDeskPanel;
    [SerializeField] private Placeholder leftDeskPlaceholder;
    [SerializeField] private GameObject rightDeskPanel;
    [SerializeField] private Placeholder rightDeskPlaceholder;

    private void Start()
    {
        Initialize();
    }

    private void StartGame()
    {
        m_startPanel.SetActive(false);
        m_selectDeskPanel.SetActive(true);
        tablesController.gameObject.SetActive(true);
        tablesController.EnableController();

        //Disable start until one desk is chosen
        m_selectDeskButton.gameObject.SetActive(false);


        leftDeskPlaceholder.OnMouseDownEvent += activateLeftPanel;
        rightDeskPlaceholder.OnMouseDownEvent += activateRightPanel;
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
    }

    private void SetListeners()
    {
        m_startButton.onClick.AddListener(StartGame);
        m_selectDeskButton.onClick.AddListener(SelectDesk);
    }

    public void EnablePlaceholderGroup(PlaceHolderGroupController phgroup, GameObject placedItemOveerride = null, GameObject previewItemOverride = null)
    {
        phgroup.EnableController();
        if (placedItemOveerride != null)
        {
            phgroup.SetPlacePrefab(placedItemOveerride);
        }
        if (previewItemOverride != null)
        {
            phgroup.SetPlacePrefab(previewItemOverride);
        }
    }

    private void activateLeftPanel(Placeholder _)
    {
        leftDeskPanel.SetActive(true);
        rightDeskPanel.SetActive(false);
        m_selectDeskButton.gameObject.SetActive(true);
    }

    private void activateRightPanel(Placeholder _)
    {
        leftDeskPanel.SetActive(false);
        rightDeskPanel.SetActive(true);
        m_selectDeskButton.gameObject.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
