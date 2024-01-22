﻿/*SE CREA UN ARCHIVO POR CADA TABLA QUE SE VA CONSULTAR O UTILIZAR EN EL SISTEMA
 Siempre debe ser publico para que se pueda acceder a el desde las otras clases.
la sintaxis es clase, tipo de atributo, nombre del atributo, metodos o acciones sobre el atributo.  
= public
dentro se tienen los atributos */

using System.ComponentModel.DataAnnotations;

namespace FairyBE.Models
{
    public class Auth //clase
    {
        //atributo ID
        public  int id { get; set; } //definición de atributo con los metodos que se habilitan para el. De acceso publico
        //atributo NAME
        public required string name { get; set; }
    }


    public class AuthGroupPermissions
    {
        public int id { get; set; }
        [Required] public bool group_id { get; set; }
        [Required] public bool permission_id { get; set; }

        /*
            "id"	"bigint"
            "group_id"	"integer"
            "permission_id"	"integer"
         TABLE_NAME = 'auth_group_permissions'
         */
    }

    public class AuthPermissions
    {
        public int id { get; set; }
        public required string name { get; set; }
        public required int content_type_id { get; set; }
        public required string codename { get; set; }


        /*
            "id"	"integer"
            "name"	"character varying"
            "content_type_id"	"integer"
            "codename"	"character varying"
        TABLE_NAME = 'auth_permission'
         */
    }
    

    public class AuthContentType
    {
        public int id { get; set; }
        public required string app_label { get; set; }
        public required string model { get; set; }


        /*
        "id"	"integer"
        "app_label"	"character varying"
        "model"	"character varying"
        TABLE_NAME = 'auth_content_type'
         */
    }


}
