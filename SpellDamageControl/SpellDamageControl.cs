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

            On.PlayMakerFSM.OnEnable += ModifySpellDamage;
        }

        private void ModifySpellDamage(On.PlayMakerFSM.orig_OnEnable orig, PlayMakerFSM self)
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
            else if (self.FsmName == "Set Damage" && self.transform.parent is not null && self.transform.parent.name == "Scr Heads")
            {
                SetFsmInt[] damageSetters = self.Fsm.GetState("Set Damage").Actions.OfType<SetFsmInt>().ToArray();
                damageSetters[0].setValue.Value = GS.HowlingWraiths;
                damageSetters[1].setValue.Value = GS.HowlingWraithsShaman;
            }
            else if (self.FsmName == "Set Damage" && self.transform.parent is not null && self.transform.parent.name == "Scr Heads 2")
            {
                SetFsmInt[] damageSetters = self.Fsm.GetState("Set Damage").Actions.OfType<SetFsmInt>().ToArray();
                damageSetters[0].setValue.Value = GS.AbyssShriek;
                damageSetters[1].setValue.Value = GS.AbyssShriekShaman;
            }
        }
    }
}