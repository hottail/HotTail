using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotTail.manipulators
{
    public partial class CritDirectHitManipulatorSettings : UserControl
    {
        public CritDirectHitManipulatorSettings(decimal initialDecayTime, int initialCritIncrease, int initialDhIncrease)
        {
            InitializeComponent();

            fullDecayTimeSetting.Value = initialDecayTime;
            critIncreaseSetting.Value = initialCritIncrease;
            dhIncreaseSetting.Value = initialDhIncrease;
        }
    }
}
