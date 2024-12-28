using UnityEngine;

public class Pointer : MonoBehaviour
{

    public LayerMask terrainLayer; // layer który jest fajny do sprawdzania myszy :3
    public Vector3 mousePositionOnMap; //POZYCJA MYSKI >:3

    public bool isOnTerrain = false;
    public bool isOnPoint = false;

    void Update()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit; //typowy raycast który mi ktoœ powiedzia³ ¿e taki jest fajny :3 (nie wiedzia³em o istnieniu Screen Point to ray)


        if (Physics.Raycast(ray, out hit, Mathf.Infinity, terrainLayer))
        {
            mousePositionOnMap = hit.point; //aktualizuje informacje gdzie jest myszka, w zale¿noœci gdzie trafi³ raycast :<
            if (hit.transform.root.tag == "Path")
            {
                isOnPoint = true;
                GetComponent<PathDrawer>().StopDrawing();
            }
            else
            {
                isOnTerrain = true;
                isOnPoint = false;
            }

        }
        else
        {
            isOnTerrain = false;
            isOnPoint = false;
        }
    }
}