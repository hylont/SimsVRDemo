using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Tablet : MonoBehaviour, IGameListener
    {
        private GameObject dominantHand;
        private Dictionary<string, GameObject> contents { get; } = new();
        [SerializeField]
        private List<GameObject> contentsGO;
        [SerializeField]
        private GameObject buttonPrefab;

        public void Start()
        {
            GameObject game = GameObject.FindGameObjectWithTag("GAME");
            game.GetComponent<Game>().AddListener(this);
            GameObject canvas = GameObject.FindGameObjectWithTag("TABLET_CANVAS");
            GameObject panel = GameObject.FindGameObjectWithTag("TABLET_PANEL");

            const int XOFFSET = 330;
            const int YOFFSET = 130;
            const int BTNS_PER_ROW = 3;

            Hand hand = dominantHand.GetComponent<Hand>();

            int idxContent = 0;
            foreach (GameObject content in contentsGO)
            {
                contents.Add(content.name.ToUpper(), content);
                GameObject newButton = Instantiate(buttonPrefab, panel.transform);
                newButton.GetComponentInChildren<TMP_Text>().text = content.name;
                newButton.transform.localPosition = new Vector3(-XOFFSET, YOFFSET, 0);
                switch (content.name.ToUpper())
                {
                    case "WALL":
                        newButton.GetComponent<Button>().onClick.AddListener(delegate { hand.SetInHandContent(new Wall(content)); });
                        break;
                    case "FLOOR":
                        newButton.GetComponent<Button>().onClick.AddListener(delegate { hand.SetInHandContent(new Floor(content)); });
                        break;
                    case "BED":
                        newButton.GetComponent<Button>().onClick.AddListener(delegate { hand.SetInHandContent(new Bed(content)); });
                        break;
                    case "FRIDGE":
                        newButton.GetComponent<Button>().onClick.AddListener(delegate { hand.SetInHandContent(new Fridge(content)); });
                        break;
                    case "AGENT":
                        newButton.GetComponent<Button>().onClick.AddListener(delegate { hand.SetInHandContent(new Agent(content)); });
                        break;
                    default:
                        Debug.LogError("[TABLET] : Button" + content.name + "not recognised !");
                        break;
                }
                //newButton.GetComponentInChildren<TextMesh>().text = content.name;
                newButton.transform.localPosition = new Vector3(newButton.transform.localPosition.x + idxContent % BTNS_PER_ROW * XOFFSET, newButton.transform.localPosition.y - Mathf.Floor(idxContent / BTNS_PER_ROW) * YOFFSET, 0);
                idxContent++;
            }
        }

        public void OnDominantHandChange(EHand newDominantHand)
        {
            dominantHand = GameObject.FindGameObjectWithTag(newDominantHand + " HAND");
        }
    }
}