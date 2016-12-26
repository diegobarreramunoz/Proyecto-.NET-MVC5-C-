using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using pso.cdd.model;
using Circon.Common;

namespace pso.cdd.mvc.Controllers {
    public class HomeController : MasterController {
        public ActionResult Index() {


            int usuario_id = SesionLogin.id_usu;
            ViewBag.sum_mes_cab = _db.cab_ts.Where(p => p.id_usu == usuario_id && p.fecha.Month == DateTime.Today.Month).Sum(z => (Decimal?)z.horas) ?? 0;
            ViewBag.sum_dia_cab = _db.cab_ts.Where(p => p.id_usu == usuario_id && p.fecha.Day == DateTime.Today.Day).Sum(z => (Decimal?)z.horas) ?? 0;
            var result = LoadData("select isnull(sum(horas),0) as horas_suma " +
                "				from dbo.cab_ts " +
                " 				where fecha between(select DATEADD(wk, DATEDIFF(wk, 0,GETDATE()),0))and getdate() and id_usu =" + usuario_id);
            ViewBag.sem_dia_cab = Decimal.Round(Decimal.Parse((result.FirstOrDefault())["horas_suma"].ToString())/60,2);
            ViewBag.perfiles = SesionPermisos;

            return View();
        }



        public ActionResult Login() {

            string[]  domain_user= Request.LogonUserIdentity.Name.ToString().Split('\\');
            string userDominio = domain_user[1];
            usuarios esUsuarioDeDominio = _db.usuarios.FirstOrDefault(p => p.Nom_cor_usu == userDominio);
            if (null != esUsuarioDeDominio)
            {
                SesionLogin = new usuarios
                {
                    id_usu = esUsuarioDeDominio.id_usu,
                    Nom_usu = esUsuarioDeDominio.Nom_usu,
                    Nom_cor_usu = esUsuarioDeDominio.Nom_cor_usu,
                    pass_usu = esUsuarioDeDominio.pass_usu,
                    jefatura = esUsuarioDeDominio.jefatura,
                    id_cargo = esUsuarioDeDominio.id_cargo,
                    id_unidad = esUsuarioDeDominio.id_unidad,
                    fecha_mod = esUsuarioDeDominio.fecha_mod,
                    usuario_mod = esUsuarioDeDominio.usuario_mod,
                    estado = esUsuarioDeDominio.estado,
                    jerarquia = esUsuarioDeDominio.jerarquia
                };
                SesionPermisos = _db.perfil_usuario.Where(p => p.id_usuario == esUsuarioDeDominio.id_usu).ToList();
                FormsAuthentication.SetAuthCookie(esUsuarioDeDominio.Nom_cor_usu, true);
                return RedirectToAction("Index");
            }
            return View();
        }


        [HttpPost]
        public ActionResult Login(string nom_cor_usu, string pass_usu) {
            if (string.IsNullOrEmpty(nom_cor_usu))
                ModelState.AddModelError("rut", "ingrese Nombre de usuario");
            if (string.IsNullOrEmpty(pass_usu))
                ModelState.AddModelError("password", "ingrese contraseña");

            if (!ModelState.IsValid) return View();
            var pwd = pass_usu;//.MD5Hash();
            var usr = _db.usuarios.FirstOrDefault(p => p.Nom_cor_usu == nom_cor_usu && p.pass_usu.Equals(pwd));
            if (usr == null || usr.pass_usu != pwd)
            {
                ModelState.AddModelError("password", "Nombre de usuario o contraseña incorrectos");
                return View();
            }
            SesionLogin = new usuarios
            {
                id_usu = usr.id_usu,
                Nom_usu = usr.Nom_usu,
                Nom_cor_usu = usr.Nom_cor_usu,
                pass_usu = usr.pass_usu,
                jefatura = usr.jefatura,
                id_cargo = usr.id_cargo,
                id_unidad = usr.id_unidad,
                fecha_mod = usr.fecha_mod,
                usuario_mod = usr.usuario_mod,
                estado = usr.estado,
                jerarquia = usr.jerarquia
            };
            SesionPermisos = _db.perfil_usuario.Where(p => p.id_usuario == usr.id_usu).ToList();
            FormsAuthentication.SetAuthCookie(nom_cor_usu, true);
            return RedirectToAction("Index");
        }

        public ActionResult Logout() {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return View("Login");
        }


        public ActionResult Panel()
        {
            return View();
        }

        public ActionResult Panel1()
        {
            return View();
        }
        public ActionResult Panel2()
        {
            return View();
        }
        public ActionResult Panel3()
        {
            return View();
        }
        public ActionResult Panel4()
        {
            return View();
        }
        public ActionResult Panel5()
        {
            return View();
        }
        public ActionResult Panel6()
        {
            return View();
        }
        public ActionResult Panel7()
        {
            return View();
        }
        public ActionResult Panel8()
        {
            return View();
        }
        public ActionResult Panel9()
        {
            return View();
        }
		        public ActionResult Panel10()
        {
            return View();
        }
        public ActionResult Panel11()
        {
            return View();
        }
        public ActionResult MisDatos()
        {

            int usuario_id = SesionLogin.id_usu;
            var usuarios = (usuarios)_db.usuarios.SingleOrDefault(p=>p.id_usu == usuario_id);
            ViewBag.listaCab_ts = _db.cab_ts.Where(p => p.usuario_mod == SesionLogin.Nom_cor_usu);
            return View(usuarios);
        }
    }
}