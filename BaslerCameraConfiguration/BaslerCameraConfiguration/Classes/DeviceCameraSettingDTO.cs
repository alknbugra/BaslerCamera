using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaslerCameraConfiguration.Classes
{
    public class DeviceCameraSettingDTO
    {
        public int IdCameraSetting { get; set; }
        public int IdDevice { get; set; }
        public string ModelName { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string PixelFormat { get; set; }
        public int ExposureTime { get; set; }
        public string ExposureAuto { get; set; }
        public int GaindB { get; set; }
        public string GainAuto { get; set; }
        public string BalanceWhiteAuto { get; set; }
        public bool EnableAcquisitionFrameRate { get; set; }
        public int AcquisitionFrameRateHz { get; set; }
        public int NumberOfPhotoShoots { get; set; }
        public int WaitingTimeBetweenShots { get; set; }
        public int OffsetX { get; set; }
        public int OffsetY { get; set; }
        public bool EnableMultipleROI { get; set; }
        public int MultipleROIvalue { get; set; }

    }
}
