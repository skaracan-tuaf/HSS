using System;
using System.Collections;
using UnityEngine;

public enum Formation
{
    Box,
    Trail,
    LineAbreast,
    Column,
    Wedge,
    Echelon,
    Ball,
    Arc,
    Tube
}

public class DroneController : MonoBehaviour
{
    public Transform[] drones; // Drone'lar

    public float moveSpeed = 5f; // Drone'ların hareket hızı

    public float gridSpacing = 25f; //ızgara aralığı
    public float targetHeight;
    public float ascentSpeed = 100f;
    public bool isAscending = false;

    public float tubeRadius = 5f;
    public float tubeHeight = 50f;

    private Formation _formation;

    private void Start()
    {
        drones = new Transform[transform.childCount];

        for (int i = 0; i < drones.Length; i++)
        {
            drones[i] = transform.GetChild(i);
        }
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.T))
        {
            isAscending = true;
            MoveDronesUp();

            //targetHeight += heightChangeAmount;
            //for (int i = 0; i < drones.Length; i++)
            //    targetHeight += drones[i].position.y + heightChangeAmount;

            //StartCoroutine(MoveDronesUpSmoothly());
        }
        else
        {
            isAscending = false;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveDronesTowardsTarget();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            PlaceObjectsInGrid(true, false, false, false);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            PlaceObjectsInGrid(false, true, false, false);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            PlaceObjectsInGrid(false, false, true, false);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            PlaceObjectsInGrid(false, false, false, true);
        }

        // yatay düzen
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            _formation = Formation.LineAbreast;
            FormationLine();
        }

        // kutu şeklinde
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _formation = Formation.Box;
            FormationBox();
        }

        // ok şeklinde
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _formation = Formation.Trail;
            FormationTrail();
        }

        // dikey düzen
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _formation = Formation.Column;
            FormationColumn();
        }

        // boru şeklinde
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _formation = Formation.Tube;
            FormationTube();
        }

        // boru şeklinde
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            _formation = Formation.Echelon;
            FormationEchelon();
        }
    }

    void FormationCube()
{
    int sideLength = Mathf.CeilToInt(Mathf.Pow(drones.Length, 1f / 3f)); // Küp kenar uzunluğu
    float halfSide = sideLength * 0.5f; // Küp kenar uzunluğunun yarısı

    for (int i = 0; i < drones.Length; i++)
    {
        int layer = i / (sideLength * sideLength); // Küp katmanı
        int row = (i % (sideLength * sideLength)) / sideLength; // Küp satırı
        int col = i % sideLength; // Küp sütunu

        Vector3 newPosition = new Vector3(
            transform.position.x + col * gridSpacing,
            transform.position.y + row * gridSpacing,
            transform.position.z + layer * gridSpacing
        );

        StartCoroutine(MoveDroneSmoothly(drones[i], newPosition));
    }
}


    void FormationLine()
    {
        for (int i = 0; i < drones.Length; i++)
        {
            int row = i / drones.Length;
            int col = i % drones.Length;

            Vector3 newPosition = new Vector3(transform.position.x + col * gridSpacing,
                transform.position.y, transform.position.z + row * gridSpacing);

            StartCoroutine(MoveDroneSmoothly(drones[i], newPosition));
        }
    }

    void FormationBox()
    {
        for (int i = 0; i < drones.Length; i++)
        {
            int row = i / Mathf.CeilToInt(Mathf.Sqrt(drones.Length));
            int col = i % Mathf.CeilToInt(Mathf.Sqrt(drones.Length));

            Vector3 newPosition = new Vector3(transform.position.x + col * gridSpacing,
                transform.position.y + row * gridSpacing, transform.position.z);

            StartCoroutine(MoveDroneSmoothly(drones[i], newPosition));
        }
    }

    void FormationTrail()
    {
        for (int i = 0; i < drones.Length; i++)
        {
            int row = i / Mathf.CeilToInt(Mathf.Sqrt(drones.Length));
            int col = i % Mathf.CeilToInt(Mathf.Sqrt(drones.Length));

            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y,
                transform.position.z + i * gridSpacing);

            StartCoroutine(MoveDroneSmoothly(drones[i], newPosition));
        }
    }

    void FormationColumn()
    {
        for (int i = 0; i < drones.Length; i++)
        {
            int row = i / drones.Length;
            int col = i % drones.Length;

            Vector3 newPosition = new Vector3(transform.position.x,
                transform.position.y + col * gridSpacing, transform.position.z + row * gridSpacing);

            StartCoroutine(MoveDroneSmoothly(drones[i], newPosition));
        }
    }

    void FormationTube()
    {
        for (int i = 0; i < drones.Length; i++)
        {
            float angle = (float)i / drones.Length * 360f;
            float radians = angle * Mathf.Deg2Rad;

            float x = transform.position.x + Mathf.Cos(radians) * tubeRadius;
            float y = transform.position.y + Mathf.Sin(radians) * tubeRadius;
            float z = transform.position.z + (float)i / drones.Length + tubeHeight;

            Vector3 newPosition = new Vector3(x, y, z);
            StartCoroutine(MoveDroneSmoothly(drones[i], newPosition));
        }
    }

    void FormationEchelon()
    {
        Vector3 center = transform.position;
        for (int i = 0; i < drones.Length; i++)
        {
            int row = i / Mathf.CeilToInt(Mathf.Sqrt(drones.Length));
            int col = i % Mathf.CeilToInt(Mathf.Sqrt(drones.Length));

            Transform child = transform.GetChild(i);
            Vector3 newPosition = new Vector3(center.x + col * gridSpacing, transform.position.y,
                center.z + row * gridSpacing);
            //child.localPosition = newPosition;
            StartCoroutine(MoveDroneSmoothly(child, newPosition));
        }
        /*
        for (int i = 0; i < drones.Length; i++)
        {
            int row = i / Mathf.CeilToInt(Mathf.Sqrt(drones.Length));
            int col = i % Mathf.CeilToInt(Mathf.Sqrt(drones.Length));

            Transform child = transform.GetChild(i);
            Vector3 newPosition = new Vector3(col * gridSpacing, transform.position.y, row * gridSpacing);
            //child.localPosition = newPosition;
            StartCoroutine(MoveDroneSmoothly(drones[i], newPosition));
        }
        */
    }

    bool CheckParentsChild()
    {
        if (drones.Length == 0)
        {
            Debug.LogWarning("Child nesne yok");
            return false;
        }
        else
        {
            return true;
        }
    }

    IEnumerator MoveDroneSmoothly(Transform drone, Vector3 targetPosition)
    {
        float distanceToTarget = Vector3.Distance(drone.position, targetPosition);
        float duration = distanceToTarget / moveSpeed;
        float elapsedTime = 0f;
        Vector3 initialPosition = drone.position;

        while (elapsedTime < duration)
        {
            float easedTime = Mathf.SmoothStep(0f, 1f, elapsedTime / duration);
            drone.position = Vector3.Lerp(initialPosition, targetPosition, easedTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        drone.position = targetPosition;


        //float elapsedTime = 0f;
        //float duration = 2f; // Yumuşak geçişin süresi

        //Vector3 initialPosition = drone.position;

        //while (elapsedTime < duration)
        //{
        //    float easedTime = Mathf.SmoothStep(0f, 1f, elapsedTime / duration);
        //    drone.position = Vector3.Lerp(initialPosition, targetPosition, easedTime);

        //    elapsedTime += Time.deltaTime;
        //    yield return null;
        //}

        // Hedefe ulaşıldığında kesin konumlandırma
        //drone.position = targetPosition;
    }


    private IEnumerator MoveDronesUpSmoothly()
    {
        float elapsedTime = 0f;
        float duration = 20f;

        while (elapsedTime < duration)
        {
            foreach (Transform drone in drones)
            {
                float currrentHeight = drone.position.y;
                float easedTime = Mathf.SmoothStep(0f, 1f, elapsedTime / duration);
                float newHeight = Mathf.Lerp(currrentHeight, targetHeight, easedTime);

                drone.position = new Vector3(drone.position.x, newHeight, drone.position.z);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    // T tuşuna basıldığında Drone'ları havaya kaldır
    private void MoveDronesUp()
    {
        if (isAscending)
        {
            foreach (Transform drone in drones)
            {
                float newY = Mathf.Lerp(drone.position.y, drone.position.y + ascentSpeed,
                    Time.deltaTime);
                //drone.transform.Translate(Vector3.up * heightChangeAmount);
                drone.position = new Vector3(drone.position.x, newY, drone.position.z);
            }
        }
    }

    void PlaceObjectsInGrid(bool horizontal, bool vertical, bool forward, bool bottom)
    {
        int rowLength = horizontal ? drones.Length : Mathf.CeilToInt(Mathf.Sqrt(drones.Length));
        int colLength = horizontal
            ? Mathf.CeilToInt(Mathf.Sqrt(drones.Length))
            : Mathf.CeilToInt((float)drones.Length / rowLength);

        for (int i = 0; i < drones.Length; i++)
        {
            int row = i / rowLength;
            int col = i % rowLength;

            Vector3 newPosition;

            if (horizontal)
            {
                newPosition = new Vector3(transform.position.x + col * gridSpacing,
                    transform.position.y, transform.position.z + row * gridSpacing);
            }
            else if (vertical)
            {
                newPosition = new Vector3(transform.position.x,
                    transform.position.y + col * gridSpacing, transform.position.z + row * gridSpacing);
            }
            else if (forward)
            {
                newPosition = new Vector3(transform.position.x + col * gridSpacing,
                    transform.position.y + row * gridSpacing, transform.position.z);
            }
            else if (bottom)
            {
                newPosition = new Vector3(transform.position.x, transform.position.y,
                    transform.position.z + i * gridSpacing);
            }
            else
            {
                newPosition = Vector3.zero;
            }

            drones[i].position = newPosition;
        }
    }


    // A tuşuna basıldığında Drone'ları hedefe gönder
    private void MoveDronesTowardsTarget()
    {
        Vector3 targetPosition = new Vector3(10f, 0f, 0f);

        foreach (Transform drone in drones)
        {
            drone.transform.position =
                Vector3.MoveTowards(drone.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
}
