using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class IAPManager : MonoBehaviour, IDetailedStoreListener
{

    IStoreController m_StoreController;

    //Adding Product ID
    public string goldProductId = "com.Aurora.InAppPurchasing.Gold50";
    public string diamonProductId = "com.Aurora.InAppPurchasing.Diamond50";


    private void Start()
    {
        InitilizePurchase();
    }

    //Initializing Purchase
    public void InitilizePurchase()
    {
        ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(goldProductId, ProductType.Consumable);
        builder.AddProduct(diamonProductId, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("Initialized Sucessfull");
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("Initialized Failed!");
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.Log("Initialized Failed!");
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
        throw new System.NotImplementedException();
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        throw new System.NotImplementedException();
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        throw new System.NotImplementedException();
    }
}
