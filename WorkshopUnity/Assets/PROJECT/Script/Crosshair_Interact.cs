using UnityEngine;
using UnityEngine.UI;

public class CrosshairInteract : MonoBehaviour
{
    [Header("UI Crosshair")]
    public Image crosshair;
    public float defaultSize = 10f;   // Taille normale
    public float interactSize = 20f;  // Taille quand interactif
    public Color defaultColor = Color.white;  // Couleur normale
    public Color interactColor = Color.green; // Couleur quand interactif
    public float transitionSpeed = 5f; 
    public KeyCode fireKey = KeyCode.Mouse0; // touche clic gauche

    [Header("Raycast")]
    public float rayDistance = 5f; // Distance max pour détecter un objet

    void Update()
    {
        bool canInteract = false;

        // Créer un rayon depuis le centre de la caméra
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        // Vérifier si un objet avec tag "candestroy" est devant
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            if (hit.collider.CompareTag("candestroy"))
            {
                canInteract = true;

                // Si clic gauche, détruire l'objet
                if (Input.GetKeyDown(fireKey))
                {
                    Destroy(hit.collider.gameObject);
                    Debug.Log("Objet détruit : " + hit.collider.name);
                }
            }
        }

        // Modifier le crosshair
        crosshair.color = Color.Lerp(crosshair.color, canInteract ? interactColor : defaultColor, Time.deltaTime * transitionSpeed);
        crosshair.rectTransform.sizeDelta = Vector2.Lerp(
            crosshair.rectTransform.sizeDelta,
            canInteract ? new Vector2(interactSize, interactSize) : new Vector2(defaultSize, defaultSize),
            Time.deltaTime * transitionSpeed
        );

        // Optionnel : afficher le rayon dans la scène pour debug
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, canInteract ? Color.green : Color.red);
    }
}