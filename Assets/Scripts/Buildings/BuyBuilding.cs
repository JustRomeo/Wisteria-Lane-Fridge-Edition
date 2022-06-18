using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Photon.Pun;
using Photon.Realtime;

public class BuyBuilding : MonoBehaviour {

    public GameObject Canvas;
    public GameObject prefab;
    public GameObject productionLineUpgrade;

    private Vector3 mousePos;
    private int buildingprice;
    private MoneyMaking _money;
    private GameObject _camera;
    private GameObject[] _buildings;
    private List<string> _buildingsColor;
    private Vector3 oldmouseposition;
    private ActiveCanvas canvascript;
    private bool isRightClickPress = false;
    private Vector3 camPos = new Vector3(0, 0, 0);
    private Vector3 startcampos = new Vector3(475, 500, 275);
    [SerializeField]
    private List<Material> materials;
    public PhotonView view;

    void Start() {
        buildingprice = 5000;
        canvascript = Canvas.GetComponent<ActiveCanvas>();
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
        _buildings = GameObject.FindGameObjectsWithTag("Building");
        _money = GameObject.FindGameObjectWithTag("Money").GetComponent<MoneyMaking>();

        oldmouseposition = Input.mousePosition;
        startcampos = _camera.transform.position;

        _buildingsColor = new List<string>();
        view = GetComponent<PhotonView>();

        for (int i = 0; i < _buildings.Length; i++) {
            _buildingsColor.Add("white");
        }

        bool status = false;
        do {
            int random_nb = Random.Range(0, _buildings.Length);
            if (_buildingsColor[random_nb] == "white") {
                buyBuilding(_buildings[random_nb], 0, random_nb);
                status = true;
            }
        } while (status == false);
    }

    void Update() {
        // If user click on Topbar, Bottom Bar, Tutorial or options button, or if canvas Interface is open, building cannot be bought
        if (canvascript.getActiveCanvas() || Input.mousePosition.y < 150 || Input.mousePosition.y > 1000 || (Input.mousePosition.y > 925 && Input.mousePosition.x > 1750))
            return;

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
            mousePos = Input.mousePosition;

        isKeepRightClickPressed();
        if (Input.GetMouseButtonDown(0))
            isBuildingBuyable();
        else if (isRightClickPress)
            viewMoving();
        // if (Input.GetMouseButtonDown(2))
        //     print("Middle Mouse Button");

        oldmouseposition = Input.mousePosition;
    }

    float MathABS(float value) {return value > 0 ? value : value * -1;}
    void isKeepRightClickPressed() {
        if (Input.GetMouseButtonUp(1))
            isRightClickPress = false;
        if (Input.GetMouseButtonDown(1))
            isRightClickPress = true;
    }

    [PunRPC]
    public void ChangeColor(int index) {
        print("Buy building = index" + index);
        print("playerColor = " + PlayerManager.playerColor);
        if (view == null)
            print("view is null");
        view.RPC("ChangeColorRPC", RpcTarget.All, PlayerManager.playerColor, index);
    }

    [PunRPC]
    void ChangeColorRPC(string color, int index) {
        print("COlor " + color);
        if (color == "red")
            _buildings[index].GetComponent<Renderer>().material = materials[0];
        else if (color == "blue")
            _buildings[index].GetComponent<Renderer>().material = materials[1];
    }

    void buyBuilding(GameObject choosen, int _price, int index) {
        ChangeColor(index);

        _money.pay(_price);
        if (_price > 0)
            buildingprice += Random.Range(_price, _price * 3);

        GameObject trck_spwnr = Instantiate(prefab, choosen.transform.position, Quaternion.identity);
        VehiclesSpawner script = trck_spwnr.GetComponent<VehiclesSpawner>();

        //script.Team[0] = 0;
        //script.teams[0] = PlayerManager.playerColor;
    }

    void isBuildingBuyable() {
        GameObject choosen = null;
        string colorBuilding = "white";
        int index = 0;
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
                colorBuilding = _buildingsColor[i];
                index = i;
            }
        } if (choosen) {
            if (_money.getMoney() >= buildingprice && colorBuilding == "white") {
                buyBuilding(choosen, buildingprice, index);
                productionLineUpgrade.GetComponent<NewProductionLineUpgrade>().increaseMaxUpgrade();
            }
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
}