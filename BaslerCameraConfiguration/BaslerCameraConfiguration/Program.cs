using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaslerCameraConfiguration
{
    /// <summary>
    /// BaslerCamera Configuration uygulamasının ana giriş noktası.
    /// </summary>
    internal static class Program
    {
        #region Constants

        /// <summary>
        /// Uygulama adı
        /// </summary>
        public const string UygulamaAdi = "Basler Camera Configuration";

        #endregion

        #region Main Method

        /// <summary>
        /// Uygulamanın ana giriş noktası.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                // Windows Forms uygulamasını başlat
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                
                // Ana formu çalıştır
                Application.Run(new DeviceCameraSettings());
            }
            catch (Exception ex)
            {
                // Kritik hataları yakala ve göster
                MessageBox.Show(
                    $"Uygulama başlatılırken kritik hata oluştu:\n\n{ex.Message}\n\nDetaylar: {ex.StackTrace}",
                    "Kritik Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
