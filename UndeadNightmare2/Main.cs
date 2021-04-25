using System;
using System.IO;
using RDR2;
using RDR2.UI;
using RDR2.Native;
using RDR2.Math;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UndeadNightmare2
{
    public class Main : Script
    {
        const int maxEnemies = 10;

        Brain[] undeadsBrains = new Brain[maxEnemies];
        Ped[] undeads = new Ped[maxEnemies];

        Blip[] mapBlips = new Blip[5];

        public Main()
        {
            Init();

            KeyDown += OnKeyDown;
            Tick += OnTick;
            Aborted += OnAbort;

            Interval = 1000;
        }

        private void OnTick(object sender, EventArgs e)
        {
            #region death system
            if (isDeath())
            {
                Player().IsInvincible = true;
                Player().Task.ClearAllImmediately();
                Player().Task.KnockOut(0, false);
                Wait(5000);
                Function.Call(Hash.DO_SCREEN_FADE_OUT, 1200);
                Wait(1201);
                Player().Position = new Vector3(-289.3333f, 811.3876f, 119.3859f);
                Function.Call(Hash.DO_SCREEN_FADE_IN, 1200);
                
                Player().IsInvincible = false;
                Player().Health = 200;
            }
            #endregion

            for (int i = 0; i < maxEnemies; i++)
            {
                if (undeads[i].Exists() && undeads[i].IsAlive && !undeadsBrains[i].isAttacking)
                {
                    undeads[i].AlwaysKeepTask = true;
                    //Ped target = getClosestPed(undeads[i]);
                    Ped target = World.GetClosest<Ped>(undeads[i].Position, World.GetAllPeds());
                    undeadsBrains[i].isAttacking = true;
                    undeads[i].Task.Combat(target);
                }
            }
        }

        #region stop script
        private void OnAbort(object sender, EventArgs e)
        {
            foreach (Ped undead in undeads)
            {
                undead.Delete();
            }
        }
        #endregion

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.X)
            {
                Init();
            }
            if (e.KeyCode == Keys.Z)
            {
                RDR2.UI.Screen.ShowSubtitle(Player().Position.ToString());
                /*foreach (Ped p in World.GetAllPeds())
                {
                    p.Delete();
                }*/
            }
        }

        public void Init()
        {
            //clearAllPeds();

            //Function.Call(Hash.SET_PLAYER_MODEL, Game.Player.Character, (uint)PedHash.Player_Zero, true);

            Game.MaxWantedLevel = 0;
            //Model sad = new Model("PLAYER_ZERO");
            //Game.Player.ChangeModel(sad);
            //Game.Player.Character.Outfit = 1;
            //Function.Call(Hash.SET_PLAYER_MODEL, Player().Handle, new Model("PLAYER_ZERO").Hash);

            //Game.Player.Character.Position = new Vector3(795.5022f, 1777.418f, 281.448f);

            Player().HealthCoreRank = 200;
            Player().HealthCore = 200;
            Player().Health = 200;
            Player().MaxHealth = 200;
            Player().HealthCoreRank = 200;
            Player().HealthCore = 200;
            Player().Health = 200;

            Function.Call(Hash.SET_WANTED_LEVEL_MULTIPLIER, 0);

            Random r = new Random();
            uint[] weathers = new uint[] { 0xBB898D2D, 0xCA71D7C, 0x995C7F44 };
            Function.Call((Hash)0x59174F1AFE095B5A, weathers[r.Next(0,2)], false, true, true, 1F, false);

            //generateEnemies();
            //generateBlips();
        }

        #region Utils

        Ped Player()
        {
            return Game.Player.Character;
        }

        void clearAllPeds()
        {
            foreach (Ped ped in World.GetAllPeds())
            {
                if (ped.IsHuman)
                    ped.Delete();
            }
        }

        bool isDeath()
        {
            if (Player().Health <= 100)
            {
                return true;
            }

            return false;
        }

        #endregion

        #region peds

        void generateEnemies()
        {
            for (int i = 0; i < undeads.Length; i++)
            {
                undeads[i] = createped("A_M_M_ARMCHOLERACORPSE_01", "Undead", 250);
                enemyPed(undeads[i]);
                undeadsBrains[i] = new Brain();
                undeadsBrains[i].isAttacking = false;
                //undeads[i].target = getClosestPed(undeads[i].ped);
            }
        }

        private Ped createped(string modelName, string name = "", int health = 100, Vector3 pos = default, float headg = 0.0f)
        {
            Model model = new Model(modelName);
            Ped ped;

            if (pos == default)
                ped = RDR2.World.CreatePed(model, Player().Position.Around(10), 0, false, false);
            else
                ped = RDR2.World.CreatePed(model, pos, headg, false, false);


            //OTHERS
            if (!String.IsNullOrEmpty(name)) ped.SetPedPromptName(name);

            ped.IsInvincible = false;
            ped.Accuracy = 100;
            ped.Velocity = new Vector3(3.0f, 3.0f, 3.0f);
            ped.Health = health;

            ped.AddBlip(BlipType.BLIP_STYLE_ENEMY);
            ped.CurrentBlip.Label = name;

            return ped;
        }

        void enemyPed(Ped ped)
        {
            Function.Call(Hash.SET_PED_FLEE_ATTRIBUTES, ped, 0, 0);
            Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, ped, 46, true);
            //(Crash)Function.Call(Hash.SET_PED_COMBAT_RANGE, ped, 9000.0f);
            Function.Call(Hash.SET_PED_SEEING_RANGE, ped, 9000.0f);
            Function.Call(Hash.SET_PED_VISUAL_FIELD_PERIPHERAL_RANGE, ped, 9000.0f);
            Function.Call(Hash.SET_PED_RELATIONSHIP_GROUP_HASH, ped, 1269650476);
        }

        #endregion

        Ped getClosestPed(Ped origin)
        {
            Ped[] allPeds = World.GetAllPeds();
            int finalPed = 0;
            float closestDistance = 99999.99f;

            int i = 0;
            foreach (Ped ped in allPeds)
            {
                float distance = ped.Position.DistanceTo2D(origin.Position);
                if (!ped.Equals(origin) && distance < closestDistance)
                {
                    finalPed = i;
                    closestDistance = distance;
                }

                i++;
            }

            return allPeds[finalPed];
        }

        #region map

        void generateBlips()
        {
            Function.Call(Hash.SET_THIS_SCRIPT_CAN_REMOVE_BLIPS_CREATED_BY_ANY_SCRIPT, true);

            BlipType blipBase = BlipType.BLIP_STYLE_TEMPORARY_HORSE;
            uint spriteId = 350569997;
            string name = "Cementery";
            float size = 5.0f;
            
            mapBlips[0] = CustomFunctions.CreateBlip(new Vector3(-5449.58f, -2911.172f, 0.86f), blipBase, spriteId, name, size);
            mapBlips[1] = CustomFunctions.CreateBlip(new Vector3(-1004.295f, -1191.475f, 59.00f), blipBase, spriteId, name, size);
            mapBlips[2] = CustomFunctions.CreateBlip(new Vector3(2725.145f, -1060.647f, 47.40f), blipBase, spriteId, name, size);
            mapBlips[3] = CustomFunctions.CreateBlip(new Vector3(-4442.324f, -2688.377f, -11.08f), blipBase, spriteId, name, size);
            mapBlips[4] = CustomFunctions.CreateBlip(new Vector3(-3328.963f, -2856.669f, -6.08f), blipBase, spriteId, name, size);
            
        }

        #endregion

    }
}


//Cinematics.cutscene("cutscene@FIN1_EXT", "1-HighHonor");
//Cinematics.cutscene("cutscene@FIN2_EXT_p18", "");

//Function.Call(Hash.DOOR_SYSTEM_SET_DOOR_STATE, 0, 0);

/*
CHOLERA
PRISIONER
cassidy
creepyoldlady
gloria
herbalist
johnbaptising
micahnemesis
mysteruiousstranger
odprostitute
poorjoe
priest_wedding
sistercalderon
soothsayer
swampfreack
tigerhandler
vampire
CS_MP_SETH
RALLY MODS
savageaftermath
savagewarnin
townwidnow
*/