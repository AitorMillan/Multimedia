using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multimedia
{
    internal class Usuario
    {
        //Atributos de la clase usuario
        //Añadir getters y setter

        private String userName;
        private String pws;


        //Constructir de la clase usuario
        public Usuario(String userName, String pws)
        {
            this.userName = userName; 
            this.pws = pws;
        }

        //Metodos de la clase usuario
        public String getUserName()
        {
            return userName;
        }
        public String getPws()
        {
               return pws;
        }

        public void setUserName(String userName)
        {
            this.userName = userName;
        }

        public void setPws(String pws)
        {
            this.pws = pws;
        }

        public override string ToString()
        {
            return "Usuario: " + userName + " Contraseña: " + pws;
        }


    }
}
