using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Capture : MonoBehaviour
{
    int photoWidth = 1920, photoHeight = 1080;
    [SerializeField] float screenshotPanelVanishWaitSeconds = 1;
    [SerializeField] float screenshotPanelVanishFadeSeconds = 1;
    
    public Camera renderCam;
    public GameObject phototohud;
    public Canvas canvas;
    
    private bool m_TakingPicture = false;
    private GameObject screenshotPanelInstance = null;

    public void TakePicture()
    {
        m_TakingPicture = true;
    }

    public void LateUpdate()
    {
        if (m_TakingPicture)
        {
            RenderTexture rt = new RenderTexture(photoWidth, photoHeight, 24);
            renderCam.targetTexture = rt;
            RenderTexture.active = rt;
            renderCam.Render();
            Texture2D screenShot = new Texture2D(photoWidth, photoHeight, TextureFormat.RGB24, false);
            screenShot.ReadPixels(new Rect(0, 0, photoWidth, photoHeight), 0, 0);
            screenShot.Apply();

            renderCam.targetTexture = null;

            screenshotPanelInstance = Instantiate(phototohud);
            screenshotPanelInstance.GetComponentsInChildren<Image>()[1].sprite = Sprite.Create(screenShot, new Rect(0, 0, photoWidth, photoHeight), new Vector2(0, 0));
            screenshotPanelInstance.transform.SetParent(canvas.transform, false);

            SaveLocalImage(screenShot);

            Camera.main.targetTexture = null;
            RenderTexture.active = null; // JC: added to avoid errors
            Destroy(rt);

            StartCoroutine(VanishScreenshotPanel());

            m_TakingPicture = false;
        }
    }

    // This function would save the picture as a PNG in a local folder
    // Only works in windows right now
    private void SaveLocalImage(Texture2D imageData)
    {
        try
        {
            byte[] imageBytes = imageData.EncodeToPNG();
            string picturesFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyPictures);
            System.IO.File.WriteAllBytes(picturesFolder + "/pcbs_battlestations_" + System.DateTime.Now.ToString("ddMMyydd-HHmmss") + ".png", imageBytes);
            ;
        }
        catch
        {
            throw new System.Exception("Unable to save picture");
        }
    }

    private IEnumerator VanishScreenshotPanel()
    {
        Image panelImage = screenshotPanelInstance.GetComponent<Image>();
        float elapsed = 0;

        yield return new WaitForSeconds(screenshotPanelVanishWaitSeconds);

        while (elapsed < screenshotPanelVanishFadeSeconds)
        {
            float newAlpha = Mathf.Lerp(1, 0, elapsed / screenshotPanelVanishFadeSeconds);
            Color newColor = new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, newAlpha);
            panelImage.color = newColor;
            elapsed += Time.deltaTime;

            yield return 0;
        }

        Destroy(panelImage.gameObject);
    }
}
