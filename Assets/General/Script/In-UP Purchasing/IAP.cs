using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAP : MonoBehaviour
{
 private void Start()
    {
        
    }
    public void OnPurchaseComplete(Product product)
  {
      if (product.definition.id=="com.devcompany.50coin")
      {
        Debug.Log("50 Coin Aldın");
            Shop.Instance.starCount += 50;
            PlayerPrefs.SetInt("StarCount", Shop.Instance.starCount);
            Shop.Instance.starText.text = Shop.Instance.starCount.ToString();
        }
      if (product.definition.id=="com.devcompany.150coin")
      {
            Shop.Instance.starCount += 150;
            PlayerPrefs.SetInt("StarCount", Shop.Instance.starCount);
            Shop.Instance.starText.text = Shop.Instance.starCount.ToString();
            Debug.Log("150 Coin Aldın");
      }
      if (product.definition.id=="com.devcompany.300coin")
      {
            Shop.Instance.starCount += 300;
            PlayerPrefs.SetInt("StarCount", Shop.Instance.starCount);
            Shop.Instance.starText.text = Shop.Instance.starCount.ToString();
            Debug.Log("300 Coin Aldın");
      }
      if (product.definition.id=="com.DevCompany.NoAds")
      {
        Debug.Log("Reklamlar Kapatıldı");        
      }
  }

   public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
  {
    Debug.Log("Ürün adı" + product + reason + "Sebebiyle Satın Alınamadı");
  }
}
