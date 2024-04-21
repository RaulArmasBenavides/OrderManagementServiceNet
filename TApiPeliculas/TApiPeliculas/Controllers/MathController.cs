using Microsoft.AspNetCore.Mvc;

namespace TApiPeliculas.Controllers
{
    [Route("api/Math")]
    [ApiController]
    public class MathController : ControllerBase
    {
        //[HttpGet(Name = "OperacionFloat")]
        //public float OperacionFloat()
        //{
        //    float a = 234.5f;
        //    float resultado = 0;

        //    resultado = a / 0;
        //    return resultado;
        //}

        [HttpGet(Name = "OperacionFloat2")]
        public bool OperacionFloat2()
        {
            double a1 = 0.1;
            double a2 = 0.2;

            double resultado = a1 + a2;

            bool Bandera = resultado == 0.3;

            return Bandera;
        }
    }
}
