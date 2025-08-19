using BaslerCameraConfiguration.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaslerCameraConfiguration
{
    public class GlobalStructureDTO
    {

        private static GlobalStructureDTO _instance;
        public static GlobalStructureDTO Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GlobalStructureDTO();
                }
                return _instance;
            }
        }
        private GlobalStructureDTO() { }

        public bool TestUrunGeldi = false;
        public BaslerCommunication BaslerCommunication { get; set; }
        public DeviceCameraSettingDTO SystemDeviceCameraDTO { get; set; } = new DeviceCameraSettingDTO();
    }
}
