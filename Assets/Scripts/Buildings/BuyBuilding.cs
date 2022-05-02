using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuyBuilding : MonoBehaviour {

    public GameObject prefab;
    public Material _teamcolor;

    private Vector3 mousePos;
    private int buildingprice;
    private MoneyMaking _money;
    private GameObject _camera;
    private GameObject[] _buildings;
    private Vector3 oldmouseposition;
    private bool isRightClickPress = false;
    private Vector3 camPos = new Vector3(0, 0, 0);
    private Vector3 startcampos = new Vector3(475, 500, 275);

    void Start() {
        buildingprice = 5000;
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
        _buildings = GameObject.FindGameObjectsWithTag("Building");
        _money = GameObject.FindGameObjectWithTag("Money").GetComponent<MoneyMaking>();

        oldmouseposition = Input.mousePosition;
        startcampos = _camera.transform.position;
    }
    void Update() {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
            mousePos = Input.mousePosition;

        isKeepRightClickPressed();
        if (Input.GetMouseButtonDown(0)) // Left Click
            isBuildingBuyable();
        else if (isRightClickPress)      // Right Click
            viewMoving();
        // if (Input.GetMouseButtonDown(2)) {
        //     print("Middle Mouse Button");
        // }

        oldmouseposition = Input.mousePosition;
    }

    float MathABS(float value) {return value > 0 ? value : value * -1;}
    void isKeepRightClickPressed() {
        if (Input.GetMouseButtonUp(1))
            isRightClickPress = false;
        if (Input.GetMouseButtonDown(1))
            isRightClickPress = true;
    }

    void buyBuilding(GameObject choosen) {
        choosen.GetComponent<Renderer>().material = _teamcolor;
        _money.pay(buildingprice);
        buildingprice += Random.Range(buildingprice, buildingprice * 3);

        GameObject trck_spwnr = Instantiate(prefab, choosen.transform.position, Quaternion.identity);
        Truck_Spawner script = trck_spwnr.GetComponent<Truck_Spawner>();

        script.Team[0] = 0;
        script.teams[0] = _teamcolor;
    }

    void isBuildingBuyable() {
        GameObject choosen = null;
        float lastdistance = 99999;
        Vector3 _cam = _camera.transform.position;
        Vector3 deltacam = new Vector3(_cam.x - startcampos.x, 0, _cam.z - startcampos.z);

        mousePos.y -= 20;
        for (int i = 0; i < _buildings.Length; i ++) {
            float deltaX = MathABS(_buildings[i].transform.position.x - deltacam.x - (mousePos.x / 2));
            float deltaY = MathABS(_buildings[i].transform.position.z - deltacam.z - (mousePos.y / 2));
            float brutdistance = Mathf.Sqrt(deltaX * deltaX + deltaY * deltaY);

            if (brutdistance < 230 && lastdistance > brutdistance) {
                lastdistance = brutdistance;
                choosen = _buildings[i];
            }
        } if (choosen) {
            if (_money.getMoney() >= buildingprice)
                buyBuilding(choosen);
            else
                print("Not enough money, need " + (buildingprice - _money.getMoney()) + "$ more.");
        }
    }
    void viewMoving() {
        Vector3 _cam = _camera.transform.position;
        Vector3 delta = new Vector3(MathABS(mousePos.x - oldmouseposition.x), MathABS(mousePos.y - oldmouseposition.y), MathABS(mousePos.z - oldmouseposition.z));

        // print("Right Mouse Button (Delta: " + delta + ")");
        if (oldmouseposition.x < mousePos.x)
            _camera.transform.position = new Vector3(_cam.x + 5, _cam.y, _cam.z);
        if (oldmouseposition.x > mousePos.x)
            _camera.transform.position = new Vector3(_cam.x - 5, _cam.y, _cam.z);
        if (oldmouseposition.y < mousePos.y)
            _camera.transform.position = new Vector3(_cam.x, _cam.y, _cam.z + 5);
        if (oldmouseposition.y > mousePos.y)
            _camera.transform.position = new Vector3(_cam.x, _cam.y, _cam.z - 5);

        oldmouseposition = Input.mousePosition;
    }
}
