using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuyBuilding : MonoBehaviour {

    public GameObject prefab;
    public Material[] _teamcolor;

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
        spawnbuildings();
    }
    void Update() {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
            mousePos = Input.mousePosition;

        isKeepRightClickPressed();
        if (Input.GetMouseButtonDown(0))
            isBuildingBuyable();
        else if (isRightClickPress)
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

    void buyBuilding(GameObject choosen, int _price) {
        choosen.GetComponent<Renderer>().material = _teamcolor[0];
        _money.pay(_price);
        if (_price > 0)
            buildingprice += Random.Range(_price, _price * 3);

        GameObject trck_spwnr = Instantiate(prefab, choosen.transform.position, Quaternion.identity);
        Truck_Spawner script = trck_spwnr.GetComponent<Truck_Spawner>();

        script.Team[0] = 0;
        script.teams[0] = _teamcolor[0];
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
                buyBuilding(choosen, buildingprice);
            else
                print("Not enough money, need " + (buildingprice - _money.getMoney()) + "$ more.");
        }
    }

    void viewMoving() {
        Vector3 _cam = _camera.transform.position;
        Vector3 delta = new Vector3(MathABS(mousePos.x - oldmouseposition.x), MathABS(mousePos.y - oldmouseposition.y), MathABS(mousePos.z - oldmouseposition.z));

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

    void spawnbuildings() {
        // TEMPORARY ===================
        PlayerPrefs.SetInt("isHost", 1);
        // =============================

        buyBuilding(_buildings[Random.Range(0, _buildings.Length)], 0);
        if (PlayerPrefs.GetInt("isHost", 0) == 1) {
            for (int i = 0; i < PlayerPrefs.GetInt("PlayerNbr", 4) - 1; i ++) {
                _buildings[Random.Range(0, _buildings.Length)].GetComponent<Renderer>().material = _teamcolor[i + 1];
            }
        }
    }
}

// Chaque team possede un nombres de buildings, leur nombre et repertorié dans une liste
// teambuildings = [[]]

// 1: [1, 5]
// 2: [2, 6]
// 3: [3, 7]
// 4: [4, 8]