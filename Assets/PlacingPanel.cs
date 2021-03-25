using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingPanel : MonoBehaviour
{
    private PlaceHolderGroupController activeController;


    public void Activate(PlaceHolderGroupController controller)
    {
        activeController = controller;
    }

    public void EndPlacement()
    {
        activeController.FixatePlaceHolder();
        activeController.DisableController();
    }
}
