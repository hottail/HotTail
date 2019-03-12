using Advanced_Combat_Tracker;
using Buttplug.Client;
using Buttplug.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotTail.manipulators
{
    public class CritDirectHitManipulator : IManipulator
    {
        public CritDirectHitManipulator()
        {
            var player = GetPlayer();

            if (player.Key != null)
            {
                LastCritHits = Int32.Parse(player.Value["crithits"]);
                LastDHits = Int32.Parse(player.Value["DirectHitCount"]);
            }
        }

        public string Descriptor { get => "Crit/DH Spikes"; }

        public double Level { get; set; } = 0;

        private int LastCritHits = 0;
        private int LastDHits = 0;

        private KeyValuePair<CombatantData, Dictionary<string, string>> GetPlayer()
        {
            if (HotTailCore.CheckIsActReady())
            {
                var allies = ActGlobals.oFormActMain.ActiveZone.ActiveEncounter.GetAllies();
                List<KeyValuePair<CombatantData, Dictionary<string, string>>> combatant = null;

                var combatantTask = Task.Run(() =>
                {
                    combatant = HotTailCore.Instance.GetCombatantList(allies);
                });
                Task.WaitAll(combatantTask);

                var player = combatant.Find(x => x.Key.Name == ActGlobals.charName);

                return player;
            }

            return new KeyValuePair<CombatantData, Dictionary<string, string>>(null, null);
        }

        public void DataUpdate()
        {
            if (HotTailCore.CheckIsActReady())
            {
                if (!ActGlobals.oFormActMain.ActiveZone.ActiveEncounter.Active)
                {
                    Stop();
                    return;
                }

                var player = GetPlayer();

                if (player.Key == null)
                {
                    return;
                }

                var CurrentCritHits = Int32.Parse(player.Value["crithits"]);
                var CurrentDHits = Int32.Parse(player.Value["DirectHitCount"]);

                Level -= 0.005;
                Level = Math.Min(Level, 1);
                Level = Math.Max(Level, 0);

                Level += 0.3 * (CurrentCritHits - LastCritHits);
                Level = Math.Min(Level, 1);
                Level = Math.Max(Level, 0);

                Level += 0.2 * (CurrentDHits - LastDHits);
                Level = Math.Min(Level, 1);
                Level = Math.Max(Level, 0);

                LastCritHits = CurrentCritHits;
                LastDHits = CurrentDHits;
            }
            else
            {
                Stop();
            }
        }

        public void Stop()
        {
            LastCritHits = 0;
            LastDHits = 0;
            Level = 0;
        }
    }
}
