using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

namespace CubeDash.Assets.Skins
{
    public class DonateSystemIAP : MonoBehaviour, IStoreListener
    {
        public static DonateSystemIAP SYSTEM;


        public static event Action<string> S_BuyActionPurchaseProduct;



        private IStoreController storeController;
        private IExtensionProvider extensionProvider;
        private Product _s_ProductNow;


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


        public void BuyProduct(string productId)
        {
            Debug.Log($"BuyProduct called for {productId}.");
            if (IsInitialized())
            {
                Product product = storeController.products.WithID(productId);

                if (product != null && product.availableToPurchase)
                {
                    Debug.Log(string.Format("BUY PRODUCT RN:" + product.definition.id.ToString()));
                    storeController.InitiatePurchase(product);
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
        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
            _s_ProductNow = args.purchasedProduct;

            // Example condition to check if the purchase is complete
            bool isPurchaseComplete = VerifyPurchase(args.purchasedProduct);

            if (isPurchaseComplete)
            {
                Debug.Log($"ProcessPurchase: Complete. Product: {args.purchasedProduct.definition.id} - {_s_ProductNow.transactionID}");
                // Grant the item to the user here
                GrantPurchasedItem(args.purchasedProduct);
                return PurchaseProcessingResult.Complete;
            }
            else
            {
                Debug.Log($"ProcessPurchase: Pending. Product: {args.purchasedProduct.definition.id} - {_s_ProductNow.transactionID}");
                // Handle the pending purchase here if needed
                HandlePendingPurchase(args.purchasedProduct);
                return PurchaseProcessingResult.Pending;
            }
        }

        private bool VerifyPurchase(Product product)
        {
            // Add your verification logic here
            // This could be a server-side verification or any other logic you need
            return true; // Assume purchase is verified for this example
        }

        private void GrantPurchasedItem(Product product)
        {
            S_BuyActionPurchaseProduct?.Invoke(product.definition.id);
            Debug.Log($"GrantPurchasedItem: {product.definition.id}");
            //_onBuyActionPurchaseProduct = null;
        }

        private void HandlePendingPurchase(Product product)
        {
            //_onBuyActionPurchaseProduct = null;
        }

        public Product GetProductByUID(string nameIdProd)
        {
            return storeController.products.WithID(nameIdProd);
        }
        public void OnRestorePurchases()
        {
            extensionProvider.GetExtension<IAppleExtensions>().RestoreTransactions(result =>
            {

                if (result)
                {
                    Debug.Log("Restore purchases succeeded.");
                    Dictionary<string, object> parameters = new Dictionary<string, object>()
                    {
                    { "restore_success", true },
                    };
                    //AnalyticsService.Instance.CustomData("myRestore", parameters);
                }
                else
                {
                    Debug.Log("Restore purchases failed.");
                    Dictionary<string, object> parameters = new Dictionary<string, object>()
                    {
                    { "restore_success", false },
                    };
                    // AnalyticsService.Instance.CustomData("myRestore", parameters);
                }

                // AnalyticsService.Instance.Flush();
            });

        }
    }
}