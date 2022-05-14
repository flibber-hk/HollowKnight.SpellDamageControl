using System;
using System.Collections.Generic;
using System.Linq;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Modding;
using UnityEngine;

namespace SpellDamageControl
{
    public class SpellDamageControl : Mod, IGlobalSettings<GlobalSettings>
    {
        internal static SpellDamageControl instance;

        public static GlobalSettings GS = new();
        public void OnLoadGlobal(GlobalSettings gs) => GS = gs;
        public GlobalSettings OnSaveGlobal() => GS;

        public SpellDamageControl() : base(null)
        {
            instance = this;
        }
        
        public override string GetVersion()
        {
            return GetType().Assembly.GetName().Version.ToString();
        }
        
        public override void Initialize()
        {
            Log("Initializing Mod...");

            On.PlayMakerFSM.OnEnable += ModifyFireballDamage;
        }

        private void ModifyFireballDamage(On.PlayMakerFSM.orig_OnEnable orig, PlayMakerFSM self)
        {
            orig(self);

            if (self.FsmName == "Fireball Control" && self.gameObject.name == "Fireball")
            {
                SetFsmInt[] damageSetters = self.Fsm.GetState("Set Damage").Actions.OfType<SetFsmInt>().ToArray();
                damageSetters[0].setValue.Value = GS.VengefulSpirit;
                damageSetters[1].setValue.Value = GS.VengefulSpiritShaman;
            }
            else if (self.FsmName == "Fireball Control" && self.gameObject.name == "Fireball2 Spiral")
            {
                SetFsmInt[] damageSetters = self.Fsm.GetState("Set Damage").Actions.OfType<SetFsmInt>().ToArray();
                damageSetters[0].setValue.Value = GS.ShadeSoul;
                damageSetters[1].setValue.Value = GS.ShadeSoulShaman;
            }
        }
    }
}