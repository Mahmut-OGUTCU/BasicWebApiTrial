# Project.API

Proje EntityFramework Generic Repository Pattern düzeyinde inşaa edilmiştir.

Projede JWT ile dinamik olarak 12 saatlik bir token oluşturulur ve token aktif olduğu sürece kullanıcı tekrar login olmadan apiye istek atabilir. Her istek atmada token kontrol edilmektedir.

Proje 3 temel Controller'dan oluşmaktadır:
* Person işlemlerinin gerçekleştirildiği
* Departman işlemlerinin gerçekleştirildiği
* Mail işlemlerinin gerçekleştirildiği

Request'e karşılık gelen Response için ApiReturn class'ı sayesinde status, message ve data(varsa) pratik bir şekilde gönderilir.

Person ve Department için database'de silme işlemi IsActive olarak tanımlanan alanda geçerlilik gösterir. true ise kullanılabilirdir.

Kullanıcı kendini kaydedemez bu nedenle ilk şifre admin tarafından SHA256 algoritması kullanılarak kayıt edilir. Kayıt ederken ve güncellerken FluentValidation ile de gerekli kontroller sağlanmıştır.

Not: Mail gönderimi de mevcuttur. İlerleyen süreçte proje içine kullanıcı eklenince eklenen kullanıcıya mail gönderimi gerçekleştirilecektir.

Url'de herhangi bir verinin gitmesi, veri güvenliğine aykırı olmasından dolayı tüm işlemler HttpPost olarak gerçekleştirilmiştir.

Person; verilerini getirme, ekleme, güncelleme, silme
Departman; verilerini getirme, ekleme, güncelleme, silme
Mail; ekleme

