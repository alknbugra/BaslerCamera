# 📷 Basler A2A2448-75ucBAS Kamera Konfigürasyon Arayüzü

[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)
[![Build Status](https://img.shields.io/badge/Build-Passing-brightgreen.svg)](https://github.com/alknbugra/BaslerCamera/actions)
[![Platform](https://img.shields.io/badge/Platform-.NET%20Framework%204.8-blueviolet.svg)](https://dotnet.microsoft.com/download/dotnet-framework)
[![Basler SDK](https://img.shields.io/badge/Basler-Pylon%20SDK-orange.svg)](https://www.baslerweb.com/en/products/software/pylon-camera-software-suite/)
[![Last Commit](https://img.shields.io/github/last-commit/alknbugra/BaslerCamera?color=orange)](https://github.com/alknbugra/BaslerCamera/commits/main)
[![Release](https://img.shields.io/github/v/release/alknbugra/BaslerCamera)](https://github.com/alknbugra/BaslerCamera/releases)

**Basler A2A2448-75ucBAS** endüstriyel kamerası için geliştirilmiş profesyonel C# konfigürasyon arayüzü. Exposure, Gain, Trigger ve diğer kamera parametrelerinin yönetimini kolaylaştırır.

## 📋 İçindekiler

* [Özellikler](#-özellikler)
* [Gereksinimler](#-gereksinimler)
* [Kurulum](#-kurulum)
* [Kullanım](#-kullanım)
* [Proje Yapısı](#-proje-yapısı)
* [API Referansı](#-api-referansı)
* [Troubleshooting](#-troubleshooting)
* [Katkıda Bulunma](#-katkıda-bulunma)
* [Lisans](#-lisans)

## ✨ Özellikler

### 🎯 Temel Özellikler

* **🔗 Otomatik Kamera Keşfi** - USB 3.0 üzerinden Basler kameralarını otomatik bulma
* **📱 Gerçek Zamanlı Görüntü Alma** - Tek kare veya sürekli görüntü alma
* **🖼️ Canlı Önizleme** - Anlık görüntü önizlemesi ve kaydetme
* **⚡ Thread-Safe İşlemler** - UI thread'i bloklamadan güvenli kamera işlemleri
* **🔧 Esnek Konfigürasyon** - Tüm kamera parametrelerinin kolay yönetimi

### 🛠️ Teknik Özellikler

* **Basler Pylon SDK** entegrasyonu
* **USB 3.0** yüksek hızlı veri transferi
* **CMOS sensör** desteği (2448 x 2048, 75 fps)
* **Multi-threading** mimarisi
* **XML konfigürasyon** dosyası desteği

### 📊 Desteklenen Kamera Parametreleri

* **Exposure Time** (Pozlama süresi)
* **Gain** (Kazanç ayarı)
* **White Balance** (Beyaz dengesi)
* **Frame Rate** (FPS kontrolü)
* **Trigger Mode** (Software/Hardware tetikleme)
* **Pixel Format** seçimleri
* **ROI** (Region of Interest) ayarları
* **Gamma, Brightness, Contrast** kontrolleri

## 🔧 Gereksinimler

### Donanım Gereksinimleri

* **Basler A2A2448-75ucBAS** kamera cihazı
* **USB 3.0** portu (yüksek hızlı veri transferi için)
* **Windows 10/11** (64-bit önerilen)
* **Minimum 4GB RAM**
* **1GB boş disk alanı**

### Yazılım Gereksinimleri

* **.NET Framework 4.8** veya üzeri
* **Visual Studio 2019/2022** (geliştirme için)
* **Basler Pylon SDK** (projede dahil)

## 🚀 Kurulum

### 1. Repository'yi Klonlayın

```bash
git clone https://github.com/alknbugra/BaslerCamera.git
cd BaslerCamera
```

### 2. Visual Studio ile Açın

```bash
# Visual Studio ile
start BaslerCameraConfiguration/BaslerCameraConfiguration.sln

# Veya dotnet CLI ile
dotnet restore BaslerCameraConfiguration/BaslerCameraConfiguration.sln
```

### 3. Bağımlılıkları Kontrol Edin

Proje klasöründe `dll/` dizininde gerekli Basler SDK dosyaları bulunmaktadır:

* `Basler.Pylon.dll`

### 4. Uygulamayı Çalıştırın

```bash
# Visual Studio'dan F5 ile
# Veya build edip exe'yi çalıştırın
```

## 📖 Kullanım

### Temel Kullanım

1. **Basler A2A2448-75ucBAS kameranı** USB 3.0 kablosu ile bilgisayara bağlayın
2. **Uygulamayı başlatın** - kamera otomatik olarak keşfedilecektir
3. **Parametreleri ayarlayın** - Exposure, Gain, Frame Rate vb.
4. **Görüntü alın** - Tek kare veya sürekli modda

### Arayüz Bileşenleri

* **📷 Kamera Bağlantı Paneli** - Kamera durumu ve bağlantı kontrolü
* **⚙️ Parametre Ayarları** - Tüm kamera parametrelerinin yönetimi
* **🖼️ Görüntü Önizleme** - Canlı görüntü görüntüleme
* **💾 Kaydetme Seçenekleri** - Görüntü ve ayar kaydetme

### Desteklenen Kamera Modelleri

* **Basler A2A2448-75ucBAS** (Ana desteklenen model)
* **Diğer Basler USB 3.0 kameraları** (Pylon SDK uyumlu)

## 📁 Proje Yapısı

```
BaslerCamera/
├── 📁 BaslerCameraConfiguration/          # Ana proje klasörü
│   ├── 📁 BaslerCameraConfiguration/      # Kaynak kodlar
│   │   ├── 📁 Classes/                    # İş mantığı sınıfları
│   │   │   ├── 📄 BaslerCommunication.cs  # Kamera iletişim sınıfı
│   │   │   ├── 📄 SettingCameraFunc.cs    # Kamera ayar fonksiyonları
│   │   │   └── 📄 DeviceCameraSettingDTO.cs # Veri transfer nesneleri
│   │   ├── 📁 dll/                        # Basler SDK dosyaları
│   │   ├── 📁 Properties/                 # Proje özellikleri
│   │   ├── 📄 DeviceCameraSettings.cs     # Ana form sınıfı
│   │   ├── 📄 Program.cs                  # Uygulama giriş noktası
│   │   └── 📄 *.csproj                    # Proje dosyası
│   └── 📄 BaslerCameraConfiguration.sln   # Solution dosyası
├── 📁 images/                             # Ekran görüntüleri
├── 📁 .github/workflows/                  # GitHub Actions
├── 📄 README.md                           # Bu dosya
├── 📄 LICENSE                             # MIT lisansı
└── 📄 .gitignore                          # Git ignore kuralları
```

## 🔌 API Referansı

### Ana Sınıflar

#### `DeviceCameraSettings` - Ana Uygulama Sınıfı

```csharp
public partial class DeviceCameraSettings : Form
{
    private SettingCameraFunc ayarClass;           // Kamera ayar fonksiyonları
    private BaslerCommunication baslerComm;        // Basler iletişim nesnesi
}
```

#### `BaslerCommunication` - Kamera İletişim Sınıfı

```csharp
public class BaslerCommunication
{
    public Camera Camera { get; set; }             // Basler kamera nesnesi
    public bool IsConnected { get; set; }          // Bağlantı durumu
    public void Connect()                          // Kamera bağlantısı
    public void Disconnect()                       // Kamera bağlantısını kes
    public Image CaptureImage()                    // Görüntü yakalama
}
```

#### `SettingCameraFunc` - Kamera Ayar Fonksiyonları

```csharp
public class SettingCameraFunc
{
    public string ModelName()                      // Kamera model adı
    public List<string> PixelFormatList()          // Desteklenen pixel formatları
    public void SetExposureTime(double value)      // Pozlama süresi ayarla
    public void SetGain(double value)              // Kazanç ayarla
    public void SetFrameRate(double value)         // FPS ayarla
}
```

### Temel Metodlar

```csharp
// Kamera bağlantısı
private void ConnectCamera()

// Görüntü yakalama
private void CaptureImage()

// Parametre ayarlama
private void SetCameraParameters()

// Ayar kaydetme
private void SaveSettings()
```

## 🔧 Troubleshooting

### Yaygın Sorunlar

#### ❌ "Kamera bulunamadı" Hatası

**Çözüm:**

1. USB 3.0 kablosunun doğru bağlandığından emin olun
2. Kameranın güç aldığından kontrol edin
3. Basler Pylon SDK'nın yüklü olduğunu kontrol edin
4. Farklı bir USB portu deneyin
5. Kamera sürücülerinin güncel olduğunu kontrol edin

#### ❌ "SDK yüklenemedi" Hatası

**Çözüm:**

1. `dll/` klasöründeki `Basler.Pylon.dll` dosyasının mevcut olduğunu kontrol edin
2. .NET Framework 4.8'in yüklü olduğunu kontrol edin
3. Visual C++ Redistributable'ı yükleyin
4. Basler Pylon Runtime'ı yükleyin

#### ❌ "Görüntü alınamıyor" Hatası

**Çözüm:**

1. Kamera parametrelerini kontrol edin (Exposure, Gain)
2. USB 3.0 portu kullandığınızdan emin olun
3. Kamera bağlantısını yeniden başlatın
4. Frame Rate ayarlarını kontrol edin

#### ❌ "Mimari Uyumsuzluğu" Uyarısı

**Çözüm:**

1. Proje ayarlarında Platform Target'ı "Any CPU" olarak ayarlayın
2. Basler.Pylon.dll dosyasının doğru mimaride olduğunu kontrol edin
3. 64-bit sistemde çalıştırıyorsanız x64 DLL kullanın

### Debug Modu

```csharp
// Console.WriteLine çıktılarını görmek için
// Visual Studio Output penceresini açın
// Veya Debug modunda çalıştırın
```

## 🤝 Katkıda Bulunma

Bu projeye katkıda bulunmak için:

1. **Fork** edin
2. **Feature branch** oluşturun (`git checkout -b feature/AmazingFeature`)
3. **Commit** edin (`git commit -m 'Add some AmazingFeature'`)
4. **Push** edin (`git push origin feature/AmazingFeature`)
5. **Pull Request** oluşturun

### Geliştirme Kuralları

* **C# Coding Standards** kullanın
* **XML Documentation** ekleyin
* **Unit Test** yazın (mümkünse)
* **README** güncelleyin
* **Basler Pylon SDK** dokümantasyonunu takip edin

## 📄 Lisans

Bu proje MIT Lisansı altında lisanslanmıştır. Detaylar için [LICENSE](LICENSE) dosyasına bakın.

## 👨‍💻 Geliştirici

**Buğra Alkın** - @alknbugra

* 🔗 **GitHub:** [@alknbugra](https://github.com/alknbugra)
* 📧 **Email:** alknbugra@gmail.com

## 🙏 Teşekkürler

* **Basler AG** - Pylon SDK için
* **Microsoft** - .NET Framework için
* **Açık kaynak topluluğu** - İlham ve destek için

## 📚 Kaynaklar

* [Basler Pylon SDK Dokümantasyonu](https://docs.baslerweb.com/pylon)
* [Basler A2A2448-75ucBAS Ürün Sayfası](https://www.baslerweb.com/en/shop/a2a2448-75ucbas/)
* [.NET Framework Dokümantasyonu](https://docs.microsoft.com/en-us/dotnet/framework/)

---

**⭐ Bu projeyi beğendiyseniz yıldız vermeyi unutmayın!**

[![GitHub stars](https://img.shields.io/github/stars/alknbugra/BaslerCamera?style=social)](https://github.com/alknbugra/BaslerCamera/stargazers)
[![GitHub forks](https://img.shields.io/github/forks/alknbugra/BaslerCamera?style=social)](https://github.com/alknbugra/BaslerCamera/network/members)