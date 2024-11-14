using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Screen_Shot_Manager : MonoBehaviour
{
    [Header("Genel")]
    public Camera myCamera;
    public string fileName = "Sprite";

    [Header("SS bölgesi")]
    [Tooltip("SS'den kesilip icon olarak kullanılacak bölge")]
    public int textureStartWidth = Screen.width / 2 - 100;
    public int textureFinishWidth = Screen.width / 2 + 100;
    public int textureStartHeight = Screen.height / 2 - 100;
    public int textureFinishHeight = Screen.height / 2 + 100;

    [Header("Icon yapılacak objeler")]
    [Tooltip("İlk objenin yeri kameraya göre düzgün ayarlanmalı.")]
    public List<GameObject> allObjects = new List<GameObject>();

    [ContextMenu("Create List Icon")]
    public void MakeListIcon()
    {
        if (string.IsNullOrEmpty(fileName))
        {
            Debug.LogError("fileName can not be Empty.");
            return;
        }
        if (textureFinishWidth < textureStartWidth)
        {
            Debug.LogError("Texture genişliğinin bitiş noktası başlangıcından büyük olmalıdır.");
            return;
        }
        if (textureFinishHeight < textureStartHeight)
        {
            Debug.LogError("Texture yüksekliğinin bitiş noktası başlangıcından büyük olmalıdır.");
            return;
        }
        StartCoroutine(ListIcon());
    }
    IEnumerator ListIcon()
    {
        if (allObjects.Count > 0)
        {
            yield return new WaitForEndOfFrame();

            // Ekran görüntüsünü al ve gösterilen klasöre koy
            var baseIconTexture = ScreenCapture.CaptureScreenshotAsTexture();
            // Obje için istenen boyutta texture oluştur
            var iconTexture = new Texture2D(textureFinishWidth - textureStartWidth, textureFinishHeight - textureStartHeight, TextureFormat.RGBA32, false);
            // Objenin bulunduğu alandaki renkleri öğren
            Color[] allColor = baseIconTexture.GetPixels(textureStartWidth, textureStartHeight, textureFinishWidth - textureStartWidth, textureFinishHeight - textureStartHeight);
            // Texture dosyasının min ve max arasında kalan kısımlarını al yeni texture kaydet
            iconTexture.SetPixels(0, 0, textureFinishWidth - textureStartWidth, textureFinishHeight - textureStartHeight, allColor);

            yield return new WaitForEndOfFrame();

            string path = Application.dataPath + "/Bomber Family/Sprite/" + fileName + "/";
            byte[] screenIconArray = iconTexture.EncodeToPNG();
            File.WriteAllBytes(path + "Icon_" + allObjects[0].name + ".png", screenIconArray);

            yield return new WaitForEndOfFrame();

            Destroy(baseIconTexture);
            UnityEditor.AssetDatabase.Refresh();
        }
        else
        {
            Debug.LogError("Object list is empty.");
        }
    }
    public void DeleteFirstObject()
    {
        Destroy(allObjects[0]);
        allObjects.RemoveAt(0);
        allObjects[0].SetActive(true);
    }
    public void SetFalseAllObject()
    {
        allObjects.ForEach(x => x.SetActive(false));
    }
}