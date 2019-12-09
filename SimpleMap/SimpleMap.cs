using MSCLoader;
using UnityEngine;

namespace SimpleMap
{
    public class SimpleMap : Mod
    {
        private const int IconSize = 10;

        private readonly Keybind _toggleMapKey = new Keybind("showmap", "Show/hide simplemap", KeyCode.F8);
        private string[] _displayNames;
        private bool[] _enabled;
        private Texture[] _icons;

        private Texture _mapbg;
        private Texture _mapIcon;

        private GameObject[] _mapObjects;

        private bool _toggleMap;
        public override string ID => "SimpleMap"; //Your mod ID (unique)
        public override string Name => "Simple in-game map"; //You mod name
        public override string Author => "AnotherFoxGuy"; //Your Username
        public override string Version => "1.0"; //Version

        // Set this to true if you will be load custom assets from Assets folder.
        // This will create subfolder in Assets folder for your mod.
        public override bool UseAssetsFolder => true;

        public override void OnLoad()
        {
            var ab = LoadAssets.LoadBundle(this, "mapicons.uni3d");


            _mapObjects = new[]
            {
                GameObject.Find("SATSUMA(557kg, 248)"),
                GameObject.Find("PLAYER"),
                GameObject.Find("train"),
                GameObject.Find("BUS"),
                GameObject.Find("FITTAN")
            };

            /* _icons = new[]
             {
                 ab.LoadAsset("icon_village.dds") as Texture ,
                 ab.LoadAsset("icon_person_activated.dds") as Texture ,
                 ab.LoadAsset("icon_machine_activated.dds") as Texture 
             };
 
             _displayNames = new[]
             {
                 "Satsuma",
                 "Player",
                 "Train"
             };*/

            _enabled = new bool[3];


            _mapbg = ab.LoadAsset("map.png") as Texture;
            _mapIcon = ab.LoadAsset("icon_repair.dds") as Texture;
            ab.Unload(false);

            Keybind.Add(this, _toggleMapKey); //register keybind for this Mod class
        }

        public override void ModSettings()
        {
            // All settings should be created here. 
            // DO NOT put anything else here that settings.
        }

        public override void OnSave()
        {
            // Called once, when save and quit
            // Serialize your save file here.
        }

        // Called once every frame
        public override void Update()
        {
            if (_toggleMapKey.IsDown())
                _toggleMap = !_toggleMap;
        }

        public override void OnGUI()
        {
            // Draw unity OnGUI() here
            if (_toggleMap)
            {
                // Make a background box
                GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "SimpleMap");

                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), _mapbg, ScaleMode.StretchToFill);

                for (var i = 0; i < _mapObjects.Length; i++)
                    //GUI.DrawTexture(convertCoord(_mapObjects[i].transform.position),  _icons[i]);
                    GUI.DrawTexture(convertCoord(_mapObjects[i].transform.position), _mapIcon);
            }
        }

        private Rect convertCoord(Vector3 inp)
        {
            var hi = IconSize / 2;
            return new Rect(Mathf.Lerp(-hi, Screen.width - hi, (inp.x + 2325) / 4650),
                Mathf.Lerp(Screen.height - hi, -hi, (inp.z + 1880) / 3760),
                IconSize, IconSize);
        }
    }
}