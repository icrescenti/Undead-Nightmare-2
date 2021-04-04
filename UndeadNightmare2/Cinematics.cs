using System;
using System.Collections.Generic;
using RDR2;
using RDR2.UI;
using RDR2.Native;
using RDR2.Math;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndeadNightmare2
{
    public class Cinematics
    {
        public static void cutscene(string name, string sts)
        {
            int animScene = Function.Call<int>(Hash._CREATE_ANIM_SCENE, name, 0, sts, false, true);

            if (Function.Call<bool>(Hash._0x25557E324489393C, animScene))
            {

                #region variables
                int[] hs = new int[] { 1,6,2,3,4,5,7,8 };
                int[] sks = new int[] { 17, 15, 3, 0, 20, 12, 17, 7, 6 };
                /*int i = 0;
                foreach (string line in red.Split(';'))
                {
                    if (i == 0)
                    {
                        hs = line.Split('=')[1].Split(',');
                    }
                    else if (i == 1)
                    {
                        //sks = Array.ConvertAll(line.Split('=')[1].Split(','), int.Parse);
                    }
                    i++;
                }*/

                #endregion

                #region characters
                Ped[] p = new Ped[10];

                Function.Call(Hash.SET_ANIM_SCENE_ENTITY, animScene, "ARTHUR", Function.Call<int>(Hash.PLAYER_PED_ID), 0);

                #region one-dutch
                p[1] = RDR2.World.CreatePed(new Model("CS_dutch"), new Vector3(2078, 1613, 198), 0, false, false);
                p[1].Outfit = sks[1];
                #endregion
                #region four-micah
                p[4] = RDR2.World.CreatePed(new Model("CS_MicahBell"), new Vector3(2078, 1613, 198), 0, false, false);
                p[4].Outfit = sks[4];
                #endregion

                /*
                #region two-abigail
                p[2] = RDR2.World.CreatePed(new Model("CS_AbigailRoberts"), new Vector3(2509, 1393, 101), 0, false, false);
                p[2].Outfit = sks[2];
                #endregion
                #region three-milton
                p[3] = RDR2.World.CreatePed(new Model("CS_miltonandrews"), new Vector3(2509, 1393, 102), 0, false, false);
                p[3].Outfit = sks[3];
                #endregion
                #region five-bill
                p[5] = RDR2.World.CreatePed(new Model("CS_billwilliamson"), new Vector3(2509, 1393, 102), 0, false, false);
                p[5].Outfit = sks[5];
                #endregion
                #region six-javier
                p[6] = RDR2.World.CreatePed(new Model("CS_javierescuella"), new Vector3(2509, 1393, 102), 0, false, false);
                p[6].Outfit = sks[6];
                #endregion
                #region seven-tilly
                p[7] = RDR2.World.CreatePed(new Model("CS_tilly"), new Vector3(2509, 1393, 102), 0, false, false);
                p[7].Outfit = sks[7];
                #endregion
                #region eight-smallmarston
                p[8] = RDR2.World.CreatePed(new Model("CS_jackmarston"), new Vector3(2509, 1393, 102), 0, false, false);
                p[8].Outfit = sks[8];
                #endregion
                */

                #endregion

                #region horses-spawn
                /*Ped[] h = new Ped[8];
                Ped hp = RDR2.World.CreatePed(new Model("A_C_Horse_KentuckySaddle_Black"), new Vector3(2506, 1405, 100), 0, false, false);
                h[0] = RDR2.World.CreatePed(new Model("A_C_Horse_Gang_Sadie"), new Vector3(2506, 1405, 96), 0, false, false);
                h[1] = RDR2.World.CreatePed(new Model("A_C_Horse_Gang_Dutch"), new Vector3(2506, 1405, 96), 0, false, false);
                h[2] = RDR2.World.CreatePed(new Model("A_C_Horse_Gang_Micah"), new Vector3(2506, 1405, 96), 0, false, false);
                h[3] = RDR2.World.CreatePed(new Model("A_C_Horse_Gang_John"), new Vector3(2506, 1405, 96), 0, false, false);
                h[4] = RDR2.World.CreatePed(new Model("A_C_Horse_Gang_Javier"), new Vector3(2506, 1405, 96), 0, false, false);
                h[5] = RDR2.World.CreatePed(new Model("A_C_Horse_Gang_Bill"), new Vector3(2506, 1405, 96), 0, false, false);
                h[6] = RDR2.World.CreatePed(new Model("A_C_Horse_Andalusian_DarkBay"), new Vector3(2506, 1405, 96), 0, false, false);
                h[7] = RDR2.World.CreatePed(new Model("A_C_Horse_Andalusian_DarkBay"), new Vector3(2506, 1405, 96), 0, false, false);*/
                #endregion

                #region humans
                Function.Call(Hash.SET_ANIM_SCENE_ENTITY, animScene, "Dutch", p[1], 0);
                Function.Call(Hash.SET_ANIM_SCENE_ENTITY, animScene, "MicahBell", p[4], 0);
                /*Function.Call(Hash.SET_ANIM_SCENE_ENTITY, animScene, "AbigailRoberts", p[2], 0);
                Function.Call(Hash.SET_ANIM_SCENE_ENTITY, animScene, "miltonandrews", p[3], 0);
                Function.Call(Hash.SET_ANIM_SCENE_ENTITY, animScene, "BillWilliamson", p[5], 0);
                Function.Call(Hash.SET_ANIM_SCENE_ENTITY, animScene, "JavierEscuella", p[6], 0);
                Function.Call(Hash.SET_ANIM_SCENE_ENTITY, animScene, "Tilly", p[7], 0);
                Function.Call(Hash.SET_ANIM_SCENE_ENTITY, animScene, "JackMarston", p[8], 0);
                Function.Call(Hash.SET_ANIM_SCENE_ENTITY, animScene, "MrsAdler", p[9], 0);*/
                #endregion

                #region horses
                /*Function.Call(Hash.SET_ANIM_SCENE_ENTITY, animScene, "Horse_01", hp, 0);
                for (int j = 0; j < h.Length; j++)
                {
                    Function.Call(Hash.SET_ANIM_SCENE_ENTITY, animScene, "Horse_01^" + hs[j], h[j], 0);
                }*/
                #endregion


                Function.Call(Hash.LOAD_ANIM_SCENE, animScene);

                while (!Function.Call<bool>(Hash._0x477122B8D05E7968, animScene, true, false)) wait(0);

                Function.Call(Hash.TAKE_OWNERSHIP_OF_ANIM_SCENE, animScene);
                Function.Call(Hash.START_ANIM_SCENE, animScene);
                
                while(!Function.Call<bool>((Hash)0xD8254CB2C586412B, animScene)) { wait(0); }

                Function.Call(Hash.DO_SCREEN_FADE_OUT, 3000);
                p[1].Delete();
                p[4].Delete();
                wait(3000);
                //while (Function.Call<bool>(Hash.IS_SCREEN_FADED_OUT)) { }
                Function.Call(Hash.DO_SCREEN_FADE_IN, 3000);

                /*p[2].Delete();
                p[3].Delete();
                wait(56200);
                //wait(2000);
                p[1].Delete();
                p[4].Delete();
                p[5].Delete();
                p[6].Delete();
                h[1].Delete();
                h[2].Delete();
                h[4].Delete();
                h[5].Delete();
                h[6].Delete();
                h[7].Delete();*/

            }
            else
            {
                RDR2.UI.Screen.ShowSubtitle("fatal error");
            }
        }

        private static void wait(int ms)
        {
            RDR2.Script.Wait(ms);
            /*ms = ms / 10;
            while (ms > 0)
            {
                RDR2.Script.Wait(1);
                if (pause)
                {
                    ms++;
                }
                ms--;
            }*/
        }
    }
}
