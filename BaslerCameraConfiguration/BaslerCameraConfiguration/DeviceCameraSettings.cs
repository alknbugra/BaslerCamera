using Basler.Pylon;
using BaslerCameraConfiguration.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaslerCameraConfiguration
{
    public partial class DeviceCameraSettings : Form
    {

        // Create class func.
        SettingCameraFunc ayarClass = new SettingCameraFunc();

        public DeviceCameraSettings()
        {
            InitializeComponent();
        }

        private void KameraCekimAyarlari_Load(object sender, EventArgs e)
        {
            // form header text change
            Text = Program.UygulamaAdi;

            // BaslerCommunication nesnesini ilk defa burada kontrol et
            if (GlobalStructureDTO.Instance.BaslerCommunication == null)
            {
                GlobalStructureDTO.Instance.BaslerCommunication = new BaslerCommunication();
            }

            LoadSetValue();
        }

        void LoadSetValue()
        {

            #region ModelName
            textBoxModelName.Text = ayarClass.ModelName();
            #endregion
            #region PixelFormat
            var PixelFormatListResult = ayarClass.PixelFormatList();
            if (PixelFormatListResult.Count > 0)
            {
                foreach (var item in PixelFormatListResult)
                {
                    comboPixelFormat.Items.Add(item);
                }
                comboPixelFormat.SelectedIndex = 0;
            }
            #endregion
            #region ExposureAuto
            var ExposureAuoListResult = ayarClass.ExposureAutoList();
            if (ExposureAuoListResult.Count > 0)
            {
                foreach (var item in ExposureAuoListResult)
                {
                    comboExposureAuto.Items.Add(item);
                }
                comboExposureAuto.SelectedIndex = 0;
            }
            #endregion
            #region GainAuto
            var GainAutoListResult = ayarClass.GainAutoList();
            if (GainAutoListResult.Count > 0)
            {
                foreach (var item in GainAutoListResult)
                {
                    comboGainAuto.Items.Add(item);
                }
                comboGainAuto.SelectedIndex = 0;
            }
            #endregion
            #region BalanceWhiteAuto
            var BalanceWhiteAutoResult = ayarClass.BalanceWhiteAutoList();
            if (BalanceWhiteAutoResult.Count > 0)
            {
                foreach (var item in BalanceWhiteAutoResult)
                {
                    comboBalanceWhiteAuto.Items.Add(item);
                }
                comboBalanceWhiteAuto.SelectedIndex = 0;
            }
            #endregion

            labelMaxWidth.Text += $" - ({ayarClass.MaxWidth()})";
            label6.Text += $" - ({ayarClass.MaxHeight()})";
            label19.Text += $" - ({ayarClass.MaxExposureTime()})";
            label22.Text += $" - ({ayarClass.MaxGainDb()})";
            label26.Text += $" - ({ayarClass.MaxFrameRate()})";

            // cihaz bilgileri dolu ise seçili hale getir.
            if (GlobalStructureDTO.Instance.SystemDeviceCameraDTO.IdCameraSetting != 0)
            {
                textBoxModelName.Text = GlobalStructureDTO.Instance.SystemDeviceCameraDTO.ModelName;
                numericWidth.Value = GlobalStructureDTO.Instance.SystemDeviceCameraDTO.Width;
                numericHeight.Value = GlobalStructureDTO.Instance.SystemDeviceCameraDTO.Height;
                comboPixelFormat.SelectedItem = comboPixelFormat.Items.Cast<ComboboxItem>().FirstOrDefault(item => item.Text == GlobalStructureDTO.Instance.SystemDeviceCameraDTO.PixelFormat);
                numericExposureTime.Value = GlobalStructureDTO.Instance.SystemDeviceCameraDTO.ExposureTime;
                comboExposureAuto.SelectedItem = comboExposureAuto.Items.Cast<ComboboxItem>().FirstOrDefault(item => item.Text == GlobalStructureDTO.Instance.SystemDeviceCameraDTO.ExposureAuto);
                numericGaindB.Value = GlobalStructureDTO.Instance.SystemDeviceCameraDTO.GaindB;
                comboGainAuto.SelectedItem = comboGainAuto.Items.Cast<ComboboxItem>().FirstOrDefault(item => item.Text == GlobalStructureDTO.Instance.SystemDeviceCameraDTO.GainAuto);
                comboBalanceWhiteAuto.SelectedItem = comboBalanceWhiteAuto.Items.Cast<ComboboxItem>().FirstOrDefault(item => item.Text == GlobalStructureDTO.Instance.SystemDeviceCameraDTO.BalanceWhiteAuto);
                checkBoxFrameRate.Checked = GlobalStructureDTO.Instance.SystemDeviceCameraDTO.EnableAcquisitionFrameRate == true ? checkBoxFrameRate.Checked = true : checkBoxFrameRate.Checked = false;
                checkBoxMultipleROI.Checked = GlobalStructureDTO.Instance.SystemDeviceCameraDTO.EnableMultipleROI == true ? checkBoxMultipleROI.Checked = true : checkBoxMultipleROI.Checked = false;
                numericFrameRate.Value = GlobalStructureDTO.Instance.SystemDeviceCameraDTO.AcquisitionFrameRateHz;
                numericWaitinTime.Value = GlobalStructureDTO.Instance.SystemDeviceCameraDTO.WaitingTimeBetweenShots;
                numericOffsetX.Value = GlobalStructureDTO.Instance.SystemDeviceCameraDTO.OffsetX;
                numericOffsetY.Value = GlobalStructureDTO.Instance.SystemDeviceCameraDTO.OffsetY;
                numericMultipleROI.Value = GlobalStructureDTO.Instance.SystemDeviceCameraDTO.MultipleROIvalue;

                GlobalStructureDTO.Instance.BaslerCommunication.SetBaslerParameters();
            }

            textDiskSaveLocation.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\KLKPerformance";
        }

        private void button_kaydet_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(textBoxModelName.Text))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.", Program.UygulamaAdi + " ::: KameraÇekimAyarları", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(numericWidth.Value.ToString()))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.", Program.UygulamaAdi + " ::: KameraÇekimAyarları", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(numericHeight.Value.ToString()))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.", Program.UygulamaAdi + " ::: KameraÇekimAyarları", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (comboPixelFormat.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.", Program.UygulamaAdi + " ::: KameraÇekimAyarları", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(numericExposureTime.Value.ToString()))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.", Program.UygulamaAdi + " ::: KameraÇekimAyarları", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (comboExposureAuto.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.", Program.UygulamaAdi + " ::: KameraÇekimAyarları", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(numericGaindB.Value.ToString()))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.", Program.UygulamaAdi + " ::: KameraÇekimAyarları", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (comboGainAuto.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.", Program.UygulamaAdi + " ::: KameraÇekimAyarları", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (comboBalanceWhiteAuto.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.", Program.UygulamaAdi + " ::: KameraÇekimAyarları", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(numericFrameRate.Value.ToString()))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.", Program.UygulamaAdi + " ::: KameraÇekimAyarları", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(numericWaitinTime.Value.ToString()))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.", Program.UygulamaAdi + " ::: KameraÇekimAyarları", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // set dto
            GlobalStructureDTO.Instance.SystemDeviceCameraDTO.IdCameraSetting = 1;
            GlobalStructureDTO.Instance.SystemDeviceCameraDTO.IdDevice = 1;
            GlobalStructureDTO.Instance.SystemDeviceCameraDTO.ModelName = textBoxModelName.Text;
            GlobalStructureDTO.Instance.SystemDeviceCameraDTO.Width = (int)numericWidth.Value;
            GlobalStructureDTO.Instance.SystemDeviceCameraDTO.Height = (int)numericHeight.Value;
            GlobalStructureDTO.Instance.SystemDeviceCameraDTO.PixelFormat = comboPixelFormat.Text;
            GlobalStructureDTO.Instance.SystemDeviceCameraDTO.ExposureTime = (int)numericExposureTime.Value;
            GlobalStructureDTO.Instance.SystemDeviceCameraDTO.ExposureAuto = comboExposureAuto.Text;
            GlobalStructureDTO.Instance.SystemDeviceCameraDTO.GaindB = (int)numericGaindB.Value;
            GlobalStructureDTO.Instance.SystemDeviceCameraDTO.GainAuto = comboGainAuto.Text;
            GlobalStructureDTO.Instance.SystemDeviceCameraDTO.BalanceWhiteAuto = comboBalanceWhiteAuto.Text;
            GlobalStructureDTO.Instance.SystemDeviceCameraDTO.EnableAcquisitionFrameRate = checkBoxFrameRate.Checked;
            GlobalStructureDTO.Instance.SystemDeviceCameraDTO.AcquisitionFrameRateHz = (int)numericFrameRate.Value;
            GlobalStructureDTO.Instance.SystemDeviceCameraDTO.WaitingTimeBetweenShots = (int)numericWaitinTime.Value;
            GlobalStructureDTO.Instance.SystemDeviceCameraDTO.OffsetX = (int)numericOffsetX.Value;
            GlobalStructureDTO.Instance.SystemDeviceCameraDTO.OffsetY = (int)numericOffsetY.Value;
            GlobalStructureDTO.Instance.SystemDeviceCameraDTO.EnableMultipleROI = checkBoxMultipleROI.Checked;
            GlobalStructureDTO.Instance.SystemDeviceCameraDTO.MultipleROIvalue = (int)numericMultipleROI.Value;

            GlobalStructureDTO.Instance.BaslerCommunication.SetBaslerParameters();

            MessageBox.Show("Kamera çekim ayarları kaydedildi.", Program.UygulamaAdi + " ::: CameraParametersSave", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void linkLabelTestCapture_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            await cameraGorselYakalaAsync();
        }

        #region Kamera Test Methods

        private async Task cameraGorselYakalaAsync()
        {
            // Görsel yakalar
            List<IGrabResult> grabResultList = await CaptureImageFromCameraAsync();

            // Fail
            if (grabResultList is null || grabResultList.Count == 0)
            {
                labelTestCaptureStatus.Invoke(new Action(() =>
                {
                    labelTestCaptureStatus.Text = $"Fail - Görsel yakalanamadı.";
                    labelTestCaptureStatus.Refresh();
                }));
            }
            else
            {
                Console.WriteLine("Çekimler tamamlandı. Ekrana basılıyor.");

                foreach (IGrabResult item in grabResultList)
                {
                    Bitmap latestFrame = await Task.Run(() => GlobalStructureDTO.Instance.BaslerCommunication.GetBaslerGrabResultToBitmapAsync(item));
                    pictureBoxCameraPreview.Invoke(new Action(() =>
                    {
                        using (var clonedImage = (Image)latestFrame.Clone())
                        using (var bitmap = new Bitmap(clonedImage))
                        {
                            pictureBoxCameraPreview.Image = (Image)bitmap.Clone();
                            pictureBoxCameraPreview.Refresh();
                        }

                        labelTestCaptureStatus.Invoke(new Action(() =>
                        {
                            labelTestCaptureStatus.Text = $"Yakalanan görsel yansıtıldı.";
                            labelTestCaptureStatus.Refresh();
                        }));
                    }));

                    if (GlobalStructureDTO.Instance.SystemDeviceCameraDTO.EnableMultipleROI)
                    {
                        // Kolon sayısını belirleyelim
                        int columnCount = 3;

                        int columnWidth = latestFrame.Width / columnCount;
                        int height = latestFrame.Height;

                        for (int i = 0; i < columnCount; i++)
                        {
                            Rectangle cropRect = new Rectangle(i * columnWidth, 0, columnWidth, height);
                            Bitmap columnImage = new Bitmap(columnWidth, height);

                            using (Graphics g = Graphics.FromImage(columnImage))
                            {
                                g.DrawImage(latestFrame, new Rectangle(0, 0, columnWidth, height), cropRect, GraphicsUnit.Pixel);
                            }

                            await SaveBitmapDiskAsync(columnImage);
                            await Task.Delay(700);
                        }
                    }
                    else
                    {
                        await SaveBitmapDiskAsync(latestFrame);
                        await Task.Delay(800);
                    }
                    //await SaveBitmapManfileAsync(latestFrame, performanceDTO);
                }

                Console.WriteLine("Kayıt tamamlandı.");

            }
        }
        async Task<List<IGrabResult>> CaptureImageFromCameraAsync()
        {

            try
            {
                List<IGrabResult> result = await GlobalStructureDTO.Instance.BaslerCommunication.GetBaslerCaptureImageFromCameraSettingFormAsync();

                return result;
            }
            catch (Exception ex)
            {
                labelTestCaptureStatus.Invoke(new Action(() =>
                {
                    labelTestCaptureStatus.Text = $"Fail - Görsel çekilemedi. Detay : {ex.Message}";
                    labelTestCaptureStatus.Refresh();
                }));


                return null;
            }
        }


        public async Task SaveBitmapDiskAsync(Bitmap bitmap)
        {
            Console.WriteLine("Çekimler kaydediliyor.");


            // Kaydedilecek lokasyonu belirleyin
            string saveCameraPictureLocation = textDiskSaveLocation.Text;

            // Dosya konumu var mı?
            if (!Directory.Exists(saveCameraPictureLocation))
            {
                Directory.CreateDirectory(saveCameraPictureLocation);
            }

            var customDate = DateTime.Now.ToString("yyyy:MM:dd:HH:mm:ss").Replace(":", "");

            try
            {
                await Task.Run(() => bitmap.Save(Path.Combine(saveCameraPictureLocation, customDate + ".jpg"), ImageFormat.Jpeg));
            }
            catch (Exception)
            {

                throw;
            }

        }

        #endregion

        private void DeviceCameraSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalStructureDTO.Instance.BaslerCommunication.SetBufferClear();
            GlobalStructureDTO.Instance.BaslerCommunication.GetBaslerCloseCamera();
        }

        private async void linkLabelPlay_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GlobalStructureDTO.Instance.TestUrunGeldi = true;
            linkLabelPlay.Enabled = false;

            await cameraGorselYakalaAsync();

            linkLabelPlay.Enabled = true;
        }

        private void linkLabelStop_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GlobalStructureDTO.Instance.TestUrunGeldi = false;
        }

        private void linkOffsetX_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            numericOffsetX.Value = ayarClass.GetOffsetX();
        }

        private void linkOffsetY_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            numericOffsetY.Value = ayarClass.GetOffsetY();
        }

        private void checkBoxMultipleROI_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxMultipleROI.Checked)
            {
                numericMultipleROI.Value = 0;
            }
        }
    }
}
