using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebRfc.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View("Principal");
        }
        //ActionResult para ir a GenerarRFC
        public ActionResult IrGenerarRfc()
        {
            return View("GenerarRfc");
        }
        //ActionResult que crea y genera el RFC
        public ActionResult GenerarRfc(E_Rfc objRfc)
        {
            //Objeto de la capa de negocio
            N_Rfc negocio = new N_Rfc();
            negocio.CrearRegistro(objRfc);
            //negocio.RfcRepetido(objRfc);
            TempData["rfcCreado"] = negocio.RegistroCreado(objRfc);
            return View("MenuRfc", TempData);
        }
        //ActionResult para ir a la vista de los registros
        public ActionResult IrRegistros()
        {
            List<E_Rfc> lista = new List<E_Rfc>();
            //Objeto de la capa de negocio
            N_Rfc negocio = new N_Rfc();
            lista = negocio.MostrarTodos();
            int cantRegistros = lista.Count;
            TempData["cantRegistros"] = cantRegistros;
            return View("Registros", lista);
        }

        //Action para eliminar registros
        public ActionResult Eliminar(int idRfc)
        {
            //Objeto de la capa de negocio
            N_Rfc negocio = new N_Rfc();
            negocio.Eliminar(idRfc);
            return RedirectToAction("IrRegistros");
        }
        //Accion para obtener datos por el ID
        public ActionResult IrEditar(int IdRfc)
        {
            E_Rfc objRfc = new E_Rfc();
            //Objeto de la capa de negocio
            N_Rfc negocio = new N_Rfc();
            objRfc = negocio.ObtenerPorId(IdRfc);
            return View("VistaEditarRfc", objRfc);
        }

        //Accion para editar un registro
        public ActionResult EditarRfc(E_Rfc objRfc, int IdRfc)
        {
            //Objeto de la capa de negocio
            N_Rfc negocio = new N_Rfc();
            negocio.EditarRfc(objRfc, IdRfc);
            return RedirectToAction("IrRegistros");
        }

        //Accion para buscar
        public ActionResult Buscar(string BuscadorRfc)
        {
            //Lista vacia
            List<E_Rfc> lista = new List<E_Rfc>();
            //Objeto de la capa de negocio
            N_Rfc negocio = new N_Rfc();
            lista = negocio.Buscar(BuscadorRfc);
            int cantRegistros = lista.Count;
            TempData["cantRegistros"] = cantRegistros;
            return View("Registros",lista);
        }

    }
}