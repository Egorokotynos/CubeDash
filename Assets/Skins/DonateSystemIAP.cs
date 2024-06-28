using System;
using UnityEngine;
using UnityEngine.Purchasing;

namespace CubeDash.Assets.Skins
{
    public class DonateSystemIAP : MonoBehaviour, IStoreListener
    {
        public static DonateSystemIAP SYSTEM;



        private IStoreController storeController;
        private IExtensionProvider extensionProvider;

        public static readonly string SKIN1_PRODUCT_ID = "cube.skin1";
        public static readonly string SKIN2_PRODUCT_ID = "cube.skin2";

        public static readonly string MONEY_200_ID = "gold.cube.200";
        public static readonly string MONEY_500_ID = "gold.cube.500";

        private void Awake()
        {
            if (SYSTEM)
            {
                Destroy(gameObject);
                return;
            }
            else
                SYSTEM = this;

            DontDestroyOnLoad(gameObject);

            InitPurchases();
        }
        private void InitPurchases()
        {
            Debug.Log("InitPurchases called.");
            if (IsInitialized())
            {
                return;
            }

            var storeBuilder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            storeBuilder.AddProduct(SKIN1_PRODUCT_ID, ProductType.NonConsumable);
            storeBuilder.AddProduct(SKIN2_PRODUCT_ID, ProductType.NonConsumable);

            storeBuilder.AddProduct(MONEY_200_ID, ProductType.Consumable);
            storeBuilder.AddProduct(MONEY_500_ID, ProductType.Consumable);

            UnityPurchasing.Initialize(this, storeBuilder);
        }


        public void BuyProduct(string productId, Action<string> OnBuyAfterTrue)
        {
            Debug.Log($"BuyProduct called for {productId}.");
            if (IsInitialized())
            {
                Product product = storeController.products.WithID(productId);

                if (product != null && product.availableToPurchase)
                {
                    Debug.Log(string.Format("BUY PRODUCT RN:" + product.definition.id.ToString()));
                    storeController.InitiatePurchase(product);
                    OnBuyAfterTrue?.Invoke(productId);
                }
                else
                {
                    Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                }
            }
            else
            {
                Debug.Log("BuyProductID FAIL. Not initialized.");
            }
        }

        private bool IsInitialized() => storeController != null && extensionProvider != null;

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            storeController = controller;
            extensionProvider = extensions;
            Debug.Log("OnInitialized called.");
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            Debug.Log($"OnInitializeFailed InitializationFailureReason: {error}, Message: {message}");
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            throw new NotImplementedException();
        }

        public Product GetProductByUID(string nameIdProd)
        {
            return storeController.products.WithID(nameIdProd);
        }
    }
}