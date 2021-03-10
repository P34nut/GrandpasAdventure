using UnityEngine;

public class Item : MonoBehaviour
{

    new public string name = "New Item";
    public string modell;// item name
    public Sprite icon = null;              // item icon

    private void DeactivateItem()
    {
        /*if (gameObject.GetComponent<TrapItem>())
        {
            SendMessageUpwards("ActivateTrap");
            

        }*/
        //gameObject.SetActive(false);
        if (transform.parent != null && transform.parent.tag == "Floor")
        {
            transform.parent.SendMessage("IsNotBlocked");
        }
        //Destroy(gameObject);
        gameObject.SetActive(false);
        if (gameObject.GetComponent<TrapItem>())
        {
            SendMessageUpwards("ActivateTrap");
            Debug.Log("Lösche: " + transform.parent.gameObject.name);
            Destroy(transform.parent.transform.parent.gameObject);


        }

    }



}