using Basler.Pylon;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaslerCameraConfiguration.Classes
{
    public class BaslerCommunication : IDisposable
    {

        private Camera camera;
        private bool disposed = false;

        public BaslerCommunication()
        {
            try
            {
                camera = new Camera();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n!!! Fail - BaslerCommunication() -> {ex.Message}");
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // Finalizer çalışmasın
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // managed resources
                if (camera != null)
                {
                    if (camera.IsOpen)
                        camera.Close();

                    camera.Dispose();
                    camera = null;
                }
            }

            // unmanaged resources burada temizlenirdi (varsa)

            disposed = true;
        }

        // Finalizer (opsiyonel, unmanaged kaynak varsa gerekir)
        ~BaslerCommunication()
        {
            Dispose(false); // managed değil, sadece unmanaged temizlenir
        }

        private void EnsureCameraIsOpen()
        {
            EnsureCameraInitialized();
            if (!camera.IsOpen)
                GetBaslerOpenCamera();
        }
        private void EnsureCameraInitialized()
        {
            if (camera == null)
            {
                NewBasler();
            }
        }
        public void NewBasler()
        {
            try
            {
                camera = new Camera();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n!!! Fail - BaslerCommunication() -> {ex.Message}");
            }
        }


        #region Camera Running Function

        public bool GetBaslerOpenCamera()
        {
            try
            {
                camera.Open();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n!!! Fail - BaslerOpenCamera() -> {ex.Message}");
                return false;
            }
        }
        public async Task<bool> GetBaslerOpenCameraAsync()
        {
            try
            {
                EnsureCameraInitialized();

                await Task.Run(() => camera.Open());
                Console.WriteLine($"Success - BaslerOpenCameraAsync()");

                return true;
            }
            catch (Exception ex)
            {
                camera.Close();
                camera.Dispose();

                Console.WriteLine($"\n!!! Fail - BaslerOpenCameraAsync() -> {ex.Message}");
                return false;
            }
        }
        public bool GetBaslerCloseCamera()
        {
            try
            {
                EnsureCameraIsOpen();

                if (camera.IsOpen)
                {
                    camera.Close();
                    Console.WriteLine($"Success - BaslerCloseCamera()");
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n!!! Fail - BaslerCloseCamera() -> {ex.Message}");
                return false;
            }
        }
        public async Task<bool> GetBaslerCloseCameraAsync()
        {
            try
            {
                EnsureCameraInitialized();

                if (camera.IsOpen)
                {
                    await Task.Run(() => camera.Close());
                    Console.WriteLine($"Success - BaslerCloseCameraAsync()");
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n!!! Fail - BaslerCloseCameraAsync() -> {ex.Message}");
                return false;
            }
        }
        public bool GetBaslerCameraIsOpen()
        {
            try
            {
                EnsureCameraIsOpen();

                Console.WriteLine($"Success - GetBaslerCameraStatus()");
                return camera.IsOpen;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n!!! Fail - GetBaslerCameraStatus() -> {ex.Message}");
                return false;
            }
        }
        public async Task<bool> GetBaslerCameraIsOpenAsync()
        {
            try
            {
                EnsureCameraInitialized();

                Console.WriteLine($"Success - GetBaslerCameraStatusAsync() - {DateTime.Now}");
                return await Task.Run(() => camera.IsOpen);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n!!! Fail - GetBaslerCameraStatusAsync() - {DateTime.Now} -> {ex.Message}");
                return false;
            }
        }
        public async Task<List<IGrabResult>> GetBaslerCaptureImageFromCameraSettingFormAsync()
        {
            List<IGrabResult> grabResultsList = new List<IGrabResult>();

            try
            {
                EnsureCameraIsOpen();

                if (!camera.StreamGrabber.IsGrabbing)
                {
                    camera.StreamGrabber.Start(GrabStrategy.OneByOne, GrabLoop.ProvidedByUser);
                    await Task.Delay(100); // Kamera veri toplamaya başlasın
                }

                do
                {
                    IGrabResult grabResult = null;

                    try
                    {
                        grabResult = camera.StreamGrabber.RetrieveResult(100, TimeoutHandling.ThrowException);

                        if (grabResult != null && grabResult.GrabSucceeded)
                        {
                            grabResultsList.Add(grabResult); // Dispose edilmeden ekleniyor, daha sonra sen dispose etmelisin
                                                             //logClass.Log(AppLogType.Warning, $"{index}. Fotoğraf çekildi...", richTextBox, label);
                            await Task.Delay(10); // Fotoğraf çekildikten sonra bekle
                        }
                        else
                        {
                            Console.WriteLine($"{grabResultsList.Count + 1}. Fotoğraf çekilemedi... grab exception: {grabResult?.ErrorDescription}");
                            //logClass.Log(AppLogType.Error, $"{index}. Fotoğraf çekilemedi...", richTextBox, label);
                        }
                    }
                    catch (System.OperationCanceledException)
                    {
                        break;
                    }
                    catch (Exception innerEx)
                    {
                        Console.WriteLine($"Fail Frame grab exception: {innerEx.Message}");
                        break;
                    }
                } while (GlobalStructureDTO.Instance.TestUrunGeldi);

                if (camera.StreamGrabber.IsGrabbing)
                    camera.StreamGrabber.Stop();

                return grabResultsList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n!!! Fail - GetBaslerCaptureImageFromCameraAsync() -> {ex.Message}");
                return null;
            }
        }
        public Bitmap GetBaslerGrabResultToBitmapAsync(IGrabResult grabResult)
        {
            PixelDataConverter converter = new PixelDataConverter();
            Bitmap bitmap = new Bitmap(grabResult.Width, grabResult.Height);


            lock (bitmap)
            {
                BitmapData bmpData = bitmap.LockBits(
                    new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                    ImageLockMode.ReadWrite,
                    bitmap.PixelFormat);

                converter.OutputPixelFormat = PixelType.BGRA8packed;
                IntPtr ptrBmp = bmpData.Scan0;
                converter.Convert(ptrBmp, bmpData.Stride * bitmap.Height, grabResult);
                bitmap.UnlockBits(bmpData);
            }

            return bitmap;
        }
        public bool SetBaslerParameters()
        {
            try
            {
                if (!camera.IsOpen)
                    GetBaslerOpenCamera(); // open camera

                if (camera.Parameters.Contains(PLCamera.Width.ToString()))
                    camera.Parameters[PLCamera.Width].TrySetValue(GlobalStructureDTO.Instance.SystemDeviceCameraDTO.Width);

                if (camera.Parameters.Contains(PLCamera.Height.ToString()))
                    camera.Parameters[PLCamera.Height].TrySetValue(GlobalStructureDTO.Instance.SystemDeviceCameraDTO.Height);

                if (camera.Parameters.Contains(PLCamera.PixelFormat.ToString()))
                    camera.Parameters[PLCamera.PixelFormat].TrySetValue(GlobalStructureDTO.Instance.SystemDeviceCameraDTO.PixelFormat);

                if (camera.Parameters.Contains(PLCamera.ExposureTime.ToString()))
                    camera.Parameters[PLCamera.ExposureTime].TrySetValue(GlobalStructureDTO.Instance.SystemDeviceCameraDTO.ExposureTime);

                if (camera.Parameters.Contains(PLCamera.ExposureAuto.ToString()))
                    camera.Parameters[PLCamera.ExposureAuto].TrySetValue(GlobalStructureDTO.Instance.SystemDeviceCameraDTO.ExposureAuto);

                if (camera.Parameters.Contains(PLCamera.Gain.ToString()))
                    camera.Parameters[PLCamera.Gain].TrySetValue(GlobalStructureDTO.Instance.SystemDeviceCameraDTO.GaindB);

                if (camera.Parameters.Contains(PLCamera.GainAuto.ToString()))
                    camera.Parameters[PLCamera.GainAuto].TrySetValue(GlobalStructureDTO.Instance.SystemDeviceCameraDTO.GainAuto);

                if (camera.Parameters.Contains(PLCamera.BalanceWhiteAuto.ToString()))
                    camera.Parameters[PLCamera.BalanceWhiteAuto].TrySetValue(GlobalStructureDTO.Instance.SystemDeviceCameraDTO.BalanceWhiteAuto);

                if (camera.Parameters.Contains(PLCamera.AcquisitionFrameRateEnable.ToString()))
                    camera.Parameters[PLCamera.AcquisitionFrameRateEnable].TrySetValue(GlobalStructureDTO.Instance.SystemDeviceCameraDTO.EnableAcquisitionFrameRate);

                if (camera.Parameters.Contains(PLCamera.AcquisitionFrameRate.ToString()))
                    camera.Parameters[PLCamera.AcquisitionFrameRate].TrySetValue(GlobalStructureDTO.Instance.SystemDeviceCameraDTO.AcquisitionFrameRateHz);

                if (camera.Parameters.Contains(PLCamera.AcquisitionMode.ToString()))
                    camera.Parameters[PLCamera.AcquisitionMode].SetValue("Continuous"); //SingleFrame, Continuous

                if (camera.Parameters.Contains(PLCamera.OffsetX.ToString()))
                    camera.Parameters[PLCamera.OffsetX].TrySetValue(GlobalStructureDTO.Instance.SystemDeviceCameraDTO.OffsetX);

                if (camera.Parameters.Contains(PLCamera.OffsetY.ToString()))
                    camera.Parameters[PLCamera.OffsetY].SetValue(GlobalStructureDTO.Instance.SystemDeviceCameraDTO.OffsetY);

                if (camera.Parameters.Contains(PLCamera.TriggerMode.ToString()))
                    camera.Parameters[PLCamera.TriggerMode].SetValue("Off");

                camera.Parameters[PLCameraInstance.MaxNumBuffer].SetValue(15); // Yeterli buffer ayarla

                if (GlobalStructureDTO.Instance.SystemDeviceCameraDTO.EnableMultipleROI)
                {
                    if (camera.Parameters.Contains(PLCamera.BslMultipleROIColumnsEnable.ToString()))
                    {
                        // ** Multiple ROI

                        int sizeValue = 0;
                        int offSetValue = 0;
                        int nextValue = GlobalStructureDTO.Instance.SystemDeviceCameraDTO.MultipleROIvalue;

                        // column enable on
                        camera.Parameters[PLCamera.BslMultipleROIColumnsEnable].SetValue(true);
                        // row enable off
                        camera.Parameters[PLCamera.BslMultipleROIRowsEnable].SetValue(false);

                        for (int i = 1; i <= 8; i++)
                        {
                            if (i % 3 == 0)
                                sizeValue = 0;
                            else
                                sizeValue = nextValue;

                            string ColumnName = "Column" + (i);
                            camera.Parameters[PLCamera.BslMultipleROIColumnSelector].SetValue(ColumnName);
                            camera.Parameters[PLCamera.BslMultipleROIColumnSize].SetValue(sizeValue);
                            camera.Parameters[PLCamera.BslMultipleROIColumnOffset].SetValue(offSetValue);

                            Console.WriteLine("ColumnName : " + ColumnName);
                            Console.WriteLine("ColumnSize : " + sizeValue);
                            Console.WriteLine("ColumnOffset : " + offSetValue + "\n");

                            offSetValue += nextValue;
                        }
                    }
                }
                else
                {
                    // column enable on
                    camera.Parameters[PLCamera.BslMultipleROIColumnsEnable].SetValue(false);
                    // row enable off
                    camera.Parameters[PLCamera.BslMultipleROIRowsEnable].SetValue(false);
                }

                Console.WriteLine($"Success - SetBaslerParameters()");
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Fail -> " + ex.Message);
                Console.WriteLine($"\n!!! Fail - SetBaslerParameters() -> {ex.Message}");
                return false;
            }
        }
        public bool SetBufferClear()
        {
            try
            {
                EnsureCameraIsOpen();

                camera.StreamGrabber.BufferFactory = null;
                GC.Collect();

                Console.WriteLine($"Success - SetBufferClear() - {DateTime.Now}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n!!! Fail - SetBufferClear() - {DateTime.Now} -> {ex.Message}");
                return false;
            }
        }

        #endregion

        #region Camera Info's
        public Camera BaslerCommunicationCamera()
        {
            return camera;
        }
        public string GetModelName()
        {
            try
            {
                EnsureCameraIsOpen();

                Console.WriteLine($"Success - GetModelName()");
                return camera.CameraInfo[CameraInfoKey.ModelName];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n!!! Fail - GetModelName() -> {ex.Message}");
                return null;
            }
        }

        public long GetMaxWidth()
        {
            try
            {
                EnsureCameraIsOpen();

                Console.WriteLine($"Success - GetMaxWidth()");
                return camera.Parameters[PLCamera.Width].GetMaximum();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n!!! Fail - GetMaxWidth() -> {ex.Message}");
                return 0;
            }
        }
        public long GetMaxHeight()
        {
            try
            {
                EnsureCameraIsOpen();

                Console.WriteLine($"Success - GetMaxHeight()");
                return camera.Parameters[PLCamera.Height].GetMaximum();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n!!! Fail - GetMaxHeight() -> {ex.Message}");
                return 0;
            }
        }
        public double GetMaxExposureTime()
        {
            try
            {
                EnsureCameraIsOpen();

                Console.WriteLine($"Success - GetMaxExposureTime()");
                return camera.Parameters[PLCamera.ExposureTime].GetMaximum();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n!!! Fail - GetMaxExposureTime() -> {ex.Message}");
                return 0;
            }
        }
        public double GetMaxGainDb()
        {
            try
            {
                EnsureCameraIsOpen();

                Console.WriteLine($"Success - GetMaxGainDb()");
                return camera.Parameters[PLCamera.Gain].GetMaximum();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n!!! Fail - GetMaxGainDb() -> {ex.Message}");
                return 0;
            }
        }
        public double GetMaxFrameRate()
        {
            try
            {
                EnsureCameraIsOpen();

                Console.WriteLine($"Success - GetMaxFrameRate()");
                return camera.Parameters[PLCamera.AcquisitionFrameRate].GetMaximum();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n!!! Fail - GetMaxFrameRate() -> {ex.Message}");
                return 0;
            }
        }
        public decimal GetOffsetX()
        {
            try
            {
                EnsureCameraIsOpen();

                camera.Parameters[PLCamera.BslCenterX].Execute();
                Console.WriteLine($"Success - GetOffsetX()");

                return camera.Parameters[PLCamera.OffsetX].GetValue();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n!!! Fail - GetOffsetX() -> {ex.Message}");
                return 0;
            }
        }
        public decimal GetOffsetY()
        {
            try
            {
                EnsureCameraIsOpen();

                camera.Parameters[PLCamera.BslCenterY].Execute();
                Console.WriteLine($"Success - GetOffsetY()");

                return camera.Parameters[PLCamera.OffsetY].GetValue();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n!!! Fail - GetOffsetY() -> {ex.Message}");
                return 0;
            }
        }

        public List<string> GetPixelFormatList()
        {
            try
            {
                EnsureCameraIsOpen();

                if (camera.Parameters.Contains(PLCamera.PixelFormat.ToString()))
                {
                    Console.WriteLine($"Success - GetPixelFormatList()");
                    return camera.Parameters[PLCamera.PixelFormat].ToList();
                }

                Console.WriteLine($"\n!!! Fail - GetPixelFormatList() -> camera.Parameters.Contains(PLCamera.PixelFormat) Not Found !");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n!!! Fail - GetPixelFormatList() -> {ex.Message}");
                return null;
            }
        }
        public List<string> GetExposureAutoList()
        {
            try
            {
                EnsureCameraIsOpen();

                if (camera.Parameters.Contains(PLCamera.ExposureAuto.ToString()))
                {
                    Console.WriteLine($"Success - GetExposureAutoList()");
                    return camera.Parameters[PLCamera.ExposureAuto].ToList();
                }

                Console.WriteLine($"\n!!! Fail - GetExposureAutoList() -> camera.Parameters.Contains(PLCamera.ExposureAuto) Not Found !");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n!!! Fail - GetExposureAutoList() -> {ex.Message}");
                return null;
            }
        }
        public List<string> GetGainAutoList()
        {
            try
            {
                EnsureCameraIsOpen();

                if (camera.Parameters.Contains(PLCamera.GainAuto.ToString()))
                {
                    Console.WriteLine($"Success - GetGainAutoList()");
                    return camera.Parameters[PLCamera.GainAuto].ToList();
                }

                Console.WriteLine($"\n!!! Fail - GetGainAutoList() -> camera.Parameters.Contains(PLCamera.GainAuto) Not Found !");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n!!! Fail - GetGainAutoList() -> {ex.Message}");
                return null;
            }
        }
        public List<string> GetBalanceWhiteAutoList()
        {
            try
            {
                EnsureCameraIsOpen();

                if (camera.Parameters.Contains(PLCamera.BalanceWhiteAuto.ToString()))
                {
                    Console.WriteLine($"Success - GetBalanceWhiteAutoList()");
                    return camera.Parameters[PLCamera.BalanceWhiteAuto].ToList();
                }

                Console.WriteLine($"\n!!! Fail - GetBalanceWhiteAutoList() -> camera.Parameters.Contains(PLCamera.BalanceWhiteAuto) Not Found !");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n!!! Fail - GetBalanceWhiteAutoList() -> {ex.Message}");
                return null;
            }
        }

        #endregion


    }
}
