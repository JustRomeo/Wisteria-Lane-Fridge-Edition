using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuyBuilding : MonoBehaviour {

    public Material _teamcolor;

    private GameObject _camera;
    private Vector3 camPos = new Vector3(0, 0, 0);
    private Vector3 deltacam = new Vector3(475, 500, 275);

    void Start() {
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
    }
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            GameObject choosen = null;
            float lastdistance = 99999;
            Vector3 mousePos = Input.mousePosition;
            GameObject[] _buildings = GameObject.FindGameObjectsWithTag("Building");

            mousePos.y -= 20;
            // camPos = new Vector3(_camera.transform.position.x - deltacam.x, _camera.transform.position.y - deltacam.y, _camera.transform.position.z - deltacam.z);
            for (int i = 0; i < _buildings.Length; i ++) {
                float deltaX = MathABS(_buildings[i].transform.position.x - (mousePos.x / 2));
                float deltaY = MathABS(_buildings[i].transform.position.z - (mousePos.y / 2));
                float brutdistance = Mathf.Sqrt(deltaX * deltaX + deltaY * deltaY);

                if (brutdistance < 230 && lastdistance > brutdistance) {
                    lastdistance = brutdistance;
                    choosen = _buildings[i];
                }
            } if (choosen != null)
                choosen.GetComponent<Renderer>().material = _teamcolor;
        }
        // else if (Input.GetMouseButtonDown(1)) {
        //     print("Right Mouse Button");
        // } if (Input.GetMouseButtonDown(2)) {
        //     print("Middle Mouse Button");
        // }
    }

    float MathABS(float value) {return value > 0 ? value : value * -1;}
}
