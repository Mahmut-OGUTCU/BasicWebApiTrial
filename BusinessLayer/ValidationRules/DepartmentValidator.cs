using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class DepartmentValidator : AbstractValidator<Department>
    {
        public DepartmentValidator()
        {
            RuleFor(x => x.DepartmentName).NotEmpty().WithMessage("Departman İsmini Doldurunuz.")
                .MinimumLength(2).WithMessage("Adı En Az 2 Karakter Giriniz.")
                .MaximumLength(25).WithMessage("Adı En Fazla 25 Karakter Giriniz.");

            RuleFor(x => x.DepartmentDescription).NotEmpty().WithMessage("Departman Açıklamasını Doldurunuz.")
                .MinimumLength(2).WithMessage("Açıklama İçin En Az 2 Karakter Giriniz.")
                .MaximumLength(60).WithMessage("Açıklama İçin En Fazla 60 Karakter Giriniz.");
        }
    }
}
