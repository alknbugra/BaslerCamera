using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaslerCameraConfiguration.Classes
{
    public class SettingCameraFunc
    {
        public string ModelName()
        {
            return GlobalStructureDTO.Instance.BaslerCommunication.GetModelName();
        }
        public string MaxWidth()
        {
            return GlobalStructureDTO.Instance.BaslerCommunication.GetMaxWidth().ToString();
        }
        public string MaxHeight()
        {
            return GlobalStructureDTO.Instance.BaslerCommunication.GetMaxHeight().ToString();
        }
        public string MaxExposureTime()
        {
            return GlobalStructureDTO.Instance.BaslerCommunication.GetMaxExposureTime().ToString();
        }
        public string MaxGainDb()
        {
            return GlobalStructureDTO.Instance.BaslerCommunication.GetMaxGainDb().ToString();
        }
        public string MaxFrameRate()
        {
            return GlobalStructureDTO.Instance.BaslerCommunication.GetMaxFrameRate().ToString();
        }
        public decimal GetOffsetX()
        {
            return GlobalStructureDTO.Instance.BaslerCommunication.GetOffsetX();
        }
        public decimal GetOffsetY()
        {
            return GlobalStructureDTO.Instance.BaslerCommunication.GetOffsetY();
        }

        public List<ComboboxItem> PixelFormatList()
        {
            List<ComboboxItem> ComboList = new List<ComboboxItem>();

            foreach (var item in GlobalStructureDTO.Instance.BaslerCommunication.GetPixelFormatList())
            {
                // create model
                ComboboxItem cmb = new ComboboxItem();
                cmb.Text = item;
                cmb.Value = item;

                // list add
                ComboList.Add(cmb);
            }

            return ComboList;
        }
        public List<ComboboxItem> ExposureAutoList()
        {
            List<ComboboxItem> ComboList = new List<ComboboxItem>();

            foreach (var item in GlobalStructureDTO.Instance.BaslerCommunication.GetExposureAutoList())
            {
                // create model
                ComboboxItem cmb = new ComboboxItem();
                cmb.Text = item;
                cmb.Value = item;

                // list add
                ComboList.Add(cmb);
            }

            return ComboList;
        }
        public List<ComboboxItem> GainAutoList()
        {
            List<ComboboxItem> ComboList = new List<ComboboxItem>();

            foreach (var item in GlobalStructureDTO.Instance.BaslerCommunication.GetGainAutoList())
            {
                // create model
                ComboboxItem cmb = new ComboboxItem();
                cmb.Text = item;
                cmb.Value = item;

                // list add
                ComboList.Add(cmb);
            }

            return ComboList;
        }
        public List<ComboboxItem> BalanceWhiteAutoList()
        {
            List<ComboboxItem> ComboList = new List<ComboboxItem>();

            foreach (var item in GlobalStructureDTO.Instance.BaslerCommunication.GetBalanceWhiteAutoList())
            {
                // create model
                ComboboxItem cmb = new ComboboxItem();
                cmb.Text = item;
                cmb.Value = item;

                // list add
                ComboList.Add(cmb);
            }

            return ComboList;
        }

    }
}
