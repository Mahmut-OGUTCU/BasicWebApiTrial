using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(x => x.PersonFirstName).NotEmpty().WithMessage("Adı Doldurunuz.")
                .MinimumLength(2).WithMessage("Adı En Az 2 Karakter Giriniz.")
                .MaximumLength(15).WithMessage("Adı En Fazla 15 Karakter Giriniz.");

            RuleFor(x => x.PersonLastName).NotEmpty().WithMessage("Soyadı Doldurunuz.")
                .MinimumLength(2).WithMessage("Soyadı En Az 2 Karakter Giriniz.")
                .MaximumLength(15).WithMessage("Soyadı En Fazla 15 Karakter Giriniz.");

            RuleFor(x => x.PersonEmail).NotEmpty().WithMessage("Epostayı Doldurunuz.")
                .EmailAddress().WithMessage("Geçerli Bir E Posta Adresi Giriniz.");

            RuleFor(x => x.PersonPhone).NotEmpty().WithMessage("Telefon Numarasını Doldurunuz.")
                .MinimumLength(10).WithMessage("Telefon Numarası En Az 10 Karakter Giriniz.")
                .MaximumLength(15).WithMessage("Telefon Numarası En Fazla 15 Karakter Giriniz.");

            RuleFor(x => x.PersonPassword).NotEmpty().WithMessage("Şifreyi Doldurunuz.")
                    .MinimumLength(8).WithMessage("Şifre En Az 8 Karakter Giriniz.")
                    .MaximumLength(15).WithMessage("Şifre En Fazla 15 Karakter Giriniz.")
                    .Matches(@"[A-Z]+").WithMessage("Şifre En Az 1 Büyük Harf Giriniz.")
                    .Matches(@"[a-z]+").WithMessage("Şifre En Az 1 Küçük Harf Giriniz.")
                    .Matches(@"[0-9]+").WithMessage("Şifre En Az 1 Rakam Giriniz.")
                    .Matches(@"[\!\?\*\.]+").WithMessage("Şifre En Az 1 Özel Karakter (!? *.) Giriniz.");

            RuleFor(x => x.DepartmentID).NotEmpty().WithMessage("Departanı Doldurunuz.");
        }
    }
}
