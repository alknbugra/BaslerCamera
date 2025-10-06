# 🤝 Katkıda Bulunma Rehberi

BaslerCamera projesine katkıda bulunmak için teşekkürler! Bu rehber, projeye nasıl katkıda bulunabileceğinizi açıklar.

## 📋 İçindekiler

* [Katkı Türleri](#katkı-türleri)
* [Geliştirme Ortamı](#geliştirme-ortamı)
* [Kod Standartları](#kod-standartları)
* [Pull Request Süreci](#pull-request-süreci)
* [Hata Bildirimi](#hata-bildirimi)
* [Özellik İsteği](#özellik-isteği)

## Katkı Türleri

### 🐛 Hata Düzeltmeleri

* Mevcut hataları düzeltme
* Performans iyileştirmeleri
* Kod optimizasyonları

### ✨ Yeni Özellikler

* Yeni kamera parametreleri
* UI/UX iyileştirmeleri
* Yeni kamera modeli desteği

### 📚 Dokümantasyon

* README güncellemeleri
* API dokümantasyonu
* Kod yorumları

### 🧪 Testler

* Unit testler
* Integration testler
* Manuel test senaryoları

## Geliştirme Ortamı

### Gereksinimler

* **Visual Studio 2019/2022** veya **Visual Studio Code**
* **.NET Framework 4.8**
* **Basler Pylon SDK**
* **Git**

### Kurulum

1. Repository'yi fork edin
2. Fork'u klonlayın:
   ```bash
   git clone https://github.com/YOUR_USERNAME/BaslerCamera.git
   cd BaslerCamera
   ```

3. Upstream remote'u ekleyin:
   ```bash
   git remote add upstream https://github.com/alknbugra/BaslerCamera.git
   ```

4. Projeyi Visual Studio'da açın:
   ```bash
   start BaslerCameraConfiguration/BaslerCameraConfiguration.sln
   ```

### Geliştirme Süreci

1. **Feature branch oluşturun:**
   ```bash
   git checkout -b feature/your-feature-name
   ```

2. **Değişikliklerinizi yapın**

3. **Test edin:**
   ```bash
   # Build test
   msbuild BaslerCameraConfiguration/BaslerCameraConfiguration.sln /p:Configuration=Release
   
   # Uygulamayı çalıştırın ve test edin
   ```

4. **Commit edin:**
   ```bash
   git add .
   git commit -m "feat: add new camera parameter support"
   ```

5. **Push edin:**
   ```bash
   git push origin feature/your-feature-name
   ```

6. **Pull Request oluşturun**

## Kod Standartları

### C# Kod Standartları

* **PascalCase** - Sınıf, metod ve özellik isimleri
* **camelCase** - Değişken ve parametre isimleri
* **UPPER_CASE** - Sabitler

```csharp
// ✅ Doğru
public class CameraSettings
{
    private const int MAX_EXPOSURE_TIME = 1000000;
    private string cameraModel;
    
    public string CameraModel 
    { 
        get => cameraModel; 
        set => cameraModel = value; 
    }
    
    public void SetExposureTime(int value)
    {
        // Implementation
    }
}

// ❌ Yanlış
public class camera_settings
{
    private const int max_exposure_time = 1000000;
    private string CameraModel;
    
    public void set_exposure_time(int Value)
    {
        // Implementation
    }
}
```

### XML Dokümantasyon

Tüm public metodlar ve özellikler için XML dokümantasyon ekleyin:

```csharp
/// <summary>
/// Kameranın pozlama süresini ayarlar.
/// </summary>
/// <param name="value">Pozlama süresi (mikrosaniye)</param>
/// <exception cref="ArgumentException">Geçersiz değer verildiğinde</exception>
public void SetExposureTime(int value)
{
    // Implementation
}
```

### Kod Organizasyonu

* **Namespace kullanımı** - Tüm sınıflar uygun namespace'lerde
* **Using direktifleri** - Gereksiz using'leri kaldırın
* **Region kullanımı** - Büyük sınıflarda region'lar kullanın

```csharp
using System;
using System.Collections.Generic;
using Basler.Pylon;

namespace BaslerCameraConfiguration.Classes
{
    public class CameraController
    {
        #region Private Fields
        private Camera camera;
        private bool isConnected;
        #endregion

        #region Public Properties
        public bool IsConnected => isConnected;
        #endregion

        #region Public Methods
        public void Connect()
        {
            // Implementation
        }
        #endregion
    }
}
```

## Pull Request Süreci

### PR Oluşturma

1. **Açıklayıcı başlık** yazın:
   ```
   feat: add support for new camera parameters
   fix: resolve connection timeout issue
   docs: update API documentation
   ```

2. **Detaylı açıklama** ekleyin:
   ```markdown
   ## Değişiklikler
   - Yeni kamera parametreleri eklendi
   - UI güncellemeleri yapıldı
   - Hata düzeltmeleri

   ## Test Edilen Durumlar
   - [x] Kamera bağlantısı test edildi
   - [x] Yeni parametreler çalışıyor
   - [x] UI güncellemeleri doğru

   ## Ekran Görüntüleri
   [Varsa ekran görüntüleri ekleyin]
   ```

3. **Checklist'i tamamlayın:**
   - [ ] Kod standartlarına uygun
   - [ ] Test edildi
   - [ ] Dokümantasyon güncellendi
   - [ ] Breaking change yok

### PR Review Süreci

* **Otomatik kontroller** - CI/CD pipeline çalışacak
* **Code review** - Maintainer'lar kodu inceleyecek
* **Geri bildirim** - Gerekirse değişiklik istenecek
* **Merge** - Onaylandıktan sonra merge edilecek

## Hata Bildirimi

### Hata Raporu Oluşturma

1. **Issue oluşturun** - GitHub Issues sekmesinden
2. **Açıklayıcı başlık** yazın
3. **Detaylı açıklama** ekleyin:

```markdown
## Hata Açıklaması
Kamera bağlantısı kurulurken timeout hatası alınıyor.

## Adımlar
1. Uygulamayı başlat
2. Kamera bağlantısını dene
3. Hata mesajı görüntülenir

## Beklenen Davranış
Kamera başarıyla bağlanmalı.

## Gerçek Davranış
"Connection timeout" hatası alınıyor.

## Ortam
- Windows 10
- .NET Framework 4.8
- Basler A2A2448-75ucBAS

## Ek Bilgiler
Hata logları ve ekran görüntüleri ekleyin.
```

## Özellik İsteği

### Özellik İsteği Oluşturma

1. **Issue oluşturun** - "Feature Request" label'ı ile
2. **Açıklayıcı başlık** yazın
3. **Detaylı açıklama** ekleyin:

```markdown
## Özellik Açıklaması
Yeni kamera parametresi desteği isteniyor.

## Kullanım Senaryosu
Kullanıcılar daha fazla kamera parametresini kontrol etmek istiyor.

## Önerilen Çözüm
Yeni parametre kontrolleri eklenebilir.

## Alternatif Çözümler
Mevcut parametreler genişletilebilir.

## Ek Bilgiler
Örnekler ve referanslar ekleyin.
```

## Kod Review Kriterleri

### ✅ Kabul Edilebilir

* Kod standartlarına uygun
* Test edilmiş
* Dokümantasyon güncellenmiş
* Hata yönetimi mevcut
* Performans göz önünde bulundurulmuş

### ❌ Reddedilebilir

* Kod standartlarına uymayan
* Test edilmemiş
* Dokümantasyon eksik
* Hata yönetimi yok
* Performans sorunları

## İletişim

* **GitHub Issues** - Hata bildirimi ve özellik istekleri
* **GitHub Discussions** - Genel tartışmalar
* **Email** - alknbugra@gmail.com

## Lisans

Bu projeye katkıda bulunarak, katkılarınızın MIT lisansı altında lisanslanacağını kabul etmiş olursunuz.

---

**Teşekkürler! BaslerCamera projesine katkıda bulunduğunuz için minnettarız! 🙏**
