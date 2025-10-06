# 📚 API Documentation

BaslerCamera Configuration Interface API dokümantasyonu.

## 📋 İçindekiler

* [Namespace](#namespace)
* [Ana Sınıflar](#ana-sınıflar)
* [Metodlar](#metodlar)
* [Özellikler](#özellikler)
* [Eventler](#eventler)
* [Örnekler](#örnekler)

## Namespace

```csharp
namespace BaslerCameraConfiguration
```

## Ana Sınıflar

### DeviceCameraSettings

Ana uygulama form sınıfı. Kamera konfigürasyon arayüzünü yönetir.

```csharp
public partial class DeviceCameraSettings : Form
```

**Inheritance:** `System.Windows.Forms.Form`

**Assembly:** BaslerCameraConfiguration.exe

### BaslerCommunication

Kamera iletişim işlemlerini yöneten sınıf.

```csharp
public class BaslerCommunication
```

**Namespace:** BaslerCameraConfiguration.Classes

**Assembly:** BaslerCameraConfiguration.exe

### SettingCameraFunc

Kamera ayar fonksiyonlarını içeren sınıf.

```csharp
public class SettingCameraFunc
```

**Namespace:** BaslerCameraConfiguration.Classes

**Assembly:** BaslerCameraConfiguration.exe

## Metodlar

### DeviceCameraSettings

#### Constructor

```csharp
public DeviceCameraSettings()
```

Yeni DeviceCameraSettings örneği oluşturur.

#### LoadSetValue

```csharp
void LoadSetValue()
```

Kamera ayarlarını form kontrollerine yükler.

**Görünürlük:** private

#### KameraCekimAyarlari_Load

```csharp
private void KameraCekimAyarlari_Load(object sender, EventArgs e)
```

Form yüklendiğinde çağrılan event handler.

**Parametreler:**
- `sender` (object): Event'i tetikleyen nesne
- `e` (EventArgs): Event argümanları

### BaslerCommunication

#### Connect

```csharp
public void Connect()
```

Kameraya bağlantı kurar.

**Görünürlük:** public

**Throws:**
- `Exception`: Bağlantı kurulamadığında

#### Disconnect

```csharp
public void Disconnect()
```

Kamera bağlantısını keser.

**Görünürlük:** public

#### CaptureImage

```csharp
public Image CaptureImage()
```

Kameradan görüntü yakalar.

**Görünürlük:** public

**Returns:** `System.Drawing.Image` - Yakalanan görüntü

**Throws:**
- `Exception`: Görüntü yakalanamadığında

### SettingCameraFunc

#### ModelName

```csharp
public string ModelName()
```

Kamera model adını döndürür.

**Görünürlük:** public

**Returns:** `string` - Kamera model adı

#### PixelFormatList

```csharp
public List<string> PixelFormatList()
```

Desteklenen pixel formatlarının listesini döndürür.

**Görünürlük:** public

**Returns:** `List<string>` - Pixel format listesi

#### SetExposureTime

```csharp
public void SetExposureTime(double value)
```

Pozlama süresini ayarlar.

**Görünürlük:** public

**Parametreler:**
- `value` (double): Pozlama süresi (mikrosaniye)

#### SetGain

```csharp
public void SetGain(double value)
```

Kameranın kazancını ayarlar.

**Görünürlük:** public

**Parametreler:**
- `value` (double): Kazanç değeri (dB)

#### SetFrameRate

```csharp
public void SetFrameRate(double value)
```

Kameranın frame rate'ini ayarlar.

**Görünürlük:** public

**Parametreler:**
- `value` (double): Frame rate (FPS)

## Özellikler

### BaslerCommunication

#### Camera

```csharp
public Camera Camera { get; set; }
```

Basler kamera nesnesi.

**Type:** `Basler.Pylon.Camera`

**Görünürlük:** public

#### IsConnected

```csharp
public bool IsConnected { get; set; }
```

Kamera bağlantı durumu.

**Type:** `bool`

**Görünürlük:** public

### DeviceCameraSettings

#### ayarClass

```csharp
private SettingCameraFunc ayarClass
```

Kamera ayar fonksiyonları nesnesi.

**Type:** `SettingCameraFunc`

**Görünürlük:** private

## Eventler

### DeviceCameraSettings

#### KameraCekimAyarlari_Load

Form yüklendiğinde tetiklenen event.

**Event Type:** `EventHandler`

**Görünürlük:** private

## Örnekler

### Temel Kullanım

```csharp
// Uygulamayı başlat
Application.Run(new DeviceCameraSettings());
```

### Kamera Bağlantısı

```csharp
var baslerComm = new BaslerCommunication();
baslerComm.Connect();

if (baslerComm.IsConnected)
{
    // Kamera bağlı, işlemler yapılabilir
    var image = baslerComm.CaptureImage();
}
```

### Kamera Ayarı

```csharp
var settingFunc = new SettingCameraFunc();

// Pozlama süresini ayarla (12ms)
settingFunc.SetExposureTime(12000);

// Kazancı ayarla (5dB)
settingFunc.SetGain(5);

// Frame rate'i ayarla (30 FPS)
settingFunc.SetFrameRate(30);
```

### Desteklenen Pixel Formatları

```csharp
var settingFunc = new SettingCameraFunc();
var pixelFormats = settingFunc.PixelFormatList();

foreach (var format in pixelFormats)
{
    Console.WriteLine($"Desteklenen format: {format}");
}
```

## Hata Yönetimi

### Yaygın Hatalar

#### Kamera Bulunamadı

```csharp
try
{
    baslerComm.Connect();
}
catch (Exception ex)
{
    MessageBox.Show($"Kamera bulunamadı: {ex.Message}");
}
```

#### Görüntü Yakalama Hatası

```csharp
try
{
    var image = baslerComm.CaptureImage();
    if (image != null)
    {
        // Görüntü başarıyla yakalandı
    }
}
catch (Exception ex)
{
    MessageBox.Show($"Görüntü yakalanamadı: {ex.Message}");
}
```

## Bağımlılıklar

### Basler Pylon SDK

```csharp
using Basler.Pylon;
```

### .NET Framework

```csharp
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
```

## Sürüm Geçmişi

- **v1.0.0** - İlk stabil sürüm
- **v0.1.0** - Beta sürüm

## Lisans

Bu API dokümantasyonu MIT lisansı altında lisanslanmıştır.
