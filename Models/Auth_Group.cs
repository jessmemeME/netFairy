/*SE CREA UN ARCHIVO POR CADA TABLA QUE SE VA CONSULTAR O UTILIZAR EN EL SISTEMA
 Siempre debe ser publico para que se pueda acceder a el desde las otras clases.
la sintaxis es clase, tipo de atributo, nombre del atributo, metodos o acciones sobre el atributo.  
= public
dentro se tienen los atributos */

namespace FairyBE.Models
{
    public class Auth_Group //clase
    {
        //atributo ID
        public  int id { get; set; } //definición de atributo con los metodos que se habilitan para el. De acceso publico
        //atributo NAME
        public required string name { get; set; }
    }
}
