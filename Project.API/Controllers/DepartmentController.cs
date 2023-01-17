using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartmentController : ControllerBase
    {
        DepartmentManager dem = new DepartmentManager(new EfDepartmentDal());

        CheckLoginManager clm = new CheckLoginManager(new EfPersonDal());
        private Person _person;

        DepartmentValidator _departmentValidator = new DepartmentValidator();

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

        [HttpPost]
        [Route("get-all")]
        public ApiReturn<dynamic> GetAll([FromBody] Department p)
        {
            _person = clm.PersonCheck(ControllerContext);
            if (_person.PersonID <= 0)
                return new ApiReturn<dynamic>() { status = false, message = "Giriş Yapın." };

            try
            {
                if (p.DepartmentID < 0)
                {
                    //Hatalı bir id girilme durumu
                    return new ApiReturn<dynamic>() { status = false, message = ERROR_PARAM_INVALID };
                }
                else if (p.DepartmentID > 0)
                {
                    //Bir id girilme durumu
                    var departmentValue = dem.DepartmentGetByID(p.DepartmentID);

                    if (departmentValue == null)
                        return new ApiReturn<dynamic>() { status = false, message = ERROR_RECORD_NOT_FOUND };

                    return new ApiReturn<dynamic>() { status = true, message = SUCCESS, data = departmentValue };
                }
                else
                {
                    //Hiç bir id girilmediyse (id = 0)
                    var departmentValues = dem.DepartmentGetAll();

                    return new ApiReturn<dynamic>() { status = true, message = SUCCESS, data = departmentValues };

                }
            }
            catch (Exception e)
            {
                return new ApiReturn<dynamic>() { status = false, message = e.Message };
            }
        }

        [HttpPost]
        [Route("department-add")]
        public ApiReturn<dynamic> DepartmentAdd([FromBody] Department p)
        {
            _person = clm.PersonCheck(ControllerContext);
            if (_person.PersonID <= 0)
                return new ApiReturn<dynamic>() { status = false, message = "Giriş Yapın." };

            try
            {
                if (p == null)
                    return new ApiReturn<dynamic>() { status = false, message = "Gerekli Alanları Doldurunuz." };

                var result = _departmentValidator.Validate(p);
                if (!result.IsValid)
                    return new ApiReturn<dynamic>() { status = false, message = result.Errors[0].ToString() };

                p.DepartmentName = p.DepartmentName.Trim();
                p.DepartmentDescription = p.DepartmentDescription.Trim();

                var checkUniqueMail = dem.DepartmentGetByNameForUnique(p.DepartmentName);
                if (checkUniqueMail != null)
                    return new ApiReturn<dynamic>() { status = false, message = "Bu İsmi Kullanılmaktadır." };

                p.CreatedDate = DateTime.Now;
                p.CreatedID = _person.PersonID;
                p.IsActive = true;

                dem.DepartmentAdd(p);
                return new ApiReturn<dynamic>() { status = true, message = SUCCESS, data = p };
            }
            catch (Exception e)
            {
                return new ApiReturn<dynamic>() { status = false, message = e.Message };
            }
        }

        [HttpPost]
        [Route("department-update")]
        public ApiReturn<dynamic> DepartmentUpdate([FromBody] Department p)
        {
            _person = clm.PersonCheck(ControllerContext);
            if (_person.PersonID <= 0)
                return new ApiReturn<dynamic>() { status = false, message = "Giriş Yapın." };

            try
            {
                if (p == null)
                    return new ApiReturn<dynamic>() { status = false, message = "Gerekli Alanları Doldurunuz." };

                if (p.DepartmentID <= 0)
                    return new ApiReturn<dynamic>() { status = false, message = "Geçerli Bir ID Giriniz." };

                var result = _departmentValidator.Validate(p);
                if (!result.IsValid)
                    return new ApiReturn<dynamic>() { status = false, message = result.Errors[0].ToString() };

                p.DepartmentName = p.DepartmentName.Trim();
                p.DepartmentDescription = p.DepartmentDescription.Trim();

                var departmentValue = dem.DepartmentGetByID(p.DepartmentID);
                if (departmentValue == null)
                    return new ApiReturn<dynamic>() { status = false, message = "Geçersiz Kayıt." };

                var checkUniqueMail = dem.DepartmentGetByNameForUnique(p.DepartmentID, p.DepartmentName);
                if (checkUniqueMail != null)
                    return new ApiReturn<dynamic>() { status = false, message = "Farklı Bir e-Posta Adresi Giriniz." };

                departmentValue.DepartmentName = p.DepartmentName;
                departmentValue.DepartmentDescription = p.DepartmentDescription;
                departmentValue.UpdateDate = DateTime.Now;
                departmentValue.UpdateID = _person.PersonID;

                dem.DepartmentUpdate(departmentValue);

                return new ApiReturn<dynamic>() { status = true, message = SUCCESS_UPDATE, data = departmentValue };
            }
            catch (Exception e)
            {
                return new ApiReturn<dynamic>() { status = false, message = e.Message };
            }
        }

        [HttpPost]
        [Route("department-delete")]
        public ApiReturn<dynamic> DepartmentDelete([FromBody] Department p)
        {
            _person = clm.PersonCheck(ControllerContext);
            if (_person.PersonID <= 0)
                return new ApiReturn<dynamic>() { status = false, message = "Giriş Yapın." };

            if (!_person.PersonIsAdmin)
                return new ApiReturn<dynamic>() { status = false, message = "Kullanıcıları Sadece Admin Siler." };

            try
            {
                if (p.DepartmentID <= 0)
                {
                    //Hatalı bir id girilme durumu
                    return new ApiReturn<dynamic>() { status = false, message = "Geçersiz ID Değeri." };
                }

                var departmentValue = dem.DepartmentGetByID(p.DepartmentID);

                if (departmentValue == null)
                    return new ApiReturn<dynamic>() { status = false, message = "Böyle Bir Kullanıcı Bulunamadı." };

                departmentValue.UpdateDate = DateTime.Now;
                departmentValue.UpdateID = _person.PersonID;
                departmentValue.IsActive = false;

                dem.DepartmentUpdate(departmentValue);

                return new ApiReturn<dynamic>() { status = true, message = "Kayıt Başarıyla Silindi.", data = departmentValue };
            }
            catch (Exception e)
            {
                return new ApiReturn<dynamic>() { status = false, message = e.Message };
            }
        }
    }
}
