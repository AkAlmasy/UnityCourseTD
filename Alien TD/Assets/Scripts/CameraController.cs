using UnityEngine;

public class CameraController : MonoBehaviour {
    private bool doMovement = true;
    public float panSpeed = 30f; //Kamera mozgási sebessége
    public float panBorderThickness = 400f; //A távolság amennyire az egér legyen a képernyő szélétől, hogy elmozduljon a kamera
   
    
    private float vertical=0;
    private float horizontal=0;

    public float scrollSpeed = 5f;
    public float minY = 5f; //A maximum közelítés értéke (zoomolásnál)
    public float maxY = 25f; //A maximum távolítás értéke (zoomolásnál)

    void Update()
    {

        //Ha Escap-et nyomunk megáll a kamera mozgatása (Tesztelésnél jól jöhet.)
        if (Input.GetKeyDown(KeyCode.Escape))
            doMovement = !doMovement;
        if (!doMovement)
            return;


        //W, A, S, D használata + ha az egér a képernyő széléhez ér akkor is tudjuk mozgatni a kamerát
        if (Input.GetKey("w") && (vertical< panBorderThickness))
        {
            vertical+=.1f;
            //new Vector3(0f, 0f, panSpeed * Time.deltaTime);
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s") && (vertical > -panBorderThickness))
        {
            vertical-=.1f;
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d")&&(horizontal <  panBorderThickness))
        {
            horizontal+=.1f;
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") &&(horizontal > -panBorderThickness))
        {
            horizontal-=.1f;
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }


        //A görgős rész -->(Zoomolás)

        //A változó értéke attól függ, hogy milyen gyorsan görgetünk, (Unityben, Edit -> Project Settings -> Input/Mouse ScrollWheel)
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;

        //scroll értéke alacsony ezért növeljük meg, monduk 1000-el
        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;

        //Nem engedi, hogy túlzoomoljunk a pályán
        pos.y = Mathf.Clamp(pos.y, minY, maxY); 

        transform.position = pos;
    }
}