using Advanced_Combat_Tracker;
using Buttplug.Client;
using Buttplug.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotTail.manipulators
{
    public class CritDirectHitManipulator : IManipulator
    {
        public event EventHandler LevelChanged;

        private double CritLevelIncrease = 0.3;
        private double DHLevelIncrease = 0.2;
        private double FullDecayTime = 20;

        private double CritIncreasePercentage { get => CritLevelIncrease * 100; set => CritLevelIncrease = value / 100; }
        private double DHIncreasePercentage { get => DHLevelIncrease * 100; set => DHLevelIncrease = value / 100; }
        private double PerTickDecay { get => (1 / FullDecayTime) / 1000 / 10000; }

        public CritDirectHitManipulator()
        {
            var settings = new CritDirectHitManipulatorSettings((decimal)FullDecayTime, (int)CritIncreasePercentage, (int)DHIncreasePercentage);

            settings.fullDecayTimeSetting.ValueChanged += (o, e) => {
                FullDecayTime = (double)((NumericUpDown)o).Value;
            };
            settings.critIncreaseSetting.ValueChanged += (o, e) => {
                CritIncreasePercentage = ((TrackBar)o).Value;
            };
            settings.dhIncreaseSetting.ValueChanged += (o, e) => {
                DHIncreasePercentage = ((TrackBar)o).Value;
            };

            manipulatorSettings = settings;

            var player = GetPlayer();

            if (player.Key != null)
            {
                LastCritHits = Int32.Parse(player.Value["crithits"]);
                LastDHits = Int32.Parse(player.Value["DirectHitCount"]);
            }
        }

        public string Descriptor { get => "Crit/DH Spikes"; }

        public UserControl ManipulatorSettings { get => manipulatorSettings; }
        private UserControl manipulatorSettings;

        public double Level
        {
            get => _level;
            set
            {
                _level = Math.Min(Math.Max(value, 0), 1);
                LevelChanged?.Invoke(this, new LevelChangedEventArgs(_level));
            }
        }
        private int LastCritHits = 0;
        private int LastDHits = 0;
        private double _level = 0;

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

        public void DataUpdate(long ticksElapsed)
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

                Level -= PerTickDecay * ticksElapsed;
                Level += CritLevelIncrease * (CurrentCritHits - LastCritHits);
                Level += DHLevelIncrease * (CurrentDHits - LastDHits);

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
