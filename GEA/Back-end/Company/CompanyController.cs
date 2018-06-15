﻿using GEA.Back_end.Company.Models;
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

        #region //Método listar Empresas
        //GET: DB
        public ActionResult Index()
        {
            return View();
        }
        //GET Company/GetCompanies
        public JsonResult GetCompanies()
        {
            var result = new JsonResult();
                var listCompanies = new List<COMPANIES>();
                listCompanies = db.Company.ToList();

                result = Json(listCompanies, JsonRequestBehavior.AllowGet);
                return result;
        }
        #endregion
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
                company.CreateadWhen = DateTime.Now;
                db.Company.Add(company);
                db.SaveChanges();
                result = Json(new { success = true });
            }
            return result;
        }

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
            }
            
            return failures;


        }
    }
}