using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Project.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        PersonManager pem = new PersonManager(new EfPersonDal());
        CheckLoginManager clm = new CheckLoginManager(new EfPersonDal());
        DepartmentManager dem = new DepartmentManager(new EfDepartmentDal());

        PersonValidator _personValidator = new PersonValidator();

        private Person _person;

        #region # sabitler ve degiskenler #
        // api geri donus cevaplari
        private const string ERROR_NEEDED_LOGIN = "Login olmalısınız";
        private const string ERROR_MODEL_NOT_NULL = "Veri model boş olamaz";
        private const string ERROR_PARAM_NOT_NULL = "Parametre boş olamaz";
        private const string ERROR_PARAM_INVALID = "Parametre hatalı";
        private const string ERROR_PARAM_INVALID_OR_NULL = "Parametre bos olamaz ya da gecersiz";
        private const string ERROR_USERNAME_AND_PASSW_INVALID_OR_NULL = "Kullanıcı adı veya Şifre alanı bos olamaz ya da gecersiz";
        private const string ERROR_USENAME_OR_PASSWORD_NOT_VALID = "Kullanıcı adı veya Şifre geçersiz";
        private const string ERROR_ACCOUNT_DISABLED = "Bu kullanıcı hesabı onaylanmadığı için kullanılamaz.";
        private const string ERROR_RECORD_NOT_FOUND = "Kayıt bulunamadı";
        private const string SUCCESS = "Veriler Hazır";
        private const string SUCCESS_INSERT = "Yeni kayıt ekleme işlemi tamamlandı";
        private const string SUCCESS_UPDATE = "Güncelleme tamamlandı";
        private const string SUCCESS_REMOVE = "Kayıt(lar) silindi";
        private const string SUCCESS_LOGIN = "Login başarılı";
        #endregion

        #region # metodlar #

        [HttpPost]
        [Route("get-all")]
        public ApiReturn<dynamic> GetAll([FromBody] Person p)
        {
            _person = clm.PersonCheck(ControllerContext);
            if (_person.PersonID <= 0)
                return new ApiReturn<dynamic>() { status = false, message = "Giriş Yapın." };

            try
            {
                if (p.PersonID < 0)
                {
                    //Hatalı bir id girilme durumu
                    return new ApiReturn<dynamic>() { status = false, message = ERROR_PARAM_INVALID };
                }
                else if (p.PersonID > 0)
                {
                    //Bir id girilme durumu
                    var personValue = pem.PersonGetByID(p.PersonID);

                    if (personValue == null)
                        return new ApiReturn<dynamic>() { status = false, message = ERROR_RECORD_NOT_FOUND };

                    return new ApiReturn<dynamic>() { status = true, message = SUCCESS, data = personValue };
                }
                else
                {
                    //Hiç bir id girilmediyse (id = 0)
                    var personValues = pem.PersonGetAll();
                    var departmentValues = dem.DepartmentGetAll();

                    //Verileri yollamadan önce pass ve token gibi bilgileri sıfırla, departman name ekle
                    var responseData = from person in personValues
                                       join department in departmentValues
                                       on person.DepartmentID equals department.DepartmentID
                                       select new
                                       {
                                           PersonID = person.PersonID,
                                           PersonFirstName = person.PersonFirstName,
                                           PersonLastName = person.PersonLastName,
                                           PersonEmail = person.PersonEmail,
                                           PersonPhone = person.PersonPhone,
                                           PersonIsAdmin = person.PersonIsAdmin,
                                           PersonLastActivited = person.PersonLastActivited,
                                           DepartmentID = person.DepartmentID,
                                           CreatedDate = person.CreatedDate,
                                           CreatedID = person.CreatedID,
                                           IsActive = person.IsActive,
                                           UpdateDate = person.UpdateDate,
                                           UpdateID = person.UpdateID,
                                           Department = department.DepartmentName,
                                       };

                    return new ApiReturn<dynamic>() { status = true, message = SUCCESS, data = responseData };

                }
            }
            catch (Exception e)
            {
                return new ApiReturn<dynamic>() { status = false, message = e.Message };
            }
        }

        [HttpPost]
        [Route("person-add")]
        public ApiReturn<dynamic> PersonAdd([FromBody] Person p)
        {
            _person = clm.PersonCheck(ControllerContext);
            if (_person.PersonID <= 0)
                return new ApiReturn<dynamic>() { status = false, message = "Giriş Yapın." };

            try
            {
                if (p == null)
                    return new ApiReturn<dynamic>() { status = false, message = "Gerekli Alanları Doldurunuz." };

                var result = _personValidator.Validate(p);
                if (!result.IsValid)
                    return new ApiReturn<dynamic>() { status = false, message = result.Errors[0].ToString() };

                p.PersonFirstName = p.PersonFirstName.Trim();
                p.PersonLastName = p.PersonLastName.Trim();
                p.PersonPassword = p.PersonPassword.Trim();
                p.PersonEmail = p.PersonEmail.Trim();
                p.PersonPhone = p.PersonPhone.Trim();

                var checkUniqueMail = pem.PersonGetByMailForUnique(p.PersonEmail);
                if (checkUniqueMail != null)
                    return new ApiReturn<dynamic>() { status = false, message = "Farklı Bir e-Posta Adresi Giriniz." };

                var checkUniquePhone = pem.PersonGetByPhoneForUnique(p.PersonPhone);
                if (checkUniquePhone != null)
                    return new ApiReturn<dynamic>() { status = false, message = "Farklı Bir Telefon Numarası Giriniz." };

                p.PersonToken = "";
                p.PersonIsAdmin = false;
                p.CreatedDate = DateTime.Now;
                p.CreatedID = _person.PersonID;
                p.IsActive = true;

                pem.PersonAdd(p);
                return new ApiReturn<dynamic>() { status = true, message = SUCCESS, data = p };
            }
            catch (Exception e)
            {
                return new ApiReturn<dynamic>() { status = false, message = e.Message };
            }
        }

        [HttpPost]
        [Route("person-update")]
        public ApiReturn<dynamic> PersonUpdate([FromBody] Person p)
        {
            _person = clm.PersonCheck(ControllerContext);
            if (_person.PersonID <= 0)
                return new ApiReturn<dynamic>() { status = false, message = "Giriş Yapın." };

            try
            {
                if (p == null)
                    return new ApiReturn<dynamic>() { status = false, message = "Gerekli Alanları Doldurunuz." };

                if (p.PersonID <= 0)
                    return new ApiReturn<dynamic>() { status = false, message = "Geçerli Bir ID Giriniz." };

                var result = _personValidator.Validate(p);
                if (!result.IsValid)
                    return new ApiReturn<dynamic>() { status = false, message = result.Errors[0].ToString() };

                p.PersonFirstName = p.PersonFirstName.Trim();
                p.PersonLastName = p.PersonLastName.Trim();
                p.PersonPassword = p.PersonPassword.Trim();
                p.PersonEmail = p.PersonEmail.Trim();
                p.PersonPhone = p.PersonPhone.Trim();

                var personValue = pem.PersonGetByID(p.PersonID);
                if (personValue == null)
                    return new ApiReturn<dynamic>() { status = false, message = "Geçersiz Kayıt." };

                var checkUniqueMail = pem.PersonGetByMailForUnique(p.PersonID, p.PersonEmail);
                if (checkUniqueMail != null)
                    return new ApiReturn<dynamic>() { status = false, message = "Farklı Bir e-Posta Adresi Giriniz." };

                var checkUniquePhone = pem.PersonGetByPhoneForUnique(p.PersonID, p.PersonPhone);
                if (checkUniquePhone != null)
                    return new ApiReturn<dynamic>() { status = false, message = "Farklı Bir Telefon Numarası Giriniz." };

                personValue.PersonFirstName = p.PersonFirstName;
                personValue.PersonLastName = p.PersonLastName;
                personValue.PersonEmail = p.PersonEmail;
                personValue.PersonPhone = p.PersonPhone;
                personValue.PersonPassword = p.PersonPassword;
                personValue.UpdateDate = DateTime.Now;
                personValue.UpdateID = _person.PersonID;

                pem.PersonUpdate(personValue);

                return new ApiReturn<dynamic>() { status = true, message = SUCCESS_UPDATE, data = personValue };
            }
            catch (Exception e)
            {
                return new ApiReturn<dynamic>() { status = false, message = e.Message };
            }
        }

        [HttpPost]
        [Route("person-delete")]
        public ApiReturn<dynamic> PersonDelete([FromBody] Person p)
        {
            _person = clm.PersonCheck(ControllerContext);
            if (_person.PersonID <= 0)
                return new ApiReturn<dynamic>() { status = false, message = "Giriş Yapın." };

            if (!_person.PersonIsAdmin)
                return new ApiReturn<dynamic>() { status = false, message = "Kullanıcıları Sadece Admin Siler." };

            try
            {
                if (p.PersonID <= 0)
                {
                    //Hatalı bir id girilme durumu
                    return new ApiReturn<dynamic>() { status = false, message = "Geçersiz ID Değeri." };
                }

                var personValue = pem.PersonGetByID(p.PersonID);

                if (personValue == null)
                    return new ApiReturn<dynamic>() { status = false, message = "Böyle Bir Kullanıcı Bulunamadı." };

                personValue.UpdateDate = DateTime.Now;
                personValue.UpdateID = _person.PersonID;
                personValue.IsActive = false;

                pem.PersonUpdate(personValue);

                return new ApiReturn<dynamic>() { status = true, message = "Kayıt Başarıyla Silindi.", data = personValue };
            }
            catch (Exception e)
            {
                return new ApiReturn<dynamic>() { status = false, message = e.Message };
            }
        }


        #endregion

    }
}
