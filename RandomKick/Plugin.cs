using BepInEx;
using System;
using UnityEngine;
using System.ComponentModel;
using Utilla;
using Photon.Pun;
using System.Threading;

namespace RandomKick
{
    /// <summary>
    /// This is your mod's main class.
    /// </summary>

    /* This attribute tells Utilla to look for [ModdedGameJoin] and [ModdedGameLeave] */
    [Description("HauntedModMenu")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [ModdedGamemode]
    public class Plugin : BaseUnityPlugin
    {
        bool inRoom;
        // Token: 0x04000006 RID: 6
        public int seconds2 = 200;
        bool waitdone = false;

        // Token: 0x04000007 RID: 7
        public int seconds;
        bool on = false;

        void Start()
        {
            /* A lot of Gorilla Tag systems will not be set up when start is called /*
			/* Put code in OnGameInitialized to avoid null references */

            Utilla.Events.GameInitialized += OnGameInitialized;
        }

        void OnEnable()
        {
            on = true;

            HarmonyPatches.ApplyHarmonyPatches();
        }

        void OnDisable()
        {
            on = false;

            HarmonyPatches.RemoveHarmonyPatches();
        }

        private void newrandom(object o)
        {
            System.Random random = new System.Random();
            this.seconds = random.Next(360000, 3630000);
        }

        void OnGameInitialized(object sender, EventArgs e)
        {
            newrandom(null) ;
            Timer timer = new Timer(new TimerCallback(this.newrandom), null, 0, this.seconds2);
            Timer timer2 = new Timer(new TimerCallback(this.leave), null, 0, this.seconds);
        }

        void leave(object o)
        {
            PhotonNetwork.Disconnect();
        }

        void Update()
        {
            
        }

        /* This attribute tells Utilla to call this method when a modded room is joined */
        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {
            /* Activate your mod here */
            /* This code will run regardless of if the mod is enabled*/

            inRoom = true;
        }

        /* This attribute tells Utilla to call this method when a modded room is left */
        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {
            /* Deactivate your mod here */
            /* This code will run regardless of if the mod is enabled*/

            inRoom = false;
        }
    }
}
