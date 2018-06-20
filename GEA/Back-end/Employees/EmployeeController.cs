using GEA.Back_end.Company.Models;
using GEA.Back_end.Employees.Models;
using GEA.Back_end.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace GEA.Back_end.Employees
{

    public class EmployeeController : Controller
    {
        GeaEntities1 db = new GeaEntities1();

        #region Método listar Funcionarios
        //GET: DB
        public ActionResult EmployeeForm()
        {
            return View();
        }
        //GET Company/GetCompanies
        public JsonResult GetEmployees()
        {
            var result = new JsonResult();
            var listEmployees = new List<EMPLOYEES>();
            listEmployees = db.EMPLOYEES.ToList();

            result = Json(listEmployees, JsonRequestBehavior.AllowGet);
            return result;
        }
        #endregion

        #region Método responsável por listar Empresas

        public JsonResult GetCompanies()
        {   
            var result = new JsonResult();
            using (var dbCompany = new GeaEntities())
            {
                var listCompanies = new List<COMPANIES>();
                listCompanies = dbCompany.Company.ToList();

                result = Json(listCompanies, JsonRequestBehavior.AllowGet);
            }
                return result;
        }
        #endregion

        #region Método responsável por salvar funcionarios
        [HttpPost]
        public JsonResult SaveEmployee(EMPLOYEES employee)
        {
            var result = new JsonResult();
            var validationFailures = new List<ValidationFailures>();

            validationFailures = validateSave(employee).ToList();

            if (validationFailures.Any())
            {
                result = Json(new { failures = validationFailures[0].Message });
                return result;
            }
            else
            {
                employee.CreatedWhen = DateTime.Now.Date;
                db.EMPLOYEES.Add(employee);
                db.SaveChanges();
                result = Json(new { success = true });
            }
            return result;
        }
        #endregion

        #region Método Responsável por atualizar Funcionários
        [HttpPost]
        public JsonResult UpdateEmployee(EMPLOYEES employee)
        {
            var result = new JsonResult();
            var validationFailures = new List<ValidationFailures>();

            validationFailures = validateSave(employee).ToList();
            if (validationFailures.Any())
            {
                result = Json(new { failures = validationFailures[0].Message });
                return result;

            }
            else
            {
                var newCompany = db.EMPLOYEES.SingleOrDefault(b => b.Id == employee.Id);
                if (newCompany != null)
                {
                    newCompany.Name = employee.Name;
                    newCompany.Cpf = employee.Cpf;

                    db.SaveChanges();
                    return Json(new { success = true });
                }

            }
            return Json(new { Success = false });
        }
        #endregion

        #region Método Responsável por Remover Funcionarios
        [HttpPost]
        public JsonResult DeleteEmployee(int id)
        {
            var employee = db.EMPLOYEES.Find(id);
            if (employee != null)
            {
                db.EMPLOYEES.Remove(employee);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        #endregion

        #region Método de validação do Salvar e Atualizar
        public static IList<ValidationFailures> validateSave(EMPLOYEES employee)
        {
            var failures = new List<ValidationFailures>();

            if (employee == null)
            {
                failures.Add(new ValidationFailures
                {
                    Message = "Por favor Preencha todos os Campos."
                });
            }
            else
            {
                if (string.IsNullOrEmpty(employee.Name))
                {
                    failures.Add(new ValidationFailures
                    {
                        Message = "Por favor informe o nome do funcionário."
                    });
                }
                if (string.IsNullOrEmpty(employee.Cpf))
                {
                    failures.Add(new ValidationFailures
                    {
                        Message = "Por favor informe o Cpf do funcionário."
                    });
                }
                if (!string.IsNullOrEmpty(employee.Cpf))
                {
                    var isValid = IsCpf(employee.Cpf);

                    if (!isValid)
                    {
                        failures.Add(new ValidationFailures
                        {
                            Message ="Por favor digite um Cpf Válido."
                        });
                    }
                }
                if(employee.CompanyId <=0)
                {
                    failures.Add(new ValidationFailures
                    {
                        Message = "Por favor Selecione uma Empresa."
                    });
                }
            }

            return failures;
        }
        #endregion

        #region Método Responsável por Validar o Cpf

        public static bool IsCpf(string cpf)
            {
                int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                string tempCpf;
                string digito;
                int soma;
                int resto;
                cpf = cpf.Trim();
                cpf = cpf.Replace(".", "").Replace("-", "");
                if (cpf.Length != 11)
                    return false;
                tempCpf = cpf.Substring(0, 9);
                soma = 0;

                for (int i = 0; i < 9; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = resto.ToString();
                tempCpf = tempCpf + digito;
                soma = 0;
                for (int i = 0; i < 10; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = digito + resto.ToString();
                return cpf.EndsWith(digito);
        }
        #endregion
    }
}
