using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CasinoUI.Models.SlotMachine
{
    public class SlotMachineIcon
    {
        public BitmapImage Image { get; private set; }
        public SlotMachineIcons Icon { get; private set; }

        public SlotMachineIcon(SlotMachineIcons icon)
        {
            Icon = icon;
            // assign image based on icon value
        }
    }
}
