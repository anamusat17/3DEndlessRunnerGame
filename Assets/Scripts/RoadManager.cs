using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// pt a genera o infinitate de trasee
public class RoadManager : MonoBehaviour
{
    public GameObject[] roadPrefabs; // vector cu traseele prestabilite
    public float zSpawn = 0; // pozitia z
    public float roadLenght = 30; // dimensiunea unui traseu
    public int numberOfRoad = 4; // nr de trasee care vor aparea 
    
    
    void Start()
    {
        for(int i = 0; i< numberOfRoad; i++)
        {
            if (i == 0)      // pt ca primul traseu sa fie cel default, fara obstacole
                SpawnRoad(0);   
            else
                SpawnRoad(Random.Range(0, roadPrefabs.Length)); // generarea random a urmatorului traseu
        }
    }

    // se genereaza trasee cat timp personajul merge inainte
    public Transform playerTransform;
    void Update()
    {
        if (playerTransform.position.z - 35 > zSpawn-(numberOfRoad * roadLenght)) // se scade 35 pt a asigura personajului o zona singura, sa nu cada in momentul in care traseul peste care trece se sterge
        {
            SpawnRoad(Random.Range(0, roadPrefabs.Length));
            DeleteRoad(); 
        }
    }

    private List<GameObject> activeRoads = new List<GameObject>(); // lista cu traseele active
    public void SpawnRoad(int roadIndex)
    {
        GameObject instantiateRoad = Instantiate(roadPrefabs[roadIndex], transform.forward * zSpawn, transform.rotation);
        activeRoads.Add(instantiateRoad); // adaug in lista traseele
        zSpawn += roadLenght;
    }

    // stregerea traseelor care au fost, pt a nu incarca suplimentar
    private void DeleteRoad()
    {
        Destroy(activeRoads[0]);
        activeRoads.RemoveAt(0);
    }
}
