using Microsoft.AspNetCore.Mvc;
using MvcCoreEmpleadosSession.Models;
using MvcCoreEmpleadosSession.Repositories;

namespace MvcCoreEmpleadosSession.Controllers
{
    public class EmpleadosController : Controller
    {
        private RepositoryEmpleados repo;

        public EmpleadosController(RepositoryEmpleados repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> SessionEmpleados()
        {
            List<Empleado> empleados =
                await this.repo.GetEmpleadosAsync();
            return View(empleados);
        }

        public IActionResult EmpleadosAlmacenados()
        {
            return View();
        }

        public async Task<IActionResult> SessionSalarios(int? salario)
        {
            if (salario != null)
            {
                //NECESITAMOS ALMACENAR EL SALARIO TOTAL 
                //DE TODOS LOS EMPLEADOS DE SESSION
                int sumaSalarial = 0;
                //PREGUNTAMOS SI YA TENEMOS LA SUMA EN SESSION
                if (HttpContext.Session.GetString("SUMASALARIAL") != null)
                {
                    //RECUPERAMOS LA SUMA SALARIAL DE SESSION
                    sumaSalarial = int.Parse
                        (HttpContext.Session.GetString("SUMASALARIAL"));
                }
                //REALIZAMOS LA SUMA DEL SALARIO RECIBIDO
                sumaSalarial += salario.Value;
                //ALMACENAMOS LA NUEVA SUMA SALARIAL EN SESSION
                HttpContext.Session.SetString("SUMASALARIAL", sumaSalarial.ToString());
                ViewData["MENSAJE"] = "Salario almacenado: " + salario.Value;
            }
            List<Empleado> empleados = await
                this.repo.GetEmpleadosAsync();
            return View(empleados);
        }

        public IActionResult SumaSalarios()
        {
            return View();
        }
    }
}
