using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WU.Exploration;

namespace WU.Player
{
    public class CrosshairInteract : MonoBehaviour
    {
        [Header("UI Crosshair")]
        public Image crosshair;
        public float defaultSize = 0.5f;
        public float interactSize = 20f;
        public Color defaultColor = Color.white;
        public Color interactColor = Color.green;
        public float transitionSpeed = 5f;
        public KeyCode fireKey = KeyCode.Mouse0;
        public float rayDistance = 5f;
    
        public TextMeshProUGUI messageText; 
        public float messageDuration = 2f;  // Durée totale 
        public float fadeDuration = 1f;     // Durée fade out

        private float messageTimer;
        private Color originalColor;
    
        void Start()
        {
            if (messageText != null)
            {
                originalColor = messageText.color;
                messageText.text = "";
                messageText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
            }
        }
        void Update()
        {
            bool canInteract = false;

            // Debug raycast
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            
            HandleRaycast(ref canInteract, ray);
            UpdateCrosshair(canInteract);
            UpdateMessageFade();
            Debug.DrawRay(ray.origin, ray.direction * rayDistance, canInteract ? Color.green : Color.red);
        }


        // Raycast
        private void HandleRaycast(ref bool canInteract, Ray ray)
        {
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, rayDistance))
            {
                if (hit.collider.CompareTag("candestroy"))
                {
                    canInteract = true;

                    if (Input.GetKeyDown(fireKey))
                    {
                        DestroyableObject obj = hit.collider.GetComponent<DestroyableObject>();
                        string msg = obj != null ? obj.messageOnDestroy : "Objet détruit !";

                        ShowMessage(msg);
                        Destroy(hit.collider.gameObject);

                        Debug.Log("Objet détruit : " + hit.collider.name);
                    }
                }
            }
        }

        // Crosshair
        private void UpdateCrosshair(bool canInteract)
        {
            crosshair.color = Color.Lerp(
                crosshair.color,
                canInteract ? interactColor : defaultColor,
                Time.deltaTime * transitionSpeed
            );

            crosshair.rectTransform.sizeDelta = Vector2.Lerp(
                crosshair.rectTransform.sizeDelta,
                canInteract ? new Vector2(interactSize, interactSize) : new Vector2(defaultSize, defaultSize),
                Time.deltaTime * transitionSpeed
            );
        }

        // Text
        private void ShowMessage(string msg)
        {
            if (messageText != null)
            {
                messageText.text = msg;
                messageText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1);
                messageTimer = messageDuration;
            }
        }

        // TEXT + Fade out
        private void UpdateMessageFade()
        {
            if (messageText == null || messageTimer <= 0)
                return;

            messageTimer -= Time.deltaTime;

            // Durée avant fade out
            if (messageTimer > fadeDuration)
            {
                messageText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1);
            }
            else
            {
                float alpha = Mathf.Clamp01(messageTimer / fadeDuration);
                Color c = messageText.color;
                c.a = alpha;
                messageText.color = c;
            }
        }
    }
}
