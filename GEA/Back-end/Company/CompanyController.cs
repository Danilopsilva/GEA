using GEA.Back_end.Company.Models;
using GEA.Back_end.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GEA.Back_end.Companies
{
    public class CompanyController : Controller
    {
        GeaEntities db = new GeaEntities();

        #region Método listar Empresas
        //GET: DB
        public ActionResult CompanyForm()
        {
            return View();
        }
        //GET Company/GetCompanies
        public JsonResult GetCompanies()
        {
            var result = new JsonResult();
                var listCompanies = new List<COMPANIES>();
                listCompanies = db.COMPANIES.ToList();

                result = Json(listCompanies, JsonRequestBehavior.AllowGet);
                return result;
        }
        #endregion

        #region Método responsável por salvar empresas
        [HttpPost]
        public JsonResult SaveCompany(COMPANIES company)
        {
            var result = new JsonResult();
            var validationFailures = new List<ValidationFailures>();

            validationFailures = validateSave(company).ToList();

            if (validationFailures.Any())
            {
                result = Json(new { failures = validationFailures[0].Message });
                return result;
            }
            else
            {
                company.CreatedWhen = DateTime.Now.Date;
                db.COMPANIES.Add(company);
                db.SaveChanges();
                result = Json(new { success = true });
            }
            return result;
        }
        #endregion

        #region Método Responsável por atualizar Empresas
        [HttpPost]
        public JsonResult UpdateCompany(COMPANIES company)
        {
            var result = new JsonResult();
            var validationFailures = new List<ValidationFailures>();

            validationFailures = validateSave(company).ToList();
            if (validationFailures.Any())
            {
                result = Json(new { failures = validationFailures[0].Message });
                return result;

            }
            else
            {
                var newCompany = db.COMPANIES.SingleOrDefault(b => b.CompanyId == company.CompanyId);
                if (newCompany != null)
                {
                    newCompany.Name = company.Name;
                    newCompany.Cnpj = company.Cnpj;
                    newCompany.CompanyId = company.CompanyId;
                    newCompany.Email = company.Email;
                    newCompany.Phone = company.Phone;

                    db.SaveChanges();
                    return Json(new { success = true });
                }

            }
              return Json(new { Success = false });
        }
        #endregion

        #region Método Responsável por Remover Empresas
        [HttpPost]
        public JsonResult DeleteCompany(int id)
        {
            var company = db.COMPANIES.Find(id);
            if (company != null)
            {
                db.COMPANIES.Remove(company);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        #endregion

        #region Método de validação do Salvar e Atualizar
        public static IList<ValidationFailures> validateSave(COMPANIES company)
        {
            var failures = new List<ValidationFailures>();

            if (company == null)
            {
                failures.Add(new ValidationFailures
                {
                    Message = "Por favor Preencha todos os Campos."
                });
            }
            else
            {
                if (string.IsNullOrEmpty(company.Name))
                {
                    failures.Add(new ValidationFailures
                    {
                        Message = "Por favor informe o nome da empresa."
                    });
                }
                if (string.IsNullOrEmpty(company.Cnpj))
                {
                    failures.Add(new ValidationFailures
                    {
                        Message = "Por favor informe o Cnpj da empresa."
                    });
                }
                var isValid = IsCnpj(company.Cnpj);

                if (!isValid)
                {
                    failures.Add(new ValidationFailures
                    {
                        Message = "Por favor informe um Cnpj Válido."
                    });
                }
            }
            
            return failures;
        }
        #endregion

        #region Método responsável por validar Cpf
        public static bool IsCnpj(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }
        #endregion
    }
}
